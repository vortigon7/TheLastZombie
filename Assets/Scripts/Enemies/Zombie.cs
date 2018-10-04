using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyComponent {

	public Rigidbody2D zRigidBody;

	public override HashSet<KeyValuePair<string, object>> createGoalState () {
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>> ();

		goal.Add (new KeyValuePair<string, object> ("moveToPlayer", true));
		return goal;
	}

	public override bool moveAgent (GOAPAction nextAction) {
		float step = 1f * Time.deltaTime;
		gameObject.transform.position = Vector2.MoveTowards (gameObject.transform.position, nextAction.target.transform.position, step);
		if (gameObject.transform.position.Equals (nextAction.target.transform.position)) {
			Debug.Log ("true");
			//nextAction.setInRange (true);
			return true;
		} else {
			return false;
		}
	}
}
