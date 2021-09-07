using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{

    public float forwardSpeed = 2f;


    [SerializeField] private int playerID = 0;
    [SerializeField] private Player  playerEntity;
    private bool canTurn = false;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerEntity = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canTurn)
        {
            float rotation = playerEntity.GetAxis("Turn");
            if(-1==rotation  || rotation==1 )
            {
                Debug.Log("Tourne");
                gameObject.transform.Rotate(new Vector3(0,90,0)*rotation);
                canTurn = false;
            }
        }

        _rigidbody.velocity = gameObject.transform.forward * forwardSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("TurnZone"))
        {
            Debug.Log("entre");
            canTurn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("TurnZone"))
        {
            canTurn = false;
        }
    }
}
