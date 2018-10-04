using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSM {

	private Stack<FSMState> stateStack = new Stack<FSMState> (); // Creates a new interface for storing states

	/**
	 * "Please feel free to assign any method that matches this signature to the delegate, and that will be called
	 * each time this delegate is mentioned in the code." -- Taken from stackoverflow
	 */
	public delegate void FSMState (FSM fsm, GameObject gameObject);  // We need to delegate this so the GOAP Agent can use it

	public void Update (GameObject gameObject) {
		if (stateStack.Peek () != null) {
			stateStack.Peek ().Invoke (this, gameObject);
		}
	}

	public void pushState (FSMState state) {
		stateStack.Push (state);
	}

	public void popState () {
		stateStack.Pop ();
	}

}
