using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverConditions : MonoBehaviour
{
    private PlayerStats playerStats;
    public bool hasLost;
    void Start()
    {
        //hasLost = false;
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        CheckPlayerMovementSpeed();
    }
    public void CheckPlayerMovementSpeed()
    {
        if (playerStats.movementSpeed == 0)
        {
            hasLost = true;
            //Debug.Log($"GameOver: hasLost");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        hasLost = true;
        //Debug.Log($"GameOver (col) : hasLost");
    }

}
