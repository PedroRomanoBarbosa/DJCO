using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGlobals : MonoBehaviour {

    public float speed = 10f;
    public bool isMoving = false;
    public Text textPressToBegin;

    void Start()
    {
    }

    void Update()
    {
        if (!isMoving && Input.anyKeyDown)
        {
            isMoving = true;
            textPressToBegin.text = "";
        }
    }
}
