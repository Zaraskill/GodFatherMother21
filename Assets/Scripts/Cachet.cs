using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cachet : MonoBehaviour
{
    // Restore the character's life
    private PlayerStats playerStats;
    private Player playerEntity;
    [Header("Cachet Properties")]
    [SerializeField] private int playerID = 0;
    [SerializeField] public float movementSpeedBoost = 10f;
    void Start()
    {
        playerEntity = ReInput.players.GetPlayer(playerID);
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (playerStats.hasCachet && playerEntity.GetAxis("ItemTwo") == 1)
        {
            AudioManager.PlayAudioAsset(AudioManager.ClipsName.CACHET, null);
            IncreasePlayerMovementSpeed();
        }
    }
    void IncreasePlayerMovementSpeed()
    {
        playerStats.movementSpeed = Mathf.Clamp(playerStats.movementSpeed + movementSpeedBoost, playerStats.minSpeedValue, playerStats.maxSpeedValue);
        playerStats.hasCachet = false;
    }
}
