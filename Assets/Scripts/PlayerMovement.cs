using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private float verticalVelocity = 0.0f;
	private float timer = 0.0f; 
	public bool jumping = true;
	private Vector3 moveVector;

	public float speed = 10f;
    public float aerialSlowDown = 1f;
    public float jumpSpeed = 20f;
	public float gravity = 2000f;
	public bool grounded;

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
        if (game.isMoving) {
            animator.SetFloat("MoveSpeed", game.speed);

            //Movement
            movementLogic();

			//Set Animations
			animationLogic();
        } else {
            //moveVector should not be changed while game is paused.
            animator.SetFloat("MoveSpeed", 0);
        }
    }

	float jumpFunction(float t) {
		return -t * t + 0.6f;
	}

    void animationLogic() {
		if (jumping) {
			animator.SetBool ("Grounded", false);
		} else {
			animator.SetBool ("Grounded", true);
		}
    }

    void movementLogic() {
        moveVector = Vector3.zero;

        // STRAFE
		if (Input.GetAxisRaw ("Horizontal") <= 0 && transform.position.x >= -5 || Input.GetAxisRaw ("Horizontal") >= 0 && transform.position.x <= 5) {
			moveVector.x = Input.GetAxisRaw ("Horizontal") * speed;
		}

		// JUMP
		if (Input.GetKey ("space")) {
            //Make jump sound
            SoundScript.Instance.MakeJumpSound();
			jumping = true;
		}
		if (jumping) {
			//Slower Aerial Movement
			moveVector.x *= aerialSlowDown;
			timer += Time.deltaTime;
			if (timer <= 0.35f) {
				if (Input.GetKey ("space")) {
					verticalVelocity += jumpFunction (timer) * Time.deltaTime * jumpSpeed;
				}
			} else {
				verticalVelocity += -gravity * 25 * Time.deltaTime;
			}
		}
        moveVector.y += verticalVelocity;
        controller.Move(moveVector * Time.deltaTime);

		if (controller.isGrounded) {
			verticalVelocity = 0.0f;
			timer = 0.0f;
			jumping = false;
		}
    }
}
