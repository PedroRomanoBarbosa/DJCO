using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSpawner : MonoBehaviour {
    
    //Prefabs
    public GameObject grassPrefab; 
    
    //Vars
    private GameObject latestGrass;
    public GameObject initialGrassPlane;
    public int grassSpawnPlane = 95;


    void Start () {
        //Create a couple extra grass planes.
        latestGrass = CreateSection(initialGrassPlane, grassPrefab);
        latestGrass = CreateSection(latestGrass, grassPrefab);
    }

	void FixedUpdate () {
        if (latestGrass.transform.position.z < grassSpawnPlane)
        {
            latestGrass = CreateSection(latestGrass, grassPrefab);
        }
    }

    GameObject CreateSection(GameObject previous, GameObject newSectionPrefab)
    {
        float previous_edge = previous.transform.position.z + previous.GetComponent<ObjectVariables>().CorridorLength / 2;
        float newPosition = previous_edge + newSectionPrefab.GetComponent<ObjectVariables>().CorridorLength / 2;

        GameObject newSection = Instantiate(newSectionPrefab, new Vector3(0, 0f, newPosition), Quaternion.identity);
        newSection.transform.parent = GameObject.Find(gameObject.name).transform;

        return newSection;
    }

}
