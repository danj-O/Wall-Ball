using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombStore : MonoBehaviour
{
    public float respawnTimer;
    public int inventory;

    public Text bombStoreText;

    // Start is called before the first frame update
    void Start()
    {
        inventory = 1;
        respawnTimer = 5f;
        SetBombCountText();
    }

    // Update is called once per frame
    void Update()
    {
        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0)
        {
            respawnTimer = 5f;
            inventory += 1;
            SetBombCountText();
        }
        else if (respawnTimer > 0)
        {
            SetBombCountText();
        }
    }
    private void SetBombCountText()
    {
        bombStoreText.text = $"New bomb in: {respawnTimer.ToString("0")} \n Bombs left: {inventory} ";
    }
}
