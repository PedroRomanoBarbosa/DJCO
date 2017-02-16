using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMovement : MonoBehaviour {
	public float speed = 5f;

	void Start () {
		
	}

	void Update () {
		transform.Translate (Vector3.back * speed * Time.deltaTime);
	}
}
