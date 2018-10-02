using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	// Public variables used in controlling the player 
	public float playerSpeed = 10f;
	public float dashTimer;
	public float dashDelay;
	//private int dashDirection;
	private Rigidbody2D playerRigidBody;
	public GameObject player;
	private Vector2 spriteDirection;
	private Vector2 playerDirection;

	void Start () {
		playerRigidBody = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		/*if (Input.GetKey (KeyCode.W)) {
			Move (Vector2.up);
		}
		if (Input.GetKey (KeyCode.S)) {
			Move (Vector2.down);
		}
		if (Input.GetKey (KeyCode.A)) {
			Move (Vector2.left);
		}
		if (Input.GetKey (KeyCode.D)) {
			Move (Vector2.right);
		}

		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			Dash ();
		}
		if (dashTimer >= 0) {
			dashTimer -= Time.deltaTime;
		}*/

		FaceMouse ();
	}

	/*void Move (Vector2 moveDirection) {
		Vector2 playerDirection = moveDirection;
		if (playerDirection.sqrMagnitude > 1) {
			player.transform.Translate (playerDirection * playerSpeed * Mathf.Sqrt(2) * Time.deltaTime);
		} else {
			player.transform.Translate (playerDirection * playerSpeed * Time.deltaTime);
		}
	}*/

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		playerRigidBody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal")* playerSpeed, 0.99f), Mathf.Lerp(0, Input.GetAxis("Vertical")* playerSpeed, 0.99f));
	}

	void FaceMouse () {
		Vector2 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

		spriteDirection = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		transform.up = spriteDirection;
	}

	void MachineGun () {

	}

	void Claw () {

	}

	void OverloadGun () {

	}

	void Dash () {
		Vector3 dashDirection = new Vector3 (spriteDirection.x, spriteDirection.y, 0);

		if (dashTimer <= 0) {
			//player.transform.Translate (dashDirection.normalized * 1000f * Time.deltaTime);
			//player.transform.position = Vector2.Lerp (player.transform.position, player.transform.position + dashDirection.normalized * 1000f * Time.deltaTime, Time.deltaTime);
			//playerRigidBody.AddForce (transform.up * 5f);
			dashTimer = dashDelay;
		}
	}
}