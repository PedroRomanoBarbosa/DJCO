using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    private Vector3 movement;
    private Rigidbody rigidbodyComponent;
    public Vector3 speed;
    public Vector3 direction;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }
        else
        {
            movement = new Vector3(speed.x * direction.x, speed.y * direction.y, speed.z * direction.z);
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
