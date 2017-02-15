using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private CharacterController controller;
	private float speed = 5f;
	private float verticalVelocity = 0.0f;
	private float gravity = 9.8f;
	private Vector3 moveVector;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.isGrounded) {
			verticalVelocity = 0.0f;
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
		}
		moveVector = Vector3.zero;
		// X - Left and Right
		moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
		// Y - Up and Down
		moveVector.y = verticalVelocity;
		// Z - Forward and Backward
		moveVector.z = speed;
		controller.Move (moveVector * Time.deltaTime);
	}
}
