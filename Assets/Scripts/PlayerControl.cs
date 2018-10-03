using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	// Public variables used in controlling the player 
	public float playerSpeed = 3;
	public float maxSpeed = 20;
	public float dashTimer;
	public float dashDelay;
	public float MouseAngle;

	//private int dashDirection;
	private Rigidbody2D playerRigidBody;
	public GameObject player;
	private Vector2 spriteDirection;
	private Vector2 playerDirection;
	private Vector2 DashMovement;

	void Start () {
		playerRigidBody = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void Update () {

		if (dashTimer >= -1) {
			dashTimer -= Time.deltaTime;
		}


		FaceMouse ();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float accelerationMultiplier = 1 - (playerRigidBody.velocity.magnitude / maxSpeed);
		Vector2 ZeroVector = new Vector2 (0f, 0f);
		Vector2 MouseCircle = (new Vector2 (Mathf.Sin (MouseAngle), Mathf.Cos (MouseAngle)) * 15.3f);
		Vector2 Movement = new Vector2(Input.GetAxis("Horizontal")* playerSpeed * accelerationMultiplier * Time.deltaTime, Input.GetAxis("Vertical") * playerSpeed * accelerationMultiplier * Time.deltaTime);
		playerRigidBody.AddForce (Movement);

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