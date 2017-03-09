using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private GameGlobals game;
    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundScript.Instance.MakeOuchSound();
            game.lives -= 1;
        }
    }

}
