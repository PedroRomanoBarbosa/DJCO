using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSpawner : MonoBehaviour {

    private GameGlobals game;

    //Prefabs
    public GameObject grassPrefab;
    public GameObject treePrefab;
    
    //Grass Vars
    private GameObject latestGrass;
    public GameObject initialGrassPlane;
    public int grassSpawnPlane = 95;

    //Tree Vars
    public int treeSpawnPlane = 95;
    private float treeSpawnTimer = 25f;
    public float treeDistance = 100;


    void Start ()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();

        //Create a couple extra grass planes.
        latestGrass = CreateSection(initialGrassPlane, grassPrefab);
        latestGrass = CreateSection(latestGrass, grassPrefab);
    }

    private void Update()
    {
        if (game.isMoving)
        {

            //Tree Spawning
            treeSpawnTimer -= game.speed * Time.deltaTime;
            if (treeSpawnTimer <= 0)
            {
                CreateTree();
            }
        }
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
        newSection.transform.parent = gameObject.transform;

        return newSection;
    }

    void CreateTree()
    {
        GameObject newTree = Instantiate(treePrefab, new Vector3(Random.Range(-23f,-13f), 3f, treeSpawnPlane), Quaternion.identity);
        newTree.transform.parent = gameObject.transform;
        treeSpawnTimer = treeDistance + Random.Range(-treeDistance/2, treeDistance*1.5f);
    }

}
