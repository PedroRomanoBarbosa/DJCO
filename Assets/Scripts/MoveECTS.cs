using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveECTS : MonoBehaviour
{
    private Vector3 movement;
    private GameGlobals game;

    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();

    }
    void Update()
    {
        //Movement
        if (game.isMoving)
            movement = new Vector3(0, 0, -1 * game.speed) * Time.deltaTime;
        else
            movement = new Vector3(0, 0, 0);
        transform.Translate(movement);
    }

    //Check if player collects THIS ECTS
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            game.score += 1;
            Destroy(gameObject);
        }
    }

}
