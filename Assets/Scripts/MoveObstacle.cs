using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    private Vector3 movement;
    private GameGlobals game;

    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();

    }
    void Update()
    {
        if (game.isMoving)
            movement = new Vector3(0, 0, -1 * game.speed) * Time.deltaTime;
        else
            movement = new Vector3(0, 0, 0);

        transform.Translate(movement);
    }

}
