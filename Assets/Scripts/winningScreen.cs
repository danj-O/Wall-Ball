using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winningScreen : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("MainGame");
    }
}
