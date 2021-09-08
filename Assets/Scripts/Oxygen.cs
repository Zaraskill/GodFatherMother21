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
    [SerializeField] public float buffTimer = 5f;
    [SerializeField] public float movementSpeedBoost = 2f;
    private Player playerEntity;
    private float initialPlayerMovementSpeed;
    private float resetTimer = 5f;
    public bool hasOxygenBuff;
    public bool hasChronoStarted;
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
        if (playerStats.hasOxygen && !hasOxygenBuff && !pacemaker.hasPacemakerBuff)
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
            playerStats.movementSpeed *= movementSpeedBoost;
            // On lance le chrono
            if (!hasChronoStarted)
            {
                hasChronoStarted = true;
                StartCoroutine(StartChrono());
            }
        }
    }
    void DeactivateMovementSpeedBoost()
    {
        playerStats.movementSpeed = initialPlayerMovementSpeed;
    }

    IEnumerator StartChrono()
    {

        if (playerEntity.GetAxis("ItemOne") != 1)
        {
            hasOxygenBuff = false;
            // Sinon si et Si aucun autre buff est en cours
            if (!pacemaker.hasPacemakerBuff && !hasOxygenBuff)
            {
                DeactivateMovementSpeedBoost();
            }
        }
        else
        {
            Debug.Log("Oxygen Used !");
            hasOxygenBuff = true;
            playerStats.movementSpeed = initialPlayerMovementSpeed;
            playerStats.movementSpeed *= movementSpeedBoost;
        }
       
        //hasChronoStarted = true;
        yield return new WaitForSeconds(1f);

        if (buffTimer != 0)
        {
            buffTimer--;
            StartCoroutine(StartChrono());
        }
        else
        {
            // Ramener la vitesse du joueur à la normale lorsque le chrono arrive à 0
            Debug.Log("Oxygen : Movement Speed Bonus Faiding");
            DeactivateMovementSpeedBoost();
            playerStats.hasOxygen = false;
            hasOxygenBuff = false;
            buffTimer = resetTimer;
            hasChronoStarted = false;
        }
    }
}
