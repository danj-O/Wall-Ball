using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//zone needs a script to know when something enters its colider
//make the player object a public component to put in opposite players




public class ZoneScript : MonoBehaviour
{
    public GameObject player;
    private PlayerController1 playerScript;

    private float overlapColliderRange = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Collider[] hits = Physics.OverlapSphere(transform.position, overlapColliderRange);
        //foreach (Collider hit in hits)
        //{
        //    if (hit.tag == player.tag)
        //    {
        //        Debug.Log("entered red zone");
        //    }
        //}
    }
    private void OnTriggerEnter(Collider collider)
    {
      
        if (collider.tag == player.tag)
        {
            if (collider.tag == "Player2")
            {
            playerScript = collider.GetComponent<PlayerController1>();
            playerScript.inDangerZone = true;
            Debug.Log("entered red zone");
            }
            else if (collider.tag == "Playa")
            {
                playerScript = collider.GetComponent<PlayerController1>();
                playerScript.inDangerZone = true;
                Debug.Log("entered blue zone");
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == player.tag)
        {
            if (other.tag == "Player2")
            {
                playerScript.inDangerZone = false;
                playerScript.isTagged = false;
                Debug.Log("left red zone");
            }
            else if (other.tag == "Playa")
            {
                playerScript.inDangerZone = false;
                playerScript.isTagged = false;
                Debug.Log("left blue zone");
            }


        }
    }

}
