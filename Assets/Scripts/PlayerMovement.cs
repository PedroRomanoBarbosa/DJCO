using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private CharacterController controller;
	private float verticalVelocity = 0.0f;
	private float timer = 0.0f;
	private bool jumping = false;
	private Vector3 moveVector;

	public float speed = 10f;
	public float jumpSpeed = 10f;
	public float gravity = 9.8f;

	void Start () {
		controller = GetComponent<CharacterController> ();
	}

	void Update () {
		if (controller.isGrounded) {
			verticalVelocity = 0.0f;
			timer = 0.0f;
			jumping = false;
		} else {
			verticalVelocity += -gravity * Time.deltaTime;
		}
		moveVector = Vector3.zero;

		// STRAFE
		moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

		// JUMP
		if(Input.GetKeyDown("space")) {
			jumping = true;
		}
		if (jumping) {
			timer += Time.deltaTime;
			if (timer <= 0.1f) {
				if (Input.GetKey("space")) {
					verticalVelocity += jumpFunction (timer / 0.25f) * Time.deltaTime * jumpSpeed;
				}
			}
		}
		moveVector.y += verticalVelocity;
		controller.Move (moveVector * Time.deltaTime);
	}

	float jumpFunction(float t) {
		return -t * t + 1;
	}
}
