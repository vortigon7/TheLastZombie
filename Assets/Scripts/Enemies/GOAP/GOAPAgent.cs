using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public sealed class GOAPAgent : MonoBehaviour {

	private FSM stateMachine;

	private FSM.FSMState idleState;
	private FSM.FSMState moveToTargetState;
	private FSM.FSMState performActionState;

	private HashSet<GOAPAction> availableActions;
	private Queue<GOAPAction> currentActions;

	private IGOAP dataProvider;

	private GOAPPlanner planner;

	// Use this for initialization
	void Start () {
		stateMachine = new FSM ();
		availableActions = new HashSet<GOAPAction> ();
		currentActions = new Queue<GOAPAction> ();
		planner = new GOAPPlanner ();
		findDataProvider ();
		createIdleState ();
		createMoveToTargetState ();
		createPerformActionState ();
		stateMachine.pushState (idleState);
		loadActions ();

	}
	
	// Update is called once per frame
	void Update () {
		stateMachine.Update (this.gameObject);
	}

	public void addAction (GOAPAction a) {
		availableActions.Add (a);
	}

	public GOAPAction getAction (Type action) {
		foreach (GOAPAction g in availableActions) {
			if (g.GetType ().Equals (action)) {
				return g;
			}
		}
		return null;
	}

	public void removeAction (GOAPAction action) {
		availableActions.Remove (action);
	}

	private bool hasActionPlan () {
		return currentActions.Count > 0;
	}

	private void createIdleState () {
		idleState = (fsm, gameObj) => {
			// GOAP planning

			// Get the world state and the goal we want to plan for
			HashSet<KeyValuePair<string, object>> worldState = dataProvider.getWorldState ();
			HashSet<KeyValuePair<string, object>> goal = dataProvider.createGoalState ();

			// Make a plan with the actions the AI wants to do in order
			Queue<GOAPAction> plan = planner.plan (gameObject, availableActions, worldState, goal);
			if (plan != null) {
				// We have a plan!
				currentActions = plan;
				dataProvider.planFound (goal, plan);

				fsm.popState(); // Clear current state
				fsm.pushState(performActionState); // Change to performActionState
			} else {
				// We don't have a plan
				dataProvider.planFailed (goal);
				fsm.popState ();
				fsm.pushState (idleState); // Back to square one
			}

		};
	}

	private void createMoveToTargetState () {
		moveToTargetState = (fsm, gameObj) => {
			// Moves AI towards target

			GOAPAction action = currentActions.Peek ();

			// Action requires a target but has none. Go back to planning stage
			if (action.requiresInRange () && action.rTarget == null) {
				fsm.popState (); // Clear moveToTargetState
				fsm.popState (); // Clear performActionState
				fsm.pushState (idleState);
				return;
			}

			// If the moveAgent variable becomes true (which tells us if the AI reached the target), clear moveToTargetState
			if (dataProvider.moveAgent (action)) {
				fsm.popState ();
			}
		};
	}

	private void createPerformActionState () {
		performActionState = (fsm, gameObj) => {
			// Perform the action
			if (!hasActionPlan()) {
				// No actions to perform
				fsm.popState ();
				fsm.pushState (idleState);
				dataProvider.actionsFinished ();
				return;
			}

			GOAPAction action = currentActions.Peek ();
			if (action.isDone ()) {
				// The action is done. We can remove it, onto the next one!
				currentActions.Dequeue ();
			}

			if (hasActionPlan ()) {
				// Perform the next action
				action = currentActions.Peek ();
				bool inRange = action.requiresInRange () ? action.isInRange () : true;

				if (inRange) {
					// We're in range, so perform the action
					bool success = action.perform (gameObj);

					if (!success) {
						// Action failed, we need to plan again
						fsm.popState ();
						fsm.pushState (idleState);
						dataProvider.planAborted (action);
					}
				} else {
					// We need to move to our target first
					// Let's make the AI move
					fsm.pushState (moveToTargetState);
				}

			} 
			// If all fails, change back to idleState (planning stage)
			else {
				fsm.popState ();
				fsm.pushState (idleState);
				dataProvider.actionsFinished ();
			}
		};
	}

	// Finds our data provider, which is always the IGOAP.cs
	private void findDataProvider () {
		foreach (Component comp in gameObject.GetComponents(typeof(Component))) {
			if (typeof(IGOAP).IsAssignableFrom (comp.GetType ())) {
				dataProvider = (IGOAP)comp;
				return;
			}
		}
	}

	// Loads action components into an array called availableActions
	private void loadActions () {
		GOAPAction[] actions = gameObject.GetComponents<GOAPAction> ();
		foreach (GOAPAction a in actions) {
			availableActions.Add (a);
		}
	}

}
