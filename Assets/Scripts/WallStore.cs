using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallStore : MonoBehaviour
{
    public float respawnTimer;
    public int inventory;

    public Text wallStoreText;

    // Start is called before the first frame update
    void Start()
    {
        inventory = 1;
        respawnTimer = 5f;
        SetWallCountText();
    }

    // Update is called once per frame
    void Update()
    {
        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0)
        {
            respawnTimer = 5f;
            inventory += 1;
            SetWallCountText();
        }
        else if (respawnTimer > 0)
        {
            SetWallCountText();
        }
    }
    private void SetWallCountText()
    {
        wallStoreText.text = $"New wall in: {respawnTimer.ToString("0")} \n Walls left: {inventory} ";
    }
}
