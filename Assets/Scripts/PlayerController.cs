using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{

    private enum InputQTE { QTEA, QTEB, QTEC, QTED };


    public float forwardSpeed = 2f;
    [SerializeField] private int playerID = 0;
    [SerializeField] private Player  playerEntity;
    private bool canTurn = false;
    private bool isInQTE = false;
    private Rigidbody _rigidbody;
    private InputQTE goodInput;
    private bool isQTESuccess = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerEntity = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInQTE)
        {
            if(playerEntity.GetAxis("QTEA") < 0 && goodInput == InputQTE.QTEA)
            {
                Debug.Log("Bouton A");
                isQTESuccess = true;
            }
            else if (playerEntity.GetAxis("QTEB") < 0 && goodInput == InputQTE.QTEB)
            {
                Debug.Log("Bouton b");
                isQTESuccess = true;
            }
            else if (playerEntity.GetAxis("QTEC") < 0 && goodInput == InputQTE.QTEC)
            {
                Debug.Log("Bouton x");
                isQTESuccess = true;
            }
            else if (playerEntity.GetAxis("QTED") < 0 && goodInput == InputQTE.QTED)
            {
                Debug.Log("Bouton y");
                isQTESuccess = true;
            }


            if (isQTESuccess)
            {
                isInQTE = false;
                Debug.Log("QTE");
            }
        }
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
            canTurn = true;
        }
        if (other.tag.Equals("OldPeople"))
        {
            isInQTE = true;
            StartQTE();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("TurnZone"))
        {
            canTurn = false;
        }
        if (other.tag.Equals("OldPeople"))
        {
            isInQTE = false;
        }
    }

    private void StartQTE()
    {
        int random = Random.Range(0, 4);
        goodInput = (InputQTE) random;
        Debug.Log(goodInput);
    }
}
