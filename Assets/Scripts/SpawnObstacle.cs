using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    private int[] tracks = {-3,0,3};
    public float spawningTime = 10.0f;
    public GameObject obstaclePrefab;

    void Start()
    {

    }

    void Update()
    {
        CreateObstacle();
    }

    void CreateObstacle()
    {
        spawningTime -= Time.deltaTime;

        if (spawningTime <= 0)
        {
            Instantiate(obstaclePrefab, new Vector3(tracks[Random.Range(0,2)],0, 35), Quaternion.identity);
            spawningTime = 5.0f;
        }
    }
}
