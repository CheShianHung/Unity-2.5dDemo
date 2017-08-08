using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main Camera

public class CameraMovement : MonoBehaviour {
	
	public GameObject Character;
	public Vector3 offset;
	public float moveSpeed; 
	public float lerpOffsetTimer;
	public float lerpQuaternionTimer;
	public float lerpOffsetInSecond;
	public float lerpQuaternionInSecond;

	private Vector3 targetOffset;
	private Quaternion targetQuaternion;
	private bool lerpOffsetSwitch;
	private bool lerpQuaternionSwitch;

	void Start () {
		lerpOffsetSwitch = false;
		lerpQuaternionSwitch = false;
		targetOffset = Vector3.zero;
		targetQuaternion = Quaternion.identity;
	}
	
	void Update () {

	}

	void FixedUpdate(){
		if (lerpOffsetSwitch) {
			LerpOffset ();
		}
		if (lerpQuaternionSwitch) {
			LerpQuaternion ();
		}
		MoveAlongWithCharacter ();
	}

	private void MoveAlongWithCharacter(){
		Vector3 targetPosition = Character.transform.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetPosition, moveSpeed * Time.deltaTime / 2);
	}

	private void LerpOffset(){
		offset = Vector3.Lerp(offset, targetOffset, lerpOffsetTimer);
		lerpOffsetTimer += Time.deltaTime / lerpOffsetInSecond;
		if(lerpOffsetTimer > 1){
			lerpOffsetSwitch = false;
		}
	}

	private void LerpQuaternion(){
		transform.rotation = Quaternion.Lerp (transform.rotation, targetQuaternion, lerpQuaternionTimer);
		lerpQuaternionTimer += Time.deltaTime / lerpQuaternionInSecond;
		if (lerpQuaternionTimer > 1) {
			lerpQuaternionSwitch = false;
		}
	}

	public void setCameraOffset(float x, float y, float z){
		targetOffset = new Vector3 (x, y, z);
		lerpOffsetTimer = 0f;
		lerpOffsetSwitch = true;
	}

	public void setCameraQuaternion(float x, float y, float z){
		if (x == -1)
			x = transform.rotation.x;
		if (y == -1)
			y = transform.rotation.y;
		if (z == -1)
			z = transform.rotation.z;
		targetQuaternion = Quaternion.Euler(x, y, z);
		lerpQuaternionTimer = 0f;
		lerpQuaternionSwitch = true;
	}
}
