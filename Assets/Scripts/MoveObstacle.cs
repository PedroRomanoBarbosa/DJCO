using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;

    private Vector3 movement;
    private Vector3 speed;
    private Vector3 direction;

    private GameGlobals game;

    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();

        direction = new Vector3(0, 0, -1);
        speed = new Vector3(0, 0, game.speed);
    }

    void Update()
    {
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
        else
        {
            if (game.isMoving)
                movement = new Vector3(speed.x * direction.x, speed.y * direction.y, speed.z * direction.z);
            else
                movement = new Vector3(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
        }

        rigidbodyComponent.velocity = movement;
    }
}
