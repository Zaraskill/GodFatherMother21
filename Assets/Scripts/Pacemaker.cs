using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Speeds up the player's character movement speed for a limited period of time. Unstoppable
public class Pacemaker : MonoBehaviour
{
    private PlayerStats playerStats;
    private Oxygen oxygen;
    [Header("Pacemaker Properties")]
    [SerializeField] public float buffTimer = 5f;
    [SerializeField] public float movementSpeedBoost = 0.3f;
    private float resetTimer = 5f;
    [HideInInspector] public bool hasPacemakerBuff;
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        oxygen = GetComponent<Oxygen>();
    }

    void Update()
    {
        if (playerStats.hasPacemaker) Debug.Log("Pacemaker Available");

        if (playerStats.hasPacemaker && !hasPacemakerBuff)
        {
            // Si aucun autre buff est en cours
            if (!oxygen.hasOxygenBuff)
            ActivateMovementSpeedBoost();
        }
    }
 
    void ActivateMovementSpeedBoost()
    {
        Debug.Log("Pacemaker Used !");
        StartCoroutine(StartChrono());
    }
    IEnumerator StartChrono()
    {
        hasPacemakerBuff = true;
        yield return new WaitForSeconds(1f);

        if (buffTimer != 0)
        {
            buffTimer--;
            playerStats.movementSpeed = Mathf.Clamp(playerStats.movementSpeed + movementSpeedBoost, playerStats.minSpeedValue, playerStats.maxSpeedValue);
            StartCoroutine(StartChrono());
        }
        else
        {
            // Ramener la vitesse du joueur à la normale lorsque le chrono arrive à 0
            Debug.Log("Pacemaker Faided !");
            playerStats.hasPacemaker = false;
            hasPacemakerBuff = false;
            buffTimer = resetTimer;
        }
    }
}