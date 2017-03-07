using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorSpawner : MonoBehaviour
{
    //Prefabs
    public GameObject emptyCorridorPrefab;

    //Vars
    private GameObject latestCorridor;
    public GameObject initialCorridor;
    public GameObject[] corridorPrefabs;

    public int coridorSpawnPlane = 95;

    void Start()
    {
        //Create Two Empty Corridors.
        latestCorridor = CreateSection(initialCorridor, emptyCorridorPrefab);
        latestCorridor = CreateSection(latestCorridor, emptyCorridorPrefab);
        
    }

    void FixedUpdate()
    {
        if(latestCorridor.transform.position.z < coridorSpawnPlane)
        {
            latestCorridor = CreateRandomSection(latestCorridor);
        }
    }
    
    GameObject CreateRandomSection(GameObject latest)
    {
        GameObject chosen = corridorPrefabs[Random.Range(0, corridorPrefabs.Length)];

        GameObject newSection = CreateSection(latestCorridor, chosen);
        return newSection;
    }

    GameObject CreateSection_EmptySection(float position)
    {
        //Para criar os dois corredores vazios no inicio.
        GameObject newEmptyCorridor = Instantiate(emptyCorridorPrefab, new Vector3(0,0f,position), Quaternion.identity);
        newEmptyCorridor.transform.parent = GameObject.Find(gameObject.name).transform;
        return newEmptyCorridor;
    }

    GameObject CreateSection(GameObject previous, GameObject newSectionPrefab)
    {
        float previous_edge = previous.transform.position.z + previous.GetComponent<ObjectVariables>().CorridorLength / 2;
        float newPosition = previous_edge + newSectionPrefab.GetComponent<ObjectVariables>().CorridorLength/2;

        GameObject newSection = Instantiate(newSectionPrefab, new Vector3(0, 0f, newPosition), Quaternion.identity);
        newSection.transform.parent = GameObject.Find(gameObject.name).transform;

        return newSection;
    }

}