using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private float GroundLength = 50f;

    private GameGlobals game;

    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();
    }


    void Update()
    {
        if (game.isMoving)
        {
            Vector3 GroundPos = transform.position;
            GroundPos.z -= game.speed * Time.deltaTime;
            transform.position = GroundPos;
        }
    }
}
