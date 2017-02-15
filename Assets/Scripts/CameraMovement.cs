using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	private Transform player;
	private float offset = 5f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraPos = transform.position;
		cameraPos.z = player.position.z - offset;
		transform.position = cameraPos;
	}
}
