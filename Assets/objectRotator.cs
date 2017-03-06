using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectRotator : MonoBehaviour {
    
    private GameGlobals game;

    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();
    }
    void Update()
    {
        float rot = transform.rotation.y + game.speed * Time.deltaTime * 100;
        transform.rotation = Quaternion.Euler(45f, rot, 45f);
    }

}
