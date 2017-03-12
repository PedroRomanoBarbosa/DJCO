using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Text scoreText;
    public static int score;

     void Start ()
    {
        scoreText.text = "Score: " + score;
    }
	
	void Update ()
    {
		
	}
}
