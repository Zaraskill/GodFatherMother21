using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private GameObject PointC;
    private GameObject PointA;
    private GameObject PointB;
    private PlayerStats playerStats;

    public float movementSpeedThresholdToPointA = 2f;
    public float movementSpeedThresholdToPointB = 5f;
    public float movementSpeedThresholdToPointC = 10f;

    public float nurseMovementSpeedToPointA = 5f;
    public float nurseMovementSpeedToPointB = 5f;
    public float nurseMovementSpeedToPointC = 5f;

    // Start is called before the first frame update
    void Start()
    {
        PointC = GameObject.FindGameObjectWithTag("PointC");
        PointA = GameObject.FindGameObjectWithTag("PointA");
        PointB = GameObject.FindGameObjectWithTag("PointB");

        playerStats = GetComponentInParent<PlayerStats>();
        
        transform.position = PointC.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Se rapproche très dangereusement du joueur
        if (playerStats.movementSpeed <= movementSpeedThresholdToPointA)
        {
            Debug.Log("Getting Close To Point A");
            transform.position = Vector3.Lerp(transform.position, PointA.transform.position, nurseMovementSpeedToPointA * Time.deltaTime);
        }
        // Se rapproche modéremment du joueur
        if (playerStats.movementSpeed > movementSpeedThresholdToPointA && playerStats.movementSpeed <= movementSpeedThresholdToPointB)
        {
            Debug.Log("Getting Close To Point B");
            transform.position = Vector3.Lerp(transform.position, PointB.transform.position, nurseMovementSpeedToPointB * Time.deltaTime);
        }
        // Rester très en arrière du joueur
        if (playerStats.movementSpeed > movementSpeedThresholdToPointA)
        {
            Debug.Log("Getting Close To Point C");
            transform.position = Vector3.Lerp(transform.position, PointC.transform.position, nurseMovementSpeedToPointC * Time.deltaTime);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DEAD !");
            SceneManager.LoadScene("LD");
        }
    }
}
