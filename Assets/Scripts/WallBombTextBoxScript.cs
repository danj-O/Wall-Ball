using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBombTextBoxScript : MonoBehaviour
{
    public void anim()
    {
        gameObject.GetComponent<Animator>().Play("smallerThenBigger");

    }
}
