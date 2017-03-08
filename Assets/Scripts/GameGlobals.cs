﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameGlobals : MonoBehaviour {

    public float speed = 10f;
    public bool isMoving = false;
    public int score = 0;
    public int lives = 3;

    public Text textPressToBegin;
    public Text textLives;
    private bool gameOver = false;

    void Start()
    {
        textLives.text = "Lives :" + lives;
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKey("escape"))
                Application.Quit();

            //Unpause
            if (!isMoving && Input.anyKeyDown)
            {
                isMoving = true;
                textPressToBegin.text = "";
            }

            //Check if the game is over
            if (lives <= 0)
            {
                gameOver = true;
                isMoving = false;
                textPressToBegin.text = "YOU LOST\nPress Space to Retry";
            }
            //Update Lives
            textLives.text = "Lives: " + lives;
        } else
        {
            //Restart the game
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }
        }

   
    }
}
