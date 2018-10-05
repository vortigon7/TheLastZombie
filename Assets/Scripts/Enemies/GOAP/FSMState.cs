using System.Collections;
using UnityEngine;

public interface FSMState {

	// This is an interface that ties defines a state in the FSM
	void Update (FSM fsm, GameObject gameObject);

}
