using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game Manager

public class PlayerSwitch : MonoBehaviour {

	public GameObject c1;
	public GameObject c2;
	public bool switchToC1;

	private CameraMovement cameraMovement;
	private bool playerStay;

	// Use this for initialization
	void Start () {
		switchToC1 = true;
		playerStay = false;
		cameraMovement = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
		C1Turn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire3")) {
			switchToC1 = !switchToC1;
			if (switchToC1) {
				C1Turn ();
			} else {
				C2Turn ();
			}
		}
		if (Input.GetButtonDown ("PlayerStay")) {
			if (!playerStay) {
				playerStay = true;
				if (switchToC1) {
					c2.GetComponent<PlayerFollow> ().enabled = false;
				} else {
					c1.GetComponent<PlayerFollow> ().enabled = false;
				}
			} else {
				if (GameObject.Find ("CharacterZone1").GetComponent<CharacterZone> ().partnerCloseBy) {
					playerStay = false;
					if (switchToC1) {
						c2.GetComponent<PlayerFollow> ().enabled = true;
					} else {
						c1.GetComponent<PlayerFollow> ().enabled = true;
					}
				}
			}
		}
	}

	private void C1Turn() {
		cameraMovement.Character = c1;
		c1.GetComponent<PlayerMovement> ().enabled = true;
		c1.GetComponent<PlayerFollow> ().enabled = false;
		c2.GetComponent<PlayerMovement> ().enabled = false;
		if (!playerStay) {
			c2.GetComponent<PlayerFollow> ().enabled = true;
		} else {
			c2.GetComponent<PlayerFollow> ().enabled = false;
		}
	}

	private void C2Turn() {
		cameraMovement.Character = c2;
		c1.GetComponent<PlayerMovement> ().enabled = false;
		if (!playerStay) {
			c1.GetComponent<PlayerFollow> ().enabled = true;
		} else {
			c1.GetComponent<PlayerFollow> ().enabled = false;
		}
		c2.GetComponent<PlayerMovement> ().enabled = true;
		c2.GetComponent<PlayerFollow> ().enabled = false;
	}
}
