using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 GroundPos = transform.position;
        if (GroundPos.z < -20)
        {
            Destroy(gameObject);
        }
    }
}
