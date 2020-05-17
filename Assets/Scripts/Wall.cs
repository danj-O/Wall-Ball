using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Wall : MonoBehaviour
{
    public BombExplode bombScript;
    //public HealthBar healthBar;

    public int maxHealth = 150;
    public int currentHealth;

    private float bombRangeFromWall = 2.5f;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, bombRangeFromWall);
        foreach (Collider hit in hits)
        {
            if (hit.tag == "Bomb")
            {
                bombScript = hit.gameObject.GetComponent<BombExplode>();
                //var hitRenderer = gameObject.GetComponent<Renderer>();
                //hitRenderer.material.SetColor("_Color", Color.black);

                if (bombScript.bombTimer <= 0 && currentHealth == 150)
                {
                    currentHealth -= 50;
                    Debug.Log("WALL DAMAGE");
                    var hitRenderer = gameObject.GetComponent<Renderer>();
                    hitRenderer.material.SetColor("_Color", Color.magenta);
                } else if (bombScript.bombTimer <= 0 && currentHealth == 100)
                {
                    currentHealth -= 50;
                    Debug.Log("WALL DAMAGE");
                    var hitRenderer = gameObject.GetComponent<Renderer>();
                    hitRenderer.material.SetColor("_Color", Color.red);
                } else if (bombScript.bombTimer <= 0 && currentHealth == 50)
                {
                    Destroy(gameObject);
                    Debug.Log("WALL BREAK!!");
                }


            }
        }
        //if (wallHealth <= 0)
        //{
        //    Destroy(gameObject);
        //    Debug.Log("WALL BREAK!!");

        //}
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            //bombScript = collision.gameObject.GetComponent<BombExplode>();
            //Debug.Log(bombScript.bombTimer);
            //if (bombScript.bombTimer <= 0)
            //{
            //    Debug.Log("minus 20 health!");
            //}

            //Debug.Log(bombScript);
            //Debug.Log("collision");
        }
    }
}
