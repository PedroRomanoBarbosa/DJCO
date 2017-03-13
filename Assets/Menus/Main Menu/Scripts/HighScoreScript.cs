using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    public Text highScore;

    void Start ()
    { 
		int atualHighScore = PlayerPrefs.GetInt("HighScore");
        highScore.text = "Highscore: " + atualHighScore;
    }
}
