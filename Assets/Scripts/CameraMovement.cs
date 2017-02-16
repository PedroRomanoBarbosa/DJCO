using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	private Transform player;
	public float offsetZ = 5f;
	public float offsetY = 5f;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update () {
		transform.position = new Vector3 (0, player.position.y + offsetY, player.position.z - offsetZ);
	}
}
