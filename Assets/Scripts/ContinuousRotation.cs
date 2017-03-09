using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousRotation : MonoBehaviour {
	private float rotation;
	public int speed;

	void Update () {
		transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.Self);
	}
}
