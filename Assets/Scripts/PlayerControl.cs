using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	// Public variables used in controlling the player 
	public float playerSpeed = 50f;
	public float maxSpeed = 50f;
	public float dashTimer;
	public float dashDelay;
	public float MouseAngle;

	//private int dashDirection;
	private Rigidbody2D playerRigidBody;
	public GameObject player;
	private Vector2 spriteDirection;
	private Vector2 playerDirection;
	private Vector2 DashMovement;
	private Vector2 velocityOld;

	void Start () {
		playerRigidBody = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void Update () {

		if (dashTimer >= -1) {
			dashTimer -= Time.deltaTime;
		}

		FaceMouse ();
		int n = 0;
		while (n < (3 * Time.deltaTime)) {
			Debug.Log (playerRigidBody.velocity.magnitude);
			n++;
		}

		velocityOld = playerRigidBody.velocity;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float accelerationMultiplier = 1 - (playerRigidBody.velocity.magnitude / maxSpeed);
		Vector2 MouseCircle = (new Vector2 (Mathf.Sin (MouseAngle), Mathf.Cos (MouseAngle)) * playerSpeed);
		Vector2 Movement = new Vector2(moveHorizontal * playerSpeed * accelerationMultiplier * Time.deltaTime, moveVertical * playerSpeed * accelerationMultiplier * Time.deltaTime);
		playerRigidBody.AddForce (Movement);

		if ((playerRigidBody.velocity.magnitude < 1) && (velocityOld.sqrMagnitude > playerRigidBody.velocity.sqrMagnitude)) {
			playerRigidBody.velocity -= playerRigidBody.velocity * Time.deltaTime * 300f;
		}

		if ((Input.GetKeyDown (KeyCode.Mouse1)) && (dashTimer <= 0)) {
			playerRigidBody.AddForce (MouseCircle * 1f);
			dashTimer = dashDelay;
			Debug.Log (playerRigidBody);
		}
			

	}

	void FaceMouse () {
		Vector2 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

		spriteDirection = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		MouseAngle = Mathf.Atan2 (spriteDirection.x, spriteDirection.y);
		 
		transform.up = spriteDirection;
	}
		
	void Dash () {
		Vector3 dashDirection = new Vector3 (spriteDirection.x, spriteDirection.y, 0);
			

	}
}