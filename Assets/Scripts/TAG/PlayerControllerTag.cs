using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControllerTag : MonoBehaviour
{
    public bool isPlayer1;

    private Rigidbody rb;

    //public HealthBar healthbar;

    private string getAxisHorizontal = "Horizontal";
    private string getAxisVertical = "Vertical";
    private string getWallAxisHor;
    private string getWallAxisVert;


    private float speed = 20f;
    private Vector3 moveDirectionWithSpeed;
    private Vector3 moveDirectionWithSpeedJoystick;
    private Vector3 combinedMoveDirectionWithSpeed;

    private FixedJoystick joystick;

    private string winningPlayer;
    public Text winnerText;
    public winningScreen winningScreen;

    public GameObject carryFlag;
    public GameObject flag;
    public bool isCarryingFlag;
    public bool flagIsDropped;

    private float overlapColliderRange = 2.5f;


    void Start()
    {

        //Set variables to player1 or player2
        if (gameObject.tag == "Playa")
        {
            isPlayer1 = true;
            getAxisHorizontal = "Horizontal";
            getAxisVertical = "Vertical";

            getWallAxisHor = "HorizontalWall";
            getWallAxisVert = "VerticalWall";

            joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();

            carryFlag = GameObject.FindWithTag("Carry Flag");

        }
        else if (gameObject.tag == "Player2")
        {
            isPlayer1 = false;
            getAxisHorizontal = "Horizontal2";
            getAxisVertical = "Vertical2";

            getWallAxisHor = "HorizontalWall2";
            getWallAxisVert = "VerticalWall2";

            joystick = GameObject.FindWithTag("Joystick2").GetComponent<FixedJoystick>();

            carryFlag = GameObject.FindWithTag("Carry Flag 2");
        }

        carryFlag.SetActive(false);
        isCarryingFlag = false;
        flagIsDropped = false;

        rb = GetComponent<Rigidbody>();

        winningScreen.gameObject.SetActive(false);
        winnerText.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis(getAxisHorizontal), 0.0f, Input.GetAxis(getAxisVertical));
        moveDirectionWithSpeed = moveDirection * speed;

        Vector3 moveDirectionJoystick = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
        moveDirectionWithSpeedJoystick = moveDirectionJoystick * speed;

        //this decides if the player is on computer keyboard or mobile device
        if (moveDirection != Vector3.zero)
        {
            combinedMoveDirectionWithSpeed = moveDirectionWithSpeed;
        }
        else
        {
            combinedMoveDirectionWithSpeed = moveDirectionWithSpeedJoystick;
            moveDirection = moveDirectionJoystick;

        }
        //move the player
        rb.velocity = combinedMoveDirectionWithSpeed;

        //rotate the player when it turns
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection.normalized), 0.5f);
        }


        if ((Input.GetKeyDown(KeyCode.C) && isPlayer1) || (Input.GetKeyDown(KeyCode.L) && !isPlayer1))
        {
            isCarryingFlag = false;
            flagIsDropped = true;
            carryFlag.SetActive(false);
        }


        Collider[] hits = Physics.OverlapSphere(transform.position, overlapColliderRange);
        foreach (Collider hit in hits)
        {
            if ((hit.tag == "Player2" && isCarryingFlag && isPlayer1) || (hit.tag == "Playa" && isCarryingFlag && !isPlayer1))
            {
                isCarryingFlag = false;
                flagIsDropped = true;
                carryFlag.SetActive(false);
                Debug.Log("TAGGED!!! DROP THE GEM!");
            }
        }
    }


    //here we can add items to inventory when entering a pickup area MAYBE
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flag2" && isPlayer1)
        {
            Debug.Log("Red player has the flag!");
            Destroy(other.gameObject);
            carryFlag.SetActive(true);
            isCarryingFlag = true;
        }
        else if (other.tag == "Flag1" && !isPlayer1)
        {
            Debug.Log("Blue player has the flag!");
            Destroy(other.gameObject);
            carryFlag.SetActive(true);
            isCarryingFlag = true;
        }

        if (other.tag == "Flag1" && isCarryingFlag && isPlayer1 || other.tag == "End Zone" && isCarryingFlag && isPlayer1)
        {
            Debug.Log("Red player wins!");
            winningPlayer = "RED PLAYER ";
            setWinnerText();
            winningScreen.gameObject.SetActive(true);

        }
        else if (other.tag == "Flag2" && isCarryingFlag && !isPlayer1 || other.tag == "End Zone 2" && isCarryingFlag && !isPlayer1)
        {
            Debug.Log("Blue player wins!");
            winningPlayer = "BLUE PLAYER ";
            setWinnerText();
            winningScreen.gameObject.SetActive(true);

        }
    }

    private void setWinnerText()
    {
        winnerText.text = winningPlayer + "WINS!";
    }
}
