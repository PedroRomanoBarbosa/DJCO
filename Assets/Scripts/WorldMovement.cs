using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool isMoving = false;

    public Text textPressToBegin;

    void Start()
    {
    }

    void Update()
    {
        if(isMoving)
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        
        if (!isMoving && Input.anyKeyDown)
        {
            isMoving = true;
            textPressToBegin.text = "";
        }
    }
}
