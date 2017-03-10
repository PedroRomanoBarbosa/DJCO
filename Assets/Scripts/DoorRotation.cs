using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour {
	private bool inAnimation;
	public GameObject player;
	public float speed;
	public float radius;

	void Start () {
		inAnimation = false;
	}

	void Update () {
		float distance = Vector3.Distance (transform.position, player.transform.position);
		if (distance < radius) {
			inAnimation = true;
		}
		if (inAnimation == true) {
			if (transform.rotation.z < 0.5) {
				transform.Rotate (0, 0, -speed * Time.deltaTime, Space.Self);
			}
		}
	}
}
