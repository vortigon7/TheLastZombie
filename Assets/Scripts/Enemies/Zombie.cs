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
}
