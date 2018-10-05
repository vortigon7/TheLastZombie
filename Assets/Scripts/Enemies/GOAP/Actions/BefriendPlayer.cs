using System;
using UnityEngine;

public class BefriendPlayer : GOAPAction {

	private bool befriended = false;

	public BefriendPlayer () {
		addEffect ("moveToPlayer", true);
	}

	public override void reset () {
		befriended = false;
	}

	public override bool isDone () {
		return befriended;
	}

	public override bool requiresInRange () {
		return true;
	}

	public override bool checkProceduralPrecondition (GameObject agent) {
		agent = GameObject.Find ("Player");
		rTarget = agent;
		return agent != null;
	}

	public override bool perform (GameObject agent) {
		return true;
	}
}
