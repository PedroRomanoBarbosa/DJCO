using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject platformPrefab;
    public GameObject emptyCorridorPrefab;

    private GameGlobals game;
    private float spawnPoint = 145f;

    // Use this for initialization
    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();
        CreateEmptySection(45f);
        CreateEmptySection(95f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEmptySection(float position)
    {
        GameObject newEmptyCorridor = Instantiate(emptyCorridorPrefab, new Vector3(0,-0.05f,position), Quaternion.identity);
        newEmptyCorridor.transform.parent = GameObject.Find("Platforms").transform;

    }

}