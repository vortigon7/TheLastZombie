using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	// Player stats
	public float pHealth; // Defines the player's health
	public float pMaxHealth; // Defines the player's maximum health
	public int pFireRate; // Defines the fire rate of the Plasma Gun
	public int pBulletDamage; // Defines the fire damage of the Plasma Gun
	public int pBulletDrain; // Defines how much health a bullet costs
	public int pClawRate; // Defines how long it takes for the Claw to become usable again
	public int pClawDamage; // Defines the Claw's damage
	public float pSpeed; // Defines the player's movement speed
	public float pFireTimer; // pFireTimer is the countdown of the Plasma Gun
	public float pClawTimer; // pClawTimer is the countdown of the Claw

	// Player healthbar
	public Slider healthBar; // This is the player's healthbar fill on the HUD

	// Death screen
	public GameObject deathScreen; // This is the death screen

	// Adding PlayerControl.cs script variables; used for disabling controls on death
	private PlayerControl pControl; // This is the PlayerControl.cs script

	// Use this for initialization
	void Start () {
		pControl = GetComponent<PlayerControl> ();
		pHealth = pMaxHealth; // Sets current health to maximum health
		healthBar.value = CalculateHealth (); // Sets healthbar at current health <-- probably not the most efficient but I'll leave it for now
		IsPlayerDead (); // Checks at start if the player is dead. Sets Death Screen inactive if not <-- To avoid a bug of showing Death Screen at beginning of scene

		// Here will come all global variables that will set the player's stats back to where they were at the last checkpoint
	}
	
	// Update is called once per frame
	void Update () {
		// This is for seeing if the healthbar works properly
		if (Input.GetMouseButtonDown(0)) {
			DebugDamage(6);
		}
		healthBar.value = CalculateHealth (); // Constantly checks if the player's health value changes
		IsPlayerDead (); // Constantly checks if the player is dead
	}

	// A float that returns the percentage of the player's health
	float CalculateHealth () {
		return pHealth / pMaxHealth;

	}

	// The code for testing damage infliction
	void DebugDamage (int damage) {
		pHealth -= damage;
	}

	// Checks if the player is dead and sets the Death Screen active if it is
	void IsPlayerDead () {
		if (pHealth <= 0) {
			deathScreen.SetActive (true);
			pControl.enabled = false;

		} else {
			deathScreen.SetActive (false);
			pControl.enabled = true;
		}
	}
}
