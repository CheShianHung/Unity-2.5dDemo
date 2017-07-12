using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {

	public GameObject playerToFollow;
	public int queueSizeLimit;

	private Queue<Vector3> transformQueue;
	private Vector3 targetStartPosition;
	private Vector3 targetPreviousPosition;
	private Vector3 startPosition;
	private float positionLerpFrame;

	void Start () {
		transformQueue = new Queue<Vector3> ();
		ResetQueue ();
	}
	
	void FixedUpdate () {
		Follow ();
		targetPreviousPosition = playerToFollow.transform.position;
	}

	void OnEnable() {
		ResetQueue ();
	}

	void OnDisable() {
		transformQueue = new Queue<Vector3> ();
	}

	private void Follow(){
		float rawH = Input.GetAxisRaw ("Horizontal");
		float rawV = Input.GetAxisRaw ("Vertical");

		if (rawH != 0f || rawV != 0f) {
			PositionUpdate ();
			RotationUpdate ();
		}
	}

	private void PositionUpdate(){
		Vector3 targetCurrentPosition = playerToFollow.transform.position;
		print (Mathf.Abs (targetCurrentPosition.x - targetPreviousPosition.x));
		if (Mathf.Abs (targetCurrentPosition.x - targetPreviousPosition.x) > 0.05f || Mathf.Abs (targetCurrentPosition.z - targetPreviousPosition.z) > 0.05f) {
			if (transformQueue.Count == queueSizeLimit) {
				Vector3 pastPosition = transformQueue.Dequeue ();
				transform.position = new Vector3 (pastPosition.x, transform.position.y, pastPosition.z);
			} else if (transformQueue.Count == 0) {
				targetStartPosition = playerToFollow.transform.position;
			} else {
				startPosition.y = transform.position.y;
				targetStartPosition.y = transform.position.y;
				transform.position = Vector3.Lerp (startPosition, targetStartPosition, positionLerpFrame);
				positionLerpFrame += (float)1f / queueSizeLimit;
			}
			transformQueue.Enqueue (playerToFollow.transform.position);
		} else {
			ResetQueue ();
		}
	}

	private void RotationUpdate(){
		Vector3 direction = playerToFollow.transform.position - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.LookRotation (direction);
	}

	private void ResetQueue(){
		startPosition = transform.position;
		targetStartPosition = playerToFollow.transform.position;
		transformQueue.Clear ();
		positionLerpFrame = 0f;
	}
		
}
