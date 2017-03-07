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
    private bool gameOver = false;

    void Start()
    {
    }

    void Update()
    {
        if (!gameOver)
        {
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
