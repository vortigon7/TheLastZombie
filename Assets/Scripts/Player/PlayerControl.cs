using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	// Variables used in controlling the player ;
	public float maxSpeed = 120f; // A value the player's speed force gets multiplied with <-- exists only for balancing speed
	public float dashDelay; // dashDelay is the float dashTimer starts the count from
	private float dashTimer; // dashTimer is the countdown of the Dash Ability

	private Rigidbody2D pRigidBody; // pRigidBody is the RigidBody2D component of the player
	private Vector2 spriteDirection; // spriteDirection is the direction where the player sprite is looking
	private Vector2 pVelocityOld; // pVelocityOld is a Vector2 that stores the velocity of the player right before its next value
	private Vector2 pMovement; // pMovement is the amount and direction of the force the player should move
	private bool pDashState; // pDashState is a boolean that defines whether the player is dashing

	private PlayerComponent pPlayer; // This is a Player.cs script

	void Start () {
		pPlayer = GetComponent<PlayerComponent> (); // This line looks for the Player.cs script attached to the player object
		pRigidBody = GetComponent<Rigidbody2D> (); // This line looks four the player's RigidBody2D component

	}

	// Update is called once per frame
	void Update () {

		// This if statement makes the Dash Ability's countdown happen
		if (dashTimer >= -1) {
			dashTimer -= Time.deltaTime;
		}
			
		FaceMouse (); // This calls the FaceMouse function every Update

		// Displaying the player's velocity in the console
		/*int n = 0;
		while (n < (3 * Time.deltaTime)) {
			Debug.Log (pRigidBody.velocity.magnitude);
			n++;
		}*/
		// A variable that stores the velocity of the player right before its next value
		pVelocityOld = pRigidBody.velocity;
	}

	// FixedUpdate is called before Update; everything related to physics should be put here
	void FixedUpdate ()
	{
		// This part makes the character move around using WASD or the arrow keys
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float accMulti = 1 - (pRigidBody.velocity.magnitude / maxSpeed);

		// This if statement checks whether the player uses the Dash Ability
		if (pDashState) {
			pMovement = Vector2.zero;
			if (pRigidBody.velocity.magnitude < 5) {
				pDashState = false;
			}
		} else { // If the player is not using the Dash Ability, the player is able to move
			pMovement = new Vector2 (moveHorizontal * pPlayer.pSpeed * accMulti * Time.deltaTime, moveVertical * pPlayer.pSpeed * accMulti * Time.deltaTime);
		}
		pRigidBody.AddForce (pMovement); // This adds the force the player is moving with

		// This is a bugfix where the player keeps moving after using the Dash Ability
		if ((pRigidBody.velocity.magnitude < 1) && (pVelocityOld.sqrMagnitude > pRigidBody.velocity.sqrMagnitude)) {
			pRigidBody.velocity -= pRigidBody.velocity * Time.deltaTime * 300f;
		}

		// This if statement starts the Dash Ability
		if ((Input.GetKeyDown (KeyCode.Mouse1)) && (dashTimer <= 0)) {
			//pRigidBody.velocity = Vector2.zero; <-- This will need further work
			pDashState = true;
			pRigidBody.AddForce (spriteDirection.normalized * pPlayer.pSpeed * 1f);
			dashTimer = dashDelay;
		}
			

	}

	// A function that makes the character always face the mouse
	void FaceMouse () {
		Vector2 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

		spriteDirection = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		 
		transform.up = spriteDirection;
	}
}