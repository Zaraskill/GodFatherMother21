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

    private Oxygen oxygen;
    private Pacemaker pacemaker;
    private Cachet cachet;
   
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gameOver = GetComponent<GameOverConditions>();
        oxygen = GetComponent<Oxygen>();
        pacemaker = GetComponent<Pacemaker>();
        cachet = GetComponent<Cachet>();

        StartCoroutine(DecreaseHealthOvertime());
    }

    void Update()
    {
        playerController.forwardSpeed = movementSpeed;
        Debug.Log($"Current Player Speed: "+playerController.forwardSpeed);
        PlayCorrespondingSound();
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
