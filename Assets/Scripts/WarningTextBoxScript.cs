using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTextBoxScript : MonoBehaviour
{
    public void anim()
    {
        gameObject.GetComponent<Animator>().Play("warningTextAnim");

    }
}
