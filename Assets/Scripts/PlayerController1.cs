using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using TMPro;


public class PlayerController1 : MonoBehaviour
{
    public float gameTimer;
    private Gyroscope m_Gyro;
    private Vector3 shakoMeter;

    public bool isPlayer1;

    private PlayerController1 otherPlayerScript;

    private Rigidbody rb;
    private Button bombButton;
    private Button wallButton;

    //public HealthBar healthbar;

    private string getAxisHorizontal = "Horizontal";
    private string getAxisVertical = "Vertical";
    private string getWallAxisHor = "HorizontalWall";
    private string getWallAxisVert = "VerticalWall";
    //private string fire1;
    //private string fire2;



    private float speed = 20f;
    private Vector3 moveDirectionWithSpeed;
    private Vector3 moveDirectionWithSpeedJoystick;
    private Vector3 combinedMoveDirectionWithSpeed;

    private DynamicJoystick joystick;
    private FixedJoystick wallJoystick;
    private Vector3 wallDirectionJoystick;
    private bool hasBuiltWall = false;

    public GameObject bomb;
    public GameObject wall;
    public GameObject wallTemp;
    private Rigidbody wallTempRb;
    private bool tempWallSpawned = false;

    public BombStore bombStore;
    public WallStore wallStore;

    //public int maxHealth = 100;
    //public int currentHealth;
    public int wallCount;
    public int bombCount;

    public float bombCooldown;
    private float bombCooldownDefault = 0f;
    private bool isBombCooledDown = true;
    //public float distanceFromObj;
    private float bombRespawnTimer;

    public Text wallCountText;
    public GameObject wallBombTextBox;
    public Text bombCountText;
    public GameObject warningTextBox;
    public Text warningText;

    private string winningPlayer;
    public Text winnerText;
    public winningScreen winningScreen;

    public GameObject carryFlag;
    public GameObject flag;
    public bool isCarryingFlag;
    public bool flagIsDropped;
    public bool isTagged;

    private float overlapColliderRange = 1.2f;

    public bool inDangerZone;


    void Start()
    {
        //currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);

        //Set variables to player1 or player2
        if (gameObject.tag == "Playa")
        {
            isPlayer1 = true;
            isTagged = false;
            getAxisHorizontal = "Horizontal";
            getAxisVertical = "Vertical";

            getWallAxisHor = "HorizontalWall";
            getWallAxisVert = "VerticalWall";

            //for movement
            joystick = GameObject.FindWithTag("Joystick").GetComponent<DynamicJoystick>();
            //for wall spawn positioning
            wallJoystick = GameObject.FindWithTag("wallJoystick").GetComponent<FixedJoystick>();
            //listener for 
            //GameObject.FindWithTag("wallJoystick").GetComponent<Button>().onClick.AddListener(DeployWall);

            GameObject.FindWithTag("Joybutton").GetComponent<Button>().onClick.AddListener(DeployBomb);
            //GameObject.FindWithTag("JoybuttonWall").GetComponent<Button>().onClick.AddListener(DeployWall);
            carryFlag = GameObject.FindWithTag("Carry Flag");
            otherPlayerScript = GameObject.FindWithTag("Player2").GetComponent<PlayerController1>();


}
        else if (gameObject.tag == "Player2")
        {
            isPlayer1 = false;
            getAxisHorizontal = "Horizontal2";
            getAxisVertical = "Vertical2";

            getWallAxisHor = "HorizontalWall2";
            getWallAxisVert = "VerticalWall2";

            joystick = GameObject.FindWithTag("Joystick2").GetComponent<DynamicJoystick>();
            wallJoystick = GameObject.FindWithTag("wallJoystick2").GetComponent<FixedJoystick>();
            //GameObject.FindWithTag("wallJoystick2").GetComponent<Button>().onClick.AddListener(DeployWall);

            GameObject.FindWithTag("Joybutton2").GetComponent<Button>().onClick.AddListener(DeployBomb);
            //GameObject.FindWithTag("JoybuttonWall2").GetComponent<Button>().onClick.AddListener(DeployWall);
            carryFlag = GameObject.FindWithTag("Carry Flag 2");
            otherPlayerScript = GameObject.FindWithTag("Playa").GetComponent<PlayerController1>();

        }

        //warningText = warningTextBox.GetComponent<Text>();

        carryFlag.SetActive(false);
        isCarryingFlag = false;
        flagIsDropped = false;

        rb = GetComponent<Rigidbody>();

        wallCount = 10;
        bombCount = 6;

        //SetWallCountText();
        wallCountText.text = "Walls: " + wallCount.ToString();
        SetBombCountText();

        winningScreen.gameObject.SetActive(false);
        winnerText.text = "";

        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        gameTimer += Time.deltaTime;
        bombRespawnTimer += Time.deltaTime;
        //Debug.Log(gameTimer);
        if (bombRespawnTimer > 10)
        {
            bombCount += 1;
            SetBombCountText();
            bombRespawnTimer = 0;
        }


        //Debug.Log(Input.gyro.rotationRateUnbiased);
        shakoMeter = Input.gyro.rotationRateUnbiased;
        if (shakoMeter.x > 3f || shakoMeter.y > 3f || shakoMeter.z > 2.5f || shakoMeter.x < -3f || shakoMeter.y < -3f || shakoMeter.z < -2.5f)
        {
            Debug.Log("YOU SHAKING!!!!!\nSHAKKINGGG\nSHAKKEEINGG");
        }



        Vector3 moveDirection = new Vector3(Input.GetAxis(getAxisHorizontal), 0.0f, Input.GetAxis(getAxisVertical));
        moveDirectionWithSpeed = moveDirection * speed;

        Vector3 moveDirectionJoystick = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
        moveDirectionWithSpeedJoystick = moveDirectionJoystick * speed;

        wallDirectionJoystick = new Vector3(wallJoystick.Horizontal, 0.0f, wallJoystick.Vertical);


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

        //RaycastHit theHit;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out theHit))
        //{
        //    if (theHit.collider.gameObject.tag == "Wall")
        //    {
        //        Debug.Log("WALLLL!!!!");
        //        distanceFromObj = theHit.distance * 100;
        //    }
        //}


        //DROPPING PREFABS(WALLS AND BOMBS) SECTION

        //bomb cooldown conditional
        if (bombCooldown > 0 && !isBombCooledDown)
        {
            bombCooldown -= Time.deltaTime;
        }
        else if(bombCooldown <= 0)
        {
            isBombCooledDown = true;
        }

        if ((Input.GetKeyDown(KeyCode.Space) && isPlayer1 && bombCount > 0 && isBombCooledDown) || (Input.GetKeyDown(KeyCode.Period) && !isPlayer1 && bombCount > 0 && isBombCooledDown))
        {
            //what to spawn, where to spawn, which direction to spawn in
            Instantiate(bomb, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z) + (transform.forward * 2), transform.rotation);
            bombCount -= 1;
            SetBombCountText();
            bombCooldown = bombCooldownDefault;
            isBombCooledDown = false;
        }
        else if ((Input.GetKeyDown(KeyCode.Space) && isPlayer1 && bombCount == 0) || (Input.GetKeyDown(KeyCode.Period) && !isPlayer1 && bombCount == 0))
        {
            Debug.Log("Player out of bombs");
            SetBombCountText();
            setWarningText("bomb");
        }



        //WALL INSTANTIATE FOR KEYS
        if ((Input.GetKeyDown(KeyCode.V) && isPlayer1 && wallCount > 0) || (Input.GetKeyDown(KeyCode.Comma) && !isPlayer1 && wallCount > 0))
        {
            Instantiate(wall, transform.position + (transform.forward * 2), transform.rotation);
            wallCount -= 1;
            SetWallCountText();
        }
        else if ((Input.GetKeyDown(KeyCode.V) && isPlayer1 && wallCount == 0) || (Input.GetKeyDown(KeyCode.Comma) && !isPlayer1 && wallCount == 0))
        {
            Debug.Log("Player 1 out of walls");
            SetWallCountText();
            setWarningText("wall");
        }

        if ((Input.GetKeyDown(KeyCode.C) && isPlayer1) || (Input.GetKeyDown(KeyCode.L) && !isPlayer1))
        {
            isCarryingFlag = false;
            flagIsDropped = true;
            carryFlag.SetActive(false);
            //GameObject.FindWithTag("Flag2").SetActive(true);
        }



        Collider[] zones = Physics.OverlapSphere(transform.position, 0f);
        foreach (Collider zone in zones)
        {

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
                setWarningText("tagged with gem");
            }
            else if ((hit.tag == "Player2" && isPlayer1 && inDangerZone && !isTagged) || (hit.tag == "Playa" && !isPlayer1 && inDangerZone && !isTagged))
            {
                PlayerController1 taggedPlayerScript = hit.GetComponent<PlayerController1>();

                int wallPenalty = wallCount / 2;
                taggedPlayerScript.wallCount += wallPenalty;
                taggedPlayerScript.SetWallCountText();
                wallCount = wallPenalty;
                SetWallCountText();

                int bombPenalty = bombCount / 2;
                taggedPlayerScript.bombCount += bombPenalty;
                taggedPlayerScript.SetBombCountText();
                bombCount = bombPenalty;
                SetBombCountText();

                isTagged = true;
                Debug.Log("NICE ONE " + hit.tag + "! You took " + wallPenalty + " walls and " + bombPenalty + " bombs");
                setWarningText("tagged");

            }


        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall Store")
        {
            if (other.GetComponent<WallStore>().inventory <= 0)
            {
                Debug.Log("Wall store has no walls!");
                //setWarningText("wall store");

            }
            else if (other.GetComponent<WallStore>().inventory > 0)
            {
                other.GetComponent<WallStore>().inventory -= 1;
                //Debug.Log(other.GetComponent<WallStore>().inventory);
                wallCount += 1;
                SetWallCountText();
            }
        }
        if (other.tag == "Bomb Store")
        {
            if (other.GetComponent<BombStore>().inventory <= 0)
            {
                //setWarningText("bomb store");

            }
            else if (other.GetComponent<BombStore>().inventory > 0)
            {
                other.GetComponent<BombStore>().inventory -= 1;
                Debug.Log(other.GetComponent<BombStore>().inventory);
                bombCount += 1;
                SetBombCountText();
                Debug.Log("Pickup Bomb");

            }
        }

        if (other.tag == "Flag2" && isPlayer1 && !isTagged)
        {
            if (!otherPlayerScript.isCarryingFlag)
            {
                Debug.Log("Red player has the flag!");
                Destroy(other.gameObject);
                carryFlag.SetActive(true);
                isCarryingFlag = true;
                setWarningText("have flag");
            }
        }
        else if (other.tag == "Flag1" && !isPlayer1 && !isTagged)
        {
            if (!otherPlayerScript.isCarryingFlag)
            {
                Debug.Log("Blue player has the flag!");
                Destroy(other.gameObject);
                carryFlag.SetActive(true);
                isCarryingFlag = true;
                setWarningText("have flag");
            }

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

    //functions to instantiate bombs and walls
    private void DeployBomb()
    {
        if (bombCount > 0)
        {
            Instantiate(bomb, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z) + (transform.forward * 2), transform.rotation);
            bombCount -= 1;
            SetBombCountText();

        }
        else if (bombCount == 0)
        {
            Debug.Log("Player out of bombs");
            SetBombCountText();
            setWarningText("bomb");

        }
    }

    public void DeployWallTemp()
    {
        if (wallDirectionJoystick != Vector3.zero && !tempWallSpawned)
        {
            wallTemp.SetActive(true);
            wallTemp.transform.position= new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z) + (wallDirectionJoystick * 2);
            wallTemp.transform.rotation = Quaternion.LookRotation(wallDirectionJoystick);

        }
    }

    public void DeployWall()
    {
        if (wallCount > 0)
        {
            if (wallDirectionJoystick != Vector3.zero)
            {
                Instantiate(wall, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z) + (wallDirectionJoystick * 2), Quaternion.LookRotation(wallDirectionJoystick));
                wallCount -= 1;
                SetWallCountText();

                wallTemp.SetActive(false);

            }
        }
        else if (wallCount == 0)
        {
            Debug.Log("Player out of walls");
            SetWallCountText();
            setWarningText("wall");

            wallTemp.SetActive(false);
        }

    }

    //functions to set player UI for bombs and walls
    private void SetWallCountText()
    {
        wallCountText.text = "Walls: " + wallCount.ToString();
        wallBombTextBox.GetComponent<WallBombTextBoxScript>().anim();
    }
    private void SetBombCountText()
    {
        bombCountText.text = "Bombs: " + bombCount.ToString();
        wallBombTextBox.GetComponent<WallBombTextBoxScript>().anim();
    }

    private void setWinnerText()
    {
        winnerText.text = winningPlayer + "WINS!";
    }

    private void setWarningText(string textMod)
    {
        if (textMod == "wall")
        {
            warningText.text = "You are out of walls!";

        }
        if (textMod == "bomb")
        {
            warningText.text = "You are out of bombs!";
        }
        if (textMod == "tagged with gem" && isCarryingFlag)
        {
            warningText.text = "You've been tagged and dropped the gem!";
        }
        if (textMod == "tagged" && !isCarryingFlag)
        {
            warningText.text = "You've been tagged and lost stuff!";
        }
        if (textMod == "have flag")
        {
            warningText.text = "You have the flag! take it home!";
        }

        //tagged tagger wall bomb have flag
        warningTextBox.GetComponent<WarningTextBoxScript>().anim();
    }
}
