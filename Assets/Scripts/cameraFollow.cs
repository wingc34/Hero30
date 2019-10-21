using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private GameObject player;

    private float x;
    private float y;
    private Vector2 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(transform.parent.name);
        x = player.transform.position.x;
	    y = player.transform.position.y;
        playerPos = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos.x = (player.transform.position.x - x) / 2;
        playerPos.y = (player.transform.position.y - y) / 2;
        transform.position = playerPos;
    }
}
