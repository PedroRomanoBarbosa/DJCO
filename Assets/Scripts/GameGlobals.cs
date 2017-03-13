using System.Collections;
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
    public bool gameOver = false;
    private float elapsedTime = 0f;
	public Generate.Difficulties difficulty = Generate.Difficulties.NoBrainer;

    void Start()
    {
        textTime.text = "Time: 00:00";
        textScore.text = "ECTS: " + score;
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
            } else if (Input.GetKeyDown("p"))
            {
                isMoving = false;
                textPressToBegin.text = "Game is Paused.\nPress any key to continue";
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
            if(Input.GetKeyDown("o"))
            {
                GameOverScript.score = score;
                SceneManager.LoadScene(2);
            }
        }
    }

    void UpdateUIText()
    {
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
