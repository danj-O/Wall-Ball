using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public int wallBreak;
    public float bombTimer;
    //public bool isWallInRange = false;
    private bool isBombLive = false;

    // Start is called before the first frame update
    void Start()
    {
        isBombLive = true;
        bombTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBombLive)
        {

            //Debug.Log(bombTimer);
            if (bombTimer <= 0)
            {
                //Debug.Log("BOOM");
                isBombLive = false;
                bombTimer = 1;
                Destroy(gameObject);
                
            }
            else if (bombTimer > 0)
            {
                bombTimer -= Time.deltaTime;
            }
        }
    }
}
