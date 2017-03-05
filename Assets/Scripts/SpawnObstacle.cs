using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    private int[] tracks = {-3,0,3};
    public float spawningTime = 10.0f;
    public GameObject obstaclePrefab;
    private GameGlobals game;

    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();

    }

    void Update()
    {
        if (game.isMoving)
        {
            CreateObstacle();
        }
    }

    void CreateObstacle()
    {
        spawningTime -= Time.deltaTime;

        if (spawningTime <= 0)
        {
            Instantiate(obstaclePrefab, new Vector3(tracks[Random.Range(0, 2)], 0, 35), Quaternion.identity);
            spawningTime = 5.0f;
        }
    }
}
