using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Speeds up the player's character movement speed for a limited period of time. Stoppable
public class Oxygen : MonoBehaviour
{
    private PlayerStats playerStats;
    private Pacemaker pacemaker;
    [Header("Oxygen Properties")]
    [SerializeField] private int playerID = 0;
    [SerializeField] public float oxygenCapacity = 15f;
    [SerializeField] public float movementSpeedBoost = 15f;
    private Player playerEntity;
    private float initialPlayerMovementSpeed;
    public bool hasOxygenBuff;
    void Start()
    {
        playerEntity = ReInput.players.GetPlayer(playerID);
        playerStats = GetComponent<PlayerStats>();
        pacemaker = GetComponent<Pacemaker>();

        initialPlayerMovementSpeed = playerStats.movementSpeed;
    }

    void Update()
    {
        if (playerStats.hasOxygen) Debug.Log("Oxygen Available");
        // Si le joueur obtient de l'oxygène et n'est pas buff
        if (playerStats.hasOxygen && !pacemaker.hasPacemakerBuff)
        {
            ActivateMovementSpeedBoost();
        }
    }

    void ActivateMovementSpeedBoost()
    {
        // Si on maintient la touche
        if (playerEntity.GetAxis("ItemOne") == 1)
        {
            hasOxygenBuff = true;
            oxygenCapacity--;
            Debug.Log(oxygenCapacity);

            if (!CheckOxygenCapacity()) return;

            playerStats.movementSpeed = Mathf.Clamp(playerStats.movementSpeed + movementSpeedBoost, playerStats.minSpeedValue, playerStats.maxSpeedValue);
        }
        else
        {
            hasOxygenBuff = false;
            if (playerStats.movementSpeed > initialPlayerMovementSpeed)
            {
                DeactivateMovementSpeedBoost();
            }
        }
    }
    void DeactivateMovementSpeedBoost()
    {
        playerStats.movementSpeed = initialPlayerMovementSpeed;
    }

    bool CheckOxygenCapacity()
    {
        if (oxygenCapacity == 0)
        {
            hasOxygenBuff = false;
            Debug.Log("Oxygen Unavailable");
        }
        return (hasOxygenBuff);
    }
}
