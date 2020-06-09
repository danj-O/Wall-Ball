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

    private float explosionRadius = 5f;
    private float power = 900f;
    private float upForce = 0f;

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
                //Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
                //foreach (Collider hit in colliders)
                //{
                //    if (hit.gameObject.tag == "Playa" || hit.gameObject.tag == "Player2")
                //    {
                //        Rigidbody rb = hit.GetComponent<Rigidbody>();
                //        rb.AddExplosionForce(power, transform.position, explosionRadius, upForce, ForceMode.Impulse);
                //    }
                //}
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
