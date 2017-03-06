using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubeObstacle : MonoBehaviour
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
            spawningTime -= Time.deltaTime;
            if (spawningTime <= 0)
            {
                CreateObstacle();
            }
        }
    }

    void CreateObstacle()
    {  
        GameObject newObstacle = Instantiate(obstaclePrefab, new Vector3(tracks[Random.Range(0, 2)], 1.5f, 35), obstaclePrefab.transform.rotation);
        newObstacle.transform.parent = GameObject.Find("Obstacles").transform;
        spawningTime = 5.0f;
    }
}
