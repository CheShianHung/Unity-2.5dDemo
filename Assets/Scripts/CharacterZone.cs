using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterZone : MonoBehaviour {

	public bool partnerCloseBy;

	void Start(){
		partnerCloseBy = true;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			partnerCloseBy = true;
//			print ("enter");
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			partnerCloseBy = false;
//			print ("exit");
		}
	}
}
