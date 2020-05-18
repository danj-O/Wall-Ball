using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player2Controller : MonoBehaviour
{
    private Rigidbody rb;
    private Button bombButton;
    private Button wallButton;

    //public HealthBar healthbar;

    private string getAxisHorizontal = "Horizontal2";
    private string getAxisVertical = "Vertical2";

    public float speed = 20;
    private Vector3 moveDirectionWithSpeed;
    private Vector3 moveDirectionWithSpeedJoystick;
    private Vector3 combinedMoveDirectionWithSpeed;

    private FixedJoystick joystick;

    public GameObject bomb;
    public GameObject wall;
    public BombStore bombStore;
    public WallStore wallStore;

    //public int maxHealth = 100;
    //public int currentHealth;
    private int wallCount;
    private int bombCount;

    public float bombCooldown;
    private float bombCooldownDefault = 0f;
    private bool isBombCooledDown = true;

    public Text wallCountText;
    public Text bombCountText;

    public Text WinnerText;

    public GameObject carryFlag;
    public GameObject flag;
    public bool isCarryingFlag;
    public bool flagIsDropped;

    private float overlapColliderRange = 2.5f;


    void Start()
    {
        //currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);

        carryFlag = GameObject.FindWithTag("Carry Flag 2");
        carryFlag.SetActive(false);
        isCarryingFlag = false;
        flagIsDropped = false;

        rb = GetComponent<Rigidbody>();
        joystick = GameObject.FindWithTag("Joystick2").GetComponent<FixedJoystick>();

        GameObject.FindWithTag("Joybutton2").GetComponent<Button>().onClick.AddListener(DeployBomb);
        GameObject.FindWithTag("JoybuttonWall2").GetComponent<Button>().onClick.AddListener(DeployWall);

        wallCount = 3;
        bombCount = 6;

        SetWallCountText();
        SetBombCountText();
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


        //DROPPING PREFABS(WALLS AND BOMBS) SECTION

        //bomb cooldown conditional
        if (bombCooldown > 0 && !isBombCooledDown)
        {
            bombCooldown -= Time.deltaTime;
        }
        else if (bombCooldown <= 0)
        {
            isBombCooledDown = true;
        }

        if (Input.GetKeyDown(KeyCode.Period) && bombCount > 0 && isBombCooledDown)
        {
            //what to spawn, where to spawn, which direction to spawn in
            Instantiate(bomb, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z) + (transform.forward * 2), transform.rotation);
            bombCount -= 1;
            SetBombCountText();
            bombCooldown = bombCooldownDefault;
            isBombCooledDown = false;
        }
        else if (Input.GetKeyDown(KeyCode.Period) && bombCount == 0)
        {
            Debug.Log("Player out of bombs");
            SetBombCountText();
        }

        if (Input.GetKeyDown(KeyCode.Comma) && wallCount > 0)
        {
            Instantiate(wall, transform.position + (transform.forward * 2), transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            wallCount -= 1;
            SetWallCountText();
        }
        else if (Input.GetKeyDown(KeyCode.Comma) && wallCount == 0)
        {
            Debug.Log("Player 1 out of walls");
            SetWallCountText();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            isCarryingFlag = false;
            flagIsDropped = true;
            carryFlag.SetActive(false);
            //GameObject.FindWithTag("Flag2").SetActive(true);
        }
        Collider[] hits = Physics.OverlapSphere(transform.position, overlapColliderRange);
        foreach (Collider hit in hits)
        {
            if (hit.tag == "Playa" && isCarryingFlag)
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
        if (other.tag == "Wall Store")
        {
            if (other.GetComponent<WallStore>().inventory <= 0)
            {
                Debug.Log("Wall store has no walls!");
            }
            else if (other.GetComponent<WallStore>().inventory > 0)
            {
                other.GetComponent<WallStore>().inventory -= 1;
                Debug.Log(other.GetComponent<WallStore>().inventory);
                wallCount += 1;
                SetWallCountText();
            }
        }
        if (other.tag == "Bomb Store")
        {
            if (other.GetComponent<BombStore>().inventory <= 0)
            {
                Debug.Log("Bomb store has no bombs!");
            }
            else if (other.GetComponent<BombStore>().inventory > 0)
            {
                other.GetComponent<BombStore>().inventory -= 1;
                Debug.Log(other.GetComponent<BombStore>().inventory);
                bombCount += 1;
                SetBombCountText();
            }
        }
        if (other.tag == "Flag1")
        {
            Destroy(other.gameObject);
            carryFlag.SetActive(true);
            isCarryingFlag = true;
        }
        if (other.tag == "Flag2" && isCarryingFlag)
        {
            Debug.Log("Blue player wins!");
            //restarts the game--should change this to option menu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //functions to instantiate bombs and walls
    private void DeployBomb()
    {
        if (bombCount > 0)
        {
            Instantiate(bomb, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z) + (transform.forward * 2), transform.rotation);
            bombCount -= 1;
            SetBombCountText();

        }
        else if (bombCount == 0)
        {
            Debug.Log("Player out of bombs");
            SetBombCountText();
        }
    }
    private void DeployWall()
    {
        if (wallCount > 0)
        {
            Instantiate(wall, transform.position + (transform.forward * 2), transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            wallCount -= 1;
            SetWallCountText();
        }
        else if (wallCount == 0)
        {
            Debug.Log("Player out of walls");
            SetWallCountText();
        }

    }

    //functions to set player UI for bombs and walls
    private void SetWallCountText()
    {
        wallCountText.text = "Walls: " + wallCount.ToString();
    }
    private void SetBombCountText()
    {
        bombCountText.text = "Bombs: " + bombCount.ToString();
    }
}
