using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : PlayerController
{
    protected override void Start()
    {
        base.Start();

        isPlayer1 = true;
        getAxisHorizontal = "Horizontal";
        getAxisVertical = "Vertical";
        getWallAxisHor = "HorizontalWall";
        getWallAxisVert = "VerticalWall";

        joystick = GameObject.FindWithTag("Joystick").GetComponent<DynamicJoystick>();
        wallJoystick = GameObject.FindWithTag("wallJoystick").GetComponent<FixedJoystick>();
        GameObject.FindWithTag("Joybutton").GetComponent<Button>().onClick.AddListener(DeployBomb);
        carryFlag = GameObject.FindWithTag("Carry Flag");
        otherPlayerScript = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
    }

    protected override void HandleBombAndWallDeployment()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bombCount > 0 && isBombCooledDown)
        {
            DeployBomb();
        }

        if (Input.GetKeyDown(KeyCode.V) && wallCount > 0)
        {
            DeployWall();
        }
    }

    protected override void HandleFlagDrop()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            DropFlag();
        }
    }

    protected override void HandleFlagPickup(Collider other)
    {
        if (other.tag == "Flag2" && !isTagged && !otherPlayerScript.isCarryingFlag)
        {
            Debug.Log("Red player has the flag!");
            Destroy(other.gameObject);
            carryFlag.SetActive(true);
            isCarryingFlag = true;
            setWarningText("have flag");
        }
    }

    protected override void CheckWinCondition(Collider other)
    {
        if ((other.tag == "Flag1" || other.tag == "End Zone") && isCarryingFlag)
        {
            Debug.Log("Red player wins!");
            winningPlayer = "RED PLAYER ";
            setWinnerText();
            winningScreen.gameObject.SetActive(true);
        }
    }
}
