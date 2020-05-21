using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag2ContainerScriptTag : MonoBehaviour
{
    public GameObject player;
    private PlayerControllerTag playerScript;
    public GameObject flag;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(flag, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
        player = GameObject.FindWithTag("Playa");
        playerScript = player.GetComponent<PlayerControllerTag>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.flagIsDropped)
        {
            Instantiate(flag, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
            playerScript.flagIsDropped = false;
        }
    }
}
