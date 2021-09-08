﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	[Header("La liste des AudioAssets (à paramétrer)")]
	public List<AudioAsset> assets;

	[Space(20)]
	[Header("Paramètres généraux")]
	public List<AudioSource> sourcesBruitages;

	[Space(10)]
	public AudioSource sourceGlobalMusic;
	public bool playMusicAtStart; // globalMusic


	//n'oubliez pas de mettre vos codes ici !!
	public enum ClipsName{
		SHOT,
		ARROW,
		HORROR
	}
	private static AudioManager instance = null;

	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (instance == null)
		{
			instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when loading another scene.
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		if (playMusicAtStart) {
			sourceGlobalMusic.Play ();
		}
		else {
			sourceGlobalMusic.Stop ();
		}
	}

	public static void PlayAudioAsset(AudioManager.ClipsName enumCode,GameObject user = null){

		/*** récupération de l'AudioAsset demandé ***/
		AudioAsset assetToUse = null;
		foreach (AudioAsset asset in instance.assets) {
			if (asset.enumCode == enumCode) {
				assetToUse = asset;
			}					
		}


		/**** sélection d'une AudioSource disponible ***/
		AudioSource sourceToUse = null;
		foreach (AudioSource source in instance.sourcesBruitages) {	
			if (source.outputAudioMixerGroup == assetToUse.mixerGroup && user == null ) {	//attention si 3D, ne pas utiliser le même audioSource 
				sourceToUse = source;
				break;
			}
			else {
				if(!source.isPlaying){
					sourceToUse = source;
					break;
				}	
			}								
		}

		/***** on joue le son ***********/
		if (assetToUse != null) {
			if (sourceToUse != null) {
				sourceToUse.outputAudioMixerGroup = assetToUse.mixerGroup;

				//besoin de 3D ? Déplace l'audiosource utilisée en enfant du gameobject et le met à la même position
				if(user != null){
					sourceToUse.spatialBlend = 1; // 1 = 3D  | 0 = 2D
					sourceToUse.gameObject.transform.SetParent(user.transform);
					sourceToUse.gameObject.transform.position = user.transform.position;
				}

				//besoin de looping ?
				if (assetToUse.isLooping) {
					sourceToUse.loop = assetToUse.isLooping;
					sourceToUse.clip = assetToUse.clip;
					sourceToUse.Play ();
				} 
				else {			
					sourceToUse.PlayOneShot (assetToUse.clip);
				}
			}
			else {
				Debug.LogError ("Pas assez d'audioSource !!");	
			}
		}
		else {			
			Debug.LogError ("Cet Asset n'existe pas !!");			
		}

	}

	/***** lancer.stopper la globalMusic   ******/
	public static void PlayMusic(){
		instance.sourceGlobalMusic.Play ();
	}

	public static void StopMusic(){
		instance.sourceGlobalMusic.Stop ();
	}	

	/*********************************************/

}

[System.Serializable]
public class AudioAsset{

	[TooltipAttribute("Utiliser le même nom que l'énumération")]
	public string idName;
	[TooltipAttribute("Identification visuelle")]
	public string description;

	[Space(20)]
	public AudioManager.ClipsName enumCode;
	public AudioClip clip;
	public AudioMixerGroup mixerGroup;

	[Space(10)]
	public bool isLooping;

}

