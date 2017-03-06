using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject platformPrefab;
    public GameObject emptyCorridorPrefab;

    private GameObject latestCorridor;

    private GameGlobals game;

    // Use this for initialization
    void Start()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();
        CreateSection_EmptySection(45f);
        latestCorridor = CreateSection_EmptySection(95f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(latestCorridor.transform.position.z < 95)
        {
            latestCorridor = CreateRandomSection(latestCorridor.transform.position.z + 50);
        }
    }


    GameObject CreateRandomSection(float position)
    {
        //Mudar isto para um random entre os outros tipos de secções
        GameObject newSection = CreateSection_EmptySection(position);


        return newSection;
    }


    GameObject CreateSection_EmptySection(float position)
    {
        GameObject newEmptyCorridor = Instantiate(emptyCorridorPrefab, new Vector3(0,-0.05f,position), Quaternion.identity);
        newEmptyCorridor.transform.parent = GameObject.Find("Platforms").transform;
        return newEmptyCorridor;
    }

}