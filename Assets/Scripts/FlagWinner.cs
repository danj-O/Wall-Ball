using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagWinner : MonoBehaviour
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
        //Debug.Log("Player 2 Wins!!!");

        if (other.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Blue Player Wins!!!");
            //restarts the game--should change this to option menu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

}
