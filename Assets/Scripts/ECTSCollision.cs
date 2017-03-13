using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECTSCollision : MonoBehaviour {
    private Vector3 movement;
    private GameGlobals game;

    void Start () {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();
    }

    //Check if player collects THIS ECTS
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            game.score += 1;
			if (game.score == 10) {
				game.difficulty = Generate.Difficulties.Easy;
			} else if (game.score == 20) {
				game.difficulty = Generate.Difficulties.Medium;
			} else if (game.score == 40) {
				game.difficulty = Generate.Difficulties.Hard;
			}
            SoundScript.Instance.MakeCoinSound();
            Destroy(gameObject);
        }
    }

}
