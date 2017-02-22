using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityGround : MonoBehaviour
{
    private float GroundLength = 50f;
    private float GroundSpeed = 5f;
    public GameObject Ground;

    void Update()
    {
        Vector3 GroundPos = Ground.transform.position;
        GroundPos.z -= GroundSpeed * Time.deltaTime;

        if (GroundPos.z < -GroundLength / 2)
        {
            GroundPos.z += GroundLength;
        }

        Ground.transform.position = GroundPos;
    }
}
