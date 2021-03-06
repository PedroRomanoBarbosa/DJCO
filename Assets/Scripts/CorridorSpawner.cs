﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorSpawner : MonoBehaviour
{
	private Generate generator;
	private GameGlobals game;

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
		game = GameObject.Find("GameController").GetComponent<GameGlobals>();
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

		generator.GenerateSection (game.difficulty);
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
				int rand;
				switch (type) {
				case Generate.Types.Column:
					gameObject = Instantiate (Column, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 0.5f, 6.5f * y);
					break;
				case Generate.Types.Bench:
					gameObject = Instantiate (Bench, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 2f, 6.5f * y);
					rand = Random.Range (0, 3);
					switch (rand) {
					case 1:
						gameObject.transform.Rotate(Vector3.up * 15, Space.Self);
						break;
					case 2:
						gameObject.transform.Rotate(Vector3.up * -15, Space.Self);
						break;
					}
					break;
				case Generate.Types.Door:
					gameObject = Instantiate (Door, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f + 2.1f, 3.5f, 6.5f * y);
					break;
				case Generate.Types.Coin:
					gameObject = Instantiate (Coin, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 2f, 6.5f * y);
					break;
				case Generate.Types.BenchCoin:
					gameObject = Instantiate (Bench, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 2f, 6.5f * y);
					rand = Random.Range (0, 3);
					switch (rand) {
					case 1:
						gameObject.transform.Rotate(Vector3.up * 15, Space.Self);
						break;
					case 2:
						gameObject.transform.Rotate(Vector3.up * -15, Space.Self);
						break;
					}
					gameObject = Instantiate (Coin, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 4f, 6.5f * y);
					break;
				case Generate.Types.Beer:
					gameObject = Instantiate (Beer, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 0f, 6.5f * y);
					break;
				case Generate.Types.BenchBeer:
					gameObject = Instantiate (Bench, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 0f, 6.5f * y);
					rand = Random.Range (0, 3);
					switch (rand) {
					case 1:
						gameObject.transform.Rotate(Vector3.up * 15, Space.Self);
						break;
					case 2:
						gameObject.transform.Rotate(Vector3.up * -15, Space.Self);
						break;
					}
					gameObject = Instantiate (Beer, newSection.transform);
					gameObject.transform.localPosition = new Vector3 (5f * x - 5f, 4f, 6.5f * y);
					break;
				}
			}
		}
		return newSection;
	}

}