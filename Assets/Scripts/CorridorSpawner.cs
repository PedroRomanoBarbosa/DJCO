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
    public GameObject latestCorridor;
    public GameObject initialCorridor;
    public GameObject[] corridorPrefabs;

    public int coridorSpawnPlane = 95;

    void Start()
    {
		generator = new Generate (3, 3);
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

		float newPosition = latest.transform.position.z + latest.GetComponent<ObjectVariables>().CorridorLength/2
			+ 10f;

		generator.GenerateSection ();
		GameObject newSection = instantiateSection (newPosition);
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
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 0.5f, 6.5f * y);
					break;
				case Generate.Types.Bench:
					gameObject = Instantiate (Bench);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 2f, 6.5f * y);
					break;
				case Generate.Types.Door:
					gameObject = Instantiate (Door);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f + 2.1f, 3.5f, 6.5f * y);
					break;
				case Generate.Types.Coin:
					gameObject = Instantiate (Coin);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 2f, 6.5f * y);
					break;
				case Generate.Types.BenchCoin:
					gameObject = Instantiate (Bench);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 2f, 6.5f * y);
					gameObject = Instantiate (Coin);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 4f, 6.5f * y);
					break;
				case Generate.Types.Beer:
					gameObject = Instantiate (Beer);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 0f, 6.5f * y);
					break;
				case Generate.Types.BenchBeer:
					gameObject = Instantiate (Bench);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 0f, 6.5f * y);
					gameObject = Instantiate (Beer);
					gameObject.transform.parent = newSection.transform;
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 4f, 6.5f * y);
					break;
				}
			}
		}
		return newSection;
	}

}