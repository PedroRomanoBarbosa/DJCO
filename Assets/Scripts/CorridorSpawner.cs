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
	public GameObject Beer;

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
		GameObject newSection = instantiateSection (newPosition);
        //GameObject newSection = Instantiate(newSectionPrefab, new Vector3(0, 0f, newPosition), Quaternion.identity);
        //newSection.transform.parent = GameObject.Find(gameObject.name).transform;

        return newSection;
    }

	/**
	 * TODO: Put obstacles in the right position
	 */
	private GameObject instantiateSection (float position) {
		GameObject newSection = Instantiate(emptyCorridorPrefab, new Vector3(0, 0f, position), Quaternion.identity);
		newSection.transform.parent = GameObject.Find(gameObject.name).transform;
		for (int y = 0; y < generator.getColumns(); y++) {
			for (int x = 0; x < generator.getLines(); x++) {
				Generate.Types type = generator.getMatrix() [y, x];
				GameObject gameObject = new GameObject ();
				switch (type) {
				case Generate.Types.Column:
					gameObject = Instantiate (Column);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 1, 5 * y);
					break;
				case Generate.Types.Bench:
					gameObject = Instantiate (Bench);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 2, 5 * y);
					break;
				case Generate.Types.Door:
					gameObject = Instantiate (Door);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 4, 5 * y);
					break;
				case Generate.Types.Coin:
					gameObject = Instantiate (Coin);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 2, 5 * y);
					break;
				case Generate.Types.BenchCoin:
					gameObject = Instantiate (Bench);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 2, 5 * y);
					gameObject = Instantiate (Coin);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 4, 5 * y);
					break;
				case Generate.Types.Beer:
					gameObject = Instantiate (Beer);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 0, 5 * y);
					break;
				case Generate.Types.BenchBeer:
					gameObject = Instantiate (Bench);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 0, 5 * y);
					gameObject = Instantiate (Beer);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5 * x - 5, 4, 5 * y);
					break;
				}
			}
		}
		return newSection;
	}

}