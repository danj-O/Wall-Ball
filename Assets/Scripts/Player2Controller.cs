using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : PlayerController
{
    protected override void Start()
    {
        base.Start();

        isPlayer1 = false;
        getAxisHorizontal = "Horizontal2";
        getAxisVertical = "Vertical2";
        getWallAxisHor = "HorizontalWall2";
        getWallAxisVert = "VerticalWall2";

        joystick = GameObject.FindWithTag("Joystick2").GetComponent<DynamicJoystick>();
        wallJoystick = GameObject.FindWithTag("wallJoystick2").GetComponent<FixedJoystick>();
        GameObject.FindWithTag("Joybutton2").GetComponent<Button>().onClick.AddListener(DeployBomb);
        carryFlag = GameObject.FindWithTag("Carry Flag 2");
        otherPlayerScript = GameObject.FindWithTag("Playa").GetComponent<PlayerController>();
    }

    protected override void HandleBombAndWallDeployment()
    {
        if (Input.GetKeyDown(KeyCode.Period) && bombCount > 0 && isBombCooledDown)
        {
            DeployBomb();
        }

        if (Input.GetKeyDown(KeyCode.Comma) && wallCount > 0)
        {
            DeployWall();
        }
    }

    protected override void HandleFlagDrop()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DropFlag();
        }
    }

    protected override void HandleFlagPickup(Collider other)
    {
        if (other.tag == "Flag1" && !isTagged && !otherPlayerScript.isCarryingFlag)
        {
            Debug.Log("Blue player has the flag!");
            Destroy(other.gameObject);
            carryFlag.SetActive(true);
            isCarryingFlag = true;
            setWarningText("have flag");
        }
    }

    protected override void CheckWinCondition(Collider other)
    {
        if ((other.tag == "Flag2" || other.tag == "End Zone 2") && isCarryingFlag)
        {
            Debug.Log("Blue player wins!");
            winningPlayer = "BLUE PLAYER ";
            setWinnerText();
            winningScreen.gameObject.SetActive(true);
        }
    }
}
