using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    private PlayerController playerController;
    [SerializeField] public float movementSpeed = 0f;
    [SerializeField] public float speedDecreaseValue = 0.5f;
    [SerializeField] public float timerBeforeSpeedDecrease = 5f;
    public bool hasPacemaker, hasOxygen, hasCachet;

    public float minSpeedValue = 0f, maxSpeedValue = 50f;
    private GameOverConditions gameOver;

    private Oxygen oxygen;
    private Pacemaker pacemaker;
    private Cachet cachet;
   
    void Start()
    {
        // Le joueur démarre avec un cachet
        hasCachet = true;

        playerController = GetComponent<PlayerController>();
        gameOver = GetComponent<GameOverConditions>();
        oxygen = GetComponent<Oxygen>();
        pacemaker = GetComponent<Pacemaker>();
        cachet = GetComponent<Cachet>();

        StartCoroutine(DecreaseSpeedOvertime());
    }

    void Update()
    {
        playerController.forwardSpeed = movementSpeed;
        Debug.Log($"Current Player Speed: "+playerController.forwardSpeed);
        //PlayCorrespondingSound();
    }
    IEnumerator DecreaseSpeedOvertime()
    {
        if (!gameOver.hasLost)
        {
            yield return new WaitForSeconds(timerBeforeSpeedDecrease);
            movementSpeed = Mathf.Clamp(movementSpeed - speedDecreaseValue, minSpeedValue, maxSpeedValue);
            gameOver.CheckPlayerMovementSpeed();
            StartCoroutine(DecreaseSpeedOvertime());
        }
        else
        {
            //Debug.Log($"Player has lost");
        }
    }
    void PlayCorrespondingSound()
    {
        if (oxygen.hasOxygenBuff)
        {
            AudioManager.PlayAudioAsset(AudioManager.ClipsName.OXYGEN_OPENING, null);
        }
        if (pacemaker.hasPacemakerBuff)
        {
            AudioManager.PlayAudioAsset(AudioManager.ClipsName.WHEELCHAIR_RUN, null);
        }
    }
}
