using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagWinner2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Player 1 Wins!!!");

        if (other.gameObject.CompareTag("Playa"))
        {
            Debug.Log("Red Player Wins!!!");
            //restarts the game--should change this to option menu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

}
