using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [HideInInspector] public PlayerController playerController;
    [SerializeField] public float movementSpeed = 1f;
    [SerializeField] public float secondsBeforeDecrease = 1f;
    [Tooltip("Speed decrease value < 1.")]
    [SerializeField] public float speedDecreaseValue_1 = 0.015f;
    [Tooltip("Speed decrease value between 1 and 2.")]
    [SerializeField] public float speedDecreaseValue_2 = 0.05f;
    [Tooltip("Speed decrease value between 2 and 3.")]
    [SerializeField] public float speedDecreaseValue_3 = 0.075f;
    [Tooltip("Speed decrease value between 3 and 4.")]
    [SerializeField] public float speedDecreaseValue_4 = 0.125f;
    public bool hasPacemaker, hasOxygen, hasCachet;
    public float minSpeedValue = 0f, maxSpeedValue = 4f;
    private bool isRunning;
    [SerializeField] public float value_1, value_2, value_3, value_4;

    [HideInInspector] public GameOverConditions gameOver;
    [HideInInspector] public Oxygen oxygen;
    [HideInInspector] public Pacemaker pacemaker;
    [HideInInspector] public Cachet cachet;
   
    void Start()
    {
        // Le joueur d�marre avec un cachet
        hasCachet = true;
        isRunning = false;

        playerController = GetComponent<PlayerController>();
        gameOver = GetComponent<GameOverConditions>();
        oxygen = GetComponent<Oxygen>();
        pacemaker = GetComponent<Pacemaker>();
        cachet = GetComponent<Cachet>();

        StartCoroutine(DecreaseSpeedOvertime());
    }

    void Update()
    {
        playerController.forwardSpeed = movementSpeed;
        //StartCoroutine(PlayerAudio());
    }
    IEnumerator DecreaseSpeedOvertime()
    {
        if (!gameOver.hasLost)
        {
            yield return new WaitForSeconds(secondsBeforeDecrease);

            if (playerController.forwardSpeed < value_1)
            {
                movementSpeed = Mathf.Clamp(movementSpeed - speedDecreaseValue_1, minSpeedValue, maxSpeedValue);
            }
            else if (playerController.forwardSpeed >= value_1 && playerController.forwardSpeed <= value_2)
            {
                movementSpeed = Mathf.Clamp(movementSpeed - speedDecreaseValue_2, minSpeedValue, maxSpeedValue);
            }
            else if (playerController.forwardSpeed >= value_2 && playerController.forwardSpeed <= value_3)
            {
                movementSpeed = Mathf.Clamp(movementSpeed - speedDecreaseValue_3, minSpeedValue, maxSpeedValue);
            }
            else if (playerController.forwardSpeed >= value_3 && playerController.forwardSpeed <= value_4)
            {
                movementSpeed = Mathf.Clamp(movementSpeed - speedDecreaseValue_4, minSpeedValue, maxSpeedValue);
            }
            gameOver.CheckPlayerMovementSpeed();
            StartCoroutine(DecreaseSpeedOvertime());
        }
    }
    IEnumerator PlayerAudio()
    {
        if (playerController.forwardSpeed >= value_2)
        {
            AudioManager.StopPlayAudioAsset(AudioManager.ClipsName.WHEELCHAIR_WALK, null);
            yield return new WaitForSeconds(1f);
            if (!isRunning)
            {
                isRunning = true;
                AudioManager.PlayAudioAsset(AudioManager.ClipsName.WHEELCHAIR_RUN, null);
            }
            StartCoroutine(PlayerAudio());
        }
        else 
        {

            AudioManager.StopPlayAudioAsset(AudioManager.ClipsName.WHEELCHAIR_RUN, null);
            yield return new WaitForSeconds(1f);
            if (isRunning)
            {
                isRunning = false;
                AudioManager.PlayAudioAsset(AudioManager.ClipsName.WHEELCHAIR_WALK, null);
            }
            StartCoroutine(PlayerAudio());
        }
    }
}
