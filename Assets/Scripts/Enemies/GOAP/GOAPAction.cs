using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPAction : MonoBehaviour {

	/*
	 * This script will set every way an action can behave
	 * Basically a superclass for all actions
	 */

	private HashSet<KeyValuePair<string, object>> preconditions; // The preconditions needed for an action to run
	private HashSet<KeyValuePair<string, object>> effects; // The change to the state after the action has run

	private bool inRange = false; // This checks whether the agent is in range of the target it needs to perform an action on

	public float cost = 1f; // This is the cost of an action (should be changed in the action component itself, inside the ActionName()

	public GameObject target; // This is the target an action is performed upon; can be null (if the action doesn't need a target)

	public GOAPAction () {
		preconditions = new HashSet<KeyValuePair<string, object>> ();
		effects = new HashSet<KeyValuePair<string, object>> ();
	}

	// Reset every GOAPAction variables
	public void doReset () {
		inRange = false;
		target = null;
		reset ();
	}

	public abstract void reset (); // Reset any variables that need to be reset before planning happens again

	public abstract bool isDone (); // Checks if the action is done

	public abstract bool checkProceduralPrecondition (GameObject agent);

	/**
     * Run the action.
     * Returns True if the action performed successfully or false
     * if something happened and it can no longer perform. In this case
     * the action queue should clear out and the goal cannot be reached.
     */
	public abstract bool perform (GameObject agent);

	public abstract bool requiresInRange (); // Does the action require to be in range of a target game object?

	// Returns a true/false value depending on if the agent is in range of the target
	public bool isInRange () {
		return inRange;
	}

	// Sets the instance's inRange variable
	public void setInRange (bool inRange) {
		this.inRange = inRange;
	}

	// Adds a precondition, requires the name of precondition (string key) and the value (object value)
	public void addPrecondition(string key, object value) {
		preconditions.Add (new KeyValuePair<string, object> (key, value));
	}

	// Removes a precondition, requires the name of precondition (string key)
	public void removePrecondition (string key) {
		KeyValuePair<string, object> remove = default(KeyValuePair<string, object>); // Defines a variable (remove) that stores the precondition that will be removed

		// For each kvp (key-value-pair) in the variable preconditions, kvp's key string will be the string of the key that will be removed
		foreach (KeyValuePair<string, object> kvp in preconditions) {
			if (kvp.Key.Equals (key)) {
				remove = kvp;
			}
		}

		// If the default KeyValuePair's parameters aren't the remove variable's parameters, remove the remove variable's parameters from our preconditions
		if (!default(KeyValuePair<string, object>).Equals (remove)) {
			preconditions.Remove (remove);
		}
	}

	// Adds a new effect to the planner
	public void addEffect (string key, object value) {
		effects.Add (new KeyValuePair<string, object> (key, value));
	}

	public void removeEffect (string key) {
		KeyValuePair<string, object> remove = default(KeyValuePair<string, object>); // Defines a variable (remove) that stores the effect that will be removed

		// For each kvp (key-value-pair) in the variable effects, kvp's key string will be the string of the key that will be removed
		foreach (KeyValuePair<string, object> kvp in effects) {
			if (kvp.Key.Equals (key)) {
				remove = kvp;
			}
		}

		// If the default KeyValuePair's parameters aren't the remove variable's parameters, remove the remove variable's parameters from our effects
		if (!default(KeyValuePair<string, object>).Equals (remove)) {
			effects.Remove (remove);
		}
	}

	// Returns the preconditions
	public HashSet<KeyValuePair<string, object>> Preconditions {
		get {
			return preconditions;
		}
	}

	// Returns the effects
	public HashSet<KeyValuePair<string, object>> Effects {
		get {
			return effects;
		}
	}

}
