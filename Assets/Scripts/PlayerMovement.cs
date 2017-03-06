using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private float verticalVelocity = 0.0f;
	private float timer = 0.0f;
	private bool jumping = false;
	private Vector3 moveVector;

	public float speed = 10f;
	public float jumpSpeed = 10f;
	public float gravity = 9.8f;

    private CharacterController controller;
    private Animator animator;
    private GameGlobals game;

    void Start () {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();
        controller = GetComponent<CharacterController> ();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", game.speed);
    }

	void Update () {

        animator.SetFloat("MoveSpeed", game.speed);

        if (controller.isGrounded) {
			verticalVelocity = 0.0f;
			timer = 0.0f;
			jumping = false;
            if(!animator.GetBool("Grounded"))
                animator.SetBool("Grounded", true);
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
            if (animator.GetBool("Grounded"))
                animator.SetBool("Grounded", false);
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
