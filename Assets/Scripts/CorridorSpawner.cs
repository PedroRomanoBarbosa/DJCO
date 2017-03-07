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

    void Start()
    {
        //Create Two Corridors.
        float previous_edge = initialCorridor.transform.position.z + initialCorridor.GetComponent<CorridorVariables>().CorridorLength / 2;
        GameObject newSection = CreateSection_EmptySection(previous_edge + emptyCorridorPrefab.GetComponent<CorridorVariables>().CorridorLength / 2);
        newSection.transform.parent = GameObject.Find("Corridors").transform;

        previous_edge = newSection.transform.position.z + newSection.GetComponent<CorridorVariables>().CorridorLength / 2;
        latestCorridor = CreateSection_EmptySection(previous_edge + emptyCorridorPrefab.GetComponent<CorridorVariables>().CorridorLength / 2);
        latestCorridor.transform.parent = GameObject.Find("Corridors").transform;

    }

    void FixedUpdate()
    {
        if(latestCorridor.transform.position.z < 95)
        {
            latestCorridor = CreateRandomSection(latestCorridor);
        }
    }
    
    GameObject CreateRandomSection(GameObject latest)
    {
        float previous_edge = latest.transform.position.z + latest.GetComponent<CorridorVariables>().CorridorLength/2;

        //--TODO--
        //Aqui ele deve escolher um prefab aleatoriamente entre os disponiveis.
        GameObject chosen = emptyCorridorPrefab;
        //--TODO--

        GameObject newSection = CreateSection_EmptySection(previous_edge + chosen.GetComponent<CorridorVariables>().CorridorLength/2);
        newSection.transform.parent = GameObject.Find("Corridors").transform;
        return newSection;
    }

    GameObject CreateSection_EmptySection(float position)
    {
        //Para criar os dois corredores vazios no inicio.
        GameObject newEmptyCorridor = Instantiate(emptyCorridorPrefab, new Vector3(0,0f,position), Quaternion.identity);
        newEmptyCorridor.transform.parent = GameObject.Find("Corridors").transform;
        return newEmptyCorridor;
    }
    
}