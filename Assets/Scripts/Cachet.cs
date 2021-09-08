using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cachet : MonoBehaviour
{
    // Restore the character's life
    private PlayerStats playerStats;
    [Header("Cachet Properties")]
    [SerializeField] public float healingAmount = 5f;
    private float minValue = 0f, maxValue = 100f;
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (playerStats.hasCachet)
        {
            AudioManager.PlayAudioAsset(AudioManager.ClipsName.CACHET, null);
            HealPlayer();
        }
    }
    void HealPlayer()
    {
        playerStats.health = Mathf.Clamp(playerStats.health + healingAmount, minValue, maxValue);
        playerStats.hasCachet = false;
    }
}
