using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Transform player;
	private float smoothSpeed = 8f;
	public Vector3 offset;

	private Vector3 mousePosition;


	void LateUpdate ()
	{
		Vector3 mousePositionRaw = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePositionRaw);
		Vector3 directionalOffset = new Vector3 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, mousePosition.z - transform.position.z);
		if (Mathf.Abs(directionalOffset.x) > 3f) {
			if (directionalOffset.x >= 0) {
				directionalOffset.x = 3f;
			}
			if (directionalOffset.x < 0) {
				directionalOffset.x = -2f;
			}
		}
		if (Mathf.Abs(directionalOffset.y) > 3f) {
			if (directionalOffset.y >= 0) {
				directionalOffset.y = 3f;
			}
			if (directionalOffset.y < 0) {
				directionalOffset.y = -3f;
			}
		}
		if (Mathf.Abs(directionalOffset.z) > 3f) {
			if (directionalOffset.z >= 0) {
				directionalOffset.z = 3f;
			}
			if (directionalOffset.z < 0) {
				directionalOffset.z = -3f;
			}
		}
		Vector3 desiredPosition = player.position + offset + directionalOffset;
		Vector3 smoothPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothPosition;

	}

}
