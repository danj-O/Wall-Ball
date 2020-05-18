using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public int wallBreak;
    public float bombTimer;
    public float bombTimerDefault = 2;

    //public bool isWallInRange = false;
    private bool isBombLive = false;
    public ParticleSystem explosion;
    private float particleDestroyDuration;

    // Start is called before the first frame update
    void Awake()
    {
        isBombLive = true;
        bombTimer = bombTimerDefault;
        //Instantiate(Sparks, transform.position, Quaternion.identity);

        //particleDestroyDuration = gameObject.GetComponent<ParticleSystem>().main.startLifetime;
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
                bombTimer = bombTimerDefault;
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                
            }
            else if (bombTimer > 0)
            {
                bombTimer -= Time.deltaTime;
            }
        }
    }
}
