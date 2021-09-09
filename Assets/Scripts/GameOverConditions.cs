using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverConditions : MonoBehaviour
{
    private PlayerStats playerStats;
    public bool hasLost;
    public bool playOnce;
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playOnce = false;
    }

    void Update()
    {
        if (hasLost && !playOnce)
        {
            playerStats.movementSpeed = 0f;
            playOnce = true;
            AudioManager.StopMusic();
            AudioManager.PlayAudioAsset(AudioManager.ClipsName.GAMEOVER, null);
        }
        CheckPlayerMovementSpeed();
    }
    public void CheckPlayerMovementSpeed()
    {
        if (playerStats.movementSpeed == 0)
        {
            hasLost = true;
        }
    }
}
