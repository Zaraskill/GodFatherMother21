using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Speeds up the player's character movement speed for a limited period of time. Stoppable
public class Oxygen : MonoBehaviour
{
    private PlayerStats playerStats;
    private Pacemaker pacemaker;
    private Player playerEntity;
    [Header("Oxygen Properties")]
    [SerializeField] private int playerID = 0;
    [SerializeField] public float oxygenCapacity = 8f;
    [SerializeField] public float movementSpeedBoost = 0.3f;
    [SerializeField] public float oxygenCapacityDecreaseValue = 1f;
    [SerializeField] bool refillOxygenCapacity;
    private float initialOxygenCapacity;
    public bool hasOxygenBuff;
    void Start()
    {
        initialOxygenCapacity = oxygenCapacity;
        playerEntity = ReInput.players.GetPlayer(playerID);
        playerStats = GetComponent<PlayerStats>();
        pacemaker = GetComponent<Pacemaker>();
    }

    void Update()
    {
        if (playerStats.hasOxygen) Debug.Log("Oxygen Available");
        if (refillOxygenCapacity) {
            oxygenCapacity = initialOxygenCapacity;
            refillOxygenCapacity = false;
        }
        if (!hasOxygenBuff && CheckOxygenCapacity() && !pacemaker.hasPacemakerBuff)
        StartCoroutine(ActivateMovementSpeedBoost());
    }

    IEnumerator ActivateMovementSpeedBoost()
    {
        while (true)
        {
            // Si le joueur obtient de l'oxygène et n'est pas buff
            if (playerStats.hasOxygen)
            {
                // Si on maintient la touche
                if (playerEntity.GetAxis("ItemOne") == 1)
                {
                    if (CheckOxygenCapacity())
                    {
                        if(!hasOxygenBuff) AudioManager.PlayAudioAsset(AudioManager.ClipsName.OXYGEN_OPENING, null);
                        Debug.Log("Oxygen Used !");

                        oxygenCapacity--;
                        Debug.Log("Number of Oxygen stacks available: "+oxygenCapacity);
                        hasOxygenBuff = true;
                        playerStats.movementSpeed = Mathf.Clamp(playerStats.movementSpeed + movementSpeedBoost, playerStats.minSpeedValue, playerStats.maxSpeedValue);
                    }
                    else
                    {
                        DisableOxygen();
                        break;
                    }
                }
                else
                {
                    DisableOxygen();
                    break;
                }
            }
            else
            {
                DisableOxygen();
                break;
            }
            yield return new WaitForSeconds(oxygenCapacityDecreaseValue);
        }
    }

    void DisableOxygen()
    {
        if (!CheckOxygenCapacity())
        {
            playerStats.hasOxygen = false;
            Debug.Log("Oxygen Faided !");
            AudioManager.PlayAudioAsset(AudioManager.ClipsName.OXYGEN_CLOSING, null);
        }
        AudioManager.StopPlayAudioAsset(AudioManager.ClipsName.OXYGEN_OPENING, null);
        hasOxygenBuff = false;
    }
    bool CheckOxygenCapacity()
    {
        if (oxygenCapacity == 0)
        {
            return (false);
        }
        return (true);
    }
}
