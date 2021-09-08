using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    private PlayerController playerController;
    [SerializeField] public float movementSpeed = 0f;
    [SerializeField] public float health = 0f;
    [SerializeField] public float healthDecreaseValue = 5f;
    [SerializeField] public float timerBeforeDecrease = 5f;
    public bool hasPacemaker, hasOxygen, hasCachet;
    private float minValue = 0f, maxValue = 100f;
    private GameOverConditions gameOver;
   
    void Start()
    {
        /*hasPacemaker = true;
        hasOxygen = true;
        hasCachet = false;
        isAlreadyBuffed = false;*/

        playerController = GetComponent<PlayerController>();
        gameOver = GetComponent<GameOverConditions>();

        StartCoroutine(DecreaseHealthOvertime());
    }

    void Update()
    {
        playerController.forwardSpeed = movementSpeed;
        Debug.Log($"Current Player Speed: "+playerController.forwardSpeed);
    }
    IEnumerator DecreaseHealthOvertime()
    {
        if (!gameOver.hasLost)
        {
            yield return new WaitForSeconds(timerBeforeDecrease);
            health = Mathf.Clamp(health - healthDecreaseValue, minValue, maxValue);
            //Debug.Log($"health: " + health);
            gameOver.CheckPlayerHealth();
            StartCoroutine(DecreaseHealthOvertime());
        }
        else
        {
            //Debug.Log($"Player has lost");
        }
    }
}
