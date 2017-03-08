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
    public Text textTime;
    public Text textScore;
    private bool gameOver = false;
    private float elapsedTime = 0f;

    void Start()
    {
        textLives.text = "Lives :" + lives;
        textTime.text = "Time: 00:00";
        textScore.text = "Score: " + score;
    }

    void Update()
    {
        if (!gameOver)
        {
            if (isMoving)
            {
                elapsedTime += Time.deltaTime;
                UpdateUIText();
            }

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
            
        }
        else
        {
            //Restart the game
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }
        }
    }

    void UpdateUIText()
    {
        //Update Lives
        textLives.text = "Lives: " + lives;
        //Update Time
        if (((int)elapsedTime) % 60 < 10)
        {
            textTime.text = "Time: " + ((int)elapsedTime) / 60 + ":0" + ((int)elapsedTime) % 60;
        }
        else
        {
            textTime.text = "Time: " + ((int)elapsedTime) / 60 + ":" + ((int)elapsedTime) % 60;
        }
        //Update Score
        textScore.text = "Score: " + score;
    }
}
