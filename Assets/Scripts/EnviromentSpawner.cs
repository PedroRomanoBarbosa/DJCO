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
        
        GameObject newTree;
        float size_Seed = Random.Range(0f, 1f);

        if (size_Seed <= 0.40f) //Small tree
        {
            newTree = Instantiate(treePrefab, new Vector3(Random.Range(-24f, -11.5f), 0, treeSpawnPlane), Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up));
            newTree.transform.localScale = Vector3.one * 0.5f;
        }
        else if(size_Seed <= 0.80f) //Medium Tree
        {
            newTree = Instantiate(treePrefab, new Vector3(Random.Range(-22f, -13f), 0, treeSpawnPlane), Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up));
            newTree.transform.localScale = Vector3.one * 1.5f;
        }
        else    //Large Tree
        {
            newTree = Instantiate(treePrefab, new Vector3(-18, 0, treeSpawnPlane), Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up));
            newTree.transform.localScale = Vector3.one * 2.5f;
        }
            
        newTree.transform.parent = gameObject.transform;
        treeSpawnTimer = treeDistance + Random.Range(-treeDistance/2, treeDistance*1.5f);
    }

}
