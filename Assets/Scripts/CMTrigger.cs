using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Objects in Transform and Quaternion objects

public class CMTrigger : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			PlayerSwitch playerSwitch = GameObject.Find ("Game Manager").GetComponent<PlayerSwitch> ();
			if ((playerSwitch.switchToC1 && other.name == "Character1") || (!playerSwitch.switchToC1 && other.name == "Character2")) {
				CameraMovement cameraMovement = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
				string[] strAry = gameObject.name.Split ('_');
				string parentObject = gameObject.transform.parent.gameObject.name;
//				print (parentObject);

				if (parentObject == "Transform") {
					cameraMovement.setCameraOffset (float.Parse (strAry [1]), float.Parse (strAry [2]), float.Parse (strAry [3]));
				} else if (parentObject == "Quaternion") {
					cameraMovement.setCameraQuaternion (float.Parse (strAry [1]), float.Parse (strAry [2]), float.Parse (strAry [3]));
				}
			}
		}
	}

}
