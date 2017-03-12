using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorSpawner : MonoBehaviour
{
	private Generate generator;

    //Prefabs
    public GameObject emptyCorridorPrefab;
	public GameObject Column;
	public GameObject Bench;
	public GameObject Door;
	public GameObject Coin;

    //Vars
    private GameObject latestCorridor;
    public GameObject initialCorridor;
    public GameObject[] corridorPrefabs;

    public int coridorSpawnPlane = 95;

    void Start()
    {
		generator = new Generate (3, 3);

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

		generator.GenerateSection ();
		instantiateObjects ();
        GameObject newSection = Instantiate(newSectionPrefab, new Vector3(0, 0f, newPosition), Quaternion.identity);
        newSection.transform.parent = GameObject.Find(gameObject.name).transform;

        return newSection;
    }

	/**
	 * TODO: Put obstacles in the right position
	 */
	private void instantiateObjects () {
		for (int y = 0; y < generator.getColumns(); y++) {
			for (int x = 0; x < generator.getLines(); x++) {
				Generate.Types type = generator.getMatrix() [y, x];
				switch (type) {
				case Generate.Types.Column:
					Instantiate (Column, new Vector3 (5 * x - 5, 1, 5 * y), Quaternion.identity);
					break;
				case Generate.Types.Bench:
					Instantiate (Bench, new Vector3 (5 * x - 5, 2, 5 * y), Quaternion.identity);
					break;
				case Generate.Types.Door:
					Instantiate (Door, new Vector3 (5 * x - 5, 4, 5 * y), Quaternion.identity);
					break;
				case Generate.Types.Coin:
					Instantiate (Coin, new Vector3 (5 * x - 5, 2, 5 * y), Quaternion.identity);
					break;
				case Generate.Types.BenchCoin:
					Instantiate (Bench, new Vector3 (5 * x - 5, 2, 5 * y), Quaternion.identity);
					Instantiate (Coin, new Vector3 (5 * x - 5, 4, 5 * y), Quaternion.identity);
					break;
				}
			}
		}
	}

}