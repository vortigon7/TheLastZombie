using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyComponent : MonoBehaviour, IGOAP {

	public float eHealth;
	public float eMaxHealth;
	public float eSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public HashSet<KeyValuePair<string, object>> getWorldState () {
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>> ();

		//worldData.Add (new KeyValuePair<string, object> ("playerHealth", (player.pHealth > 0)));
		//worldData.Add (new KeyValuePair<string, object> ("playerPosition", player.transform));

		return worldData;
	}

	public abstract HashSet<KeyValuePair<string, object>> createGoalState ();

	public void planFailed (HashSet<KeyValuePair<string, object>> failedGoal) {
		
	}

	public void planFound (HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions) {

	}

	public void actionsFinished () {
		
	}

	public void planAborted (GOAPAction aborter) {

	}

	public abstract bool moveAgent (GOAPAction nextAction);
}
