using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnECTS : MonoBehaviour
{
    public float spawningTime = 5.0f;
    public GameObject ECTPrefab;
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
        GameObject newObstacle = Instantiate(ECTPrefab, new Vector3(Random.Range(-5f, 5f), 1.5f, 100), ECTPrefab.transform.rotation);
        newObstacle.transform.parent = gameObject.transform;
        spawningTime = 5.0f;
    }
}
