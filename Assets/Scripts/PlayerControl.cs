using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	// Public variables used in controlling the player 
	public float pSpeed = 50f;
	public float maxSpeed = 50f;
	public float dashTimer;
	public float dashDelay;
	public float mouseAngle;

	//private int dashDirection;
	private Rigidbody2D pRigidBody;
	public GameObject player;
	private Vector2 spriteDirection;
	private Vector2 pDirection;
	private Vector2 pVelocityOld;
	private Vector2 pMovement;
	private bool pDashState;

	void Start () {
		pRigidBody = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void Update () {

		if (dashTimer >= -1) {
			dashTimer -= Time.deltaTime;
		}

		FaceMouse ();
		int n = 0;
		while (n < (3 * Time.deltaTime)) {
			Debug.Log (pRigidBody.velocity.magnitude);
			n++;
		}

		pVelocityOld = pRigidBody.velocity;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float accMulti = 1 - (pRigidBody.velocity.magnitude / maxSpeed);
		//Vector2 mouseCircle = (new Vector2 (Mathf.Sin (mouseAngle), Mathf.Cos (mouseAngle)) * pSpeed);
		if (pDashState) {
			pMovement = Vector2.zero;
			if (pRigidBody.velocity.magnitude < 5) {
				pDashState = false;
			}
		} else {
			pMovement = new Vector2 (moveHorizontal * pSpeed * accMulti * Time.deltaTime, moveVertical * pSpeed * accMulti * Time.deltaTime);
		}
		pRigidBody.AddForce (pMovement);

		if ((pRigidBody.velocity.magnitude < 1) && (pVelocityOld.sqrMagnitude > pRigidBody.velocity.sqrMagnitude)) {
			pRigidBody.velocity -= pRigidBody.velocity * Time.deltaTime * 300f;
		}

		if ((Input.GetKeyDown (KeyCode.Mouse1)) && (dashTimer <= 0)) {
			//pRigidBody.velocity = Vector2.zero;
			pDashState = true;
			pRigidBody.AddForce (spriteDirection.normalized * pSpeed * 1f);
			dashTimer = dashDelay;
			//Debug.Log (pRigidBody);
		}
			

	}

	void FaceMouse () {
		Vector2 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

		spriteDirection = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		mouseAngle = Mathf.Atan2 (spriteDirection.x, spriteDirection.y);
		 
		transform.up = spriteDirection;
	}
		
	void Dash () {
		Vector3 dashDirection = new Vector3 (spriteDirection.x, spriteDirection.y, 0);
			

	}
}