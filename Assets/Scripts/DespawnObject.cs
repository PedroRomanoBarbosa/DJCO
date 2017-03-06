using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObject : MonoBehaviour {

    public float despawnPlane = -30f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 GroundPos = transform.position;
        if (GroundPos.z < despawnPlane)
        {
            Destroy(gameObject);
        }
    }
}
