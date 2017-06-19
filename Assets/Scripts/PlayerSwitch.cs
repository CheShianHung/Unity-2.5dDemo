using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour {

	public GameObject c1;
	public GameObject c2;

	private CameraMovement cameraMovement;
	private bool switchToC1;

	// Use this for initialization
	void Start () {
		cameraMovement = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
		C1Turn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire3")) {
			if (switchToC1) {
				C2Turn ();
			} else {
				C1Turn ();
			}
		}
	}

	private void C1Turn() {
		switchToC1 = true;
		cameraMovement.Character = c1;
		c1.GetComponent<PlayerMovement> ().enabled = true;
		c1.GetComponent<PlayerFollow> ().enabled = false;
		c2.GetComponent<PlayerMovement> ().enabled = false;
		c2.GetComponent<PlayerFollow> ().enabled = true;
	}

	private void C2Turn() {
		switchToC1 = false;
		cameraMovement.Character = c2;
		c1.GetComponent<PlayerMovement> ().enabled = false;
		c1.GetComponent<PlayerFollow> ().enabled = true;
		c2.GetComponent<PlayerMovement> ().enabled = true;
		c2.GetComponent<PlayerFollow> ().enabled = false;
	}
}
