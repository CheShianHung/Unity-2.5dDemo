using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//Direction Rotation
	//Forward: 0 => 1
	//Right: 90 => 1
	//Down: 180 => -1
	//Left: 270 => -1

	public float moveSpeed; //5f
	public float jumpInitialSpeed; //5f
	public float jumpGravity; //19f
	public float jumpAirTime; //1.5f
	public float jumpDownForce; //1.2f

	private Rigidbody rb;
	private bool jumping;
	private bool jumpOnGround;
	private float jumpHoldingTimer;
	private float jumpCurrentForce;
	private float jumpHoldingGravity;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		jumping = false;
		jumpOnGround = true;
		jumpHoldingTimer = 0f;
		jumpCurrentForce = 0f;
		jumpHoldingGravity = 0f;
	}

	void Update(){

	}

	void FixedUpdate(){
		Movement ();
		Jumping ();
	}

	private void Movement(){
		float h = Input.GetAxis  ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		float rawH = Input.GetAxisRaw ("Horizontal");
		float rawV = Input.GetAxisRaw ("Vertical");

		if (rawH != 0f || rawV != 0f) {
			float angle = Mathf.Rad2Deg * Mathf.Atan (h / v);
			if (h >= 0f && v <= 0f)
				angle -= 180f;
			if (h <= 0f && v <= 0f)
				angle += 180f;
			if (v == 0f && h > 0f)
				angle = 90f;
			else if (v == 0f)
				angle = -90f;
			if (h == 0 && v < 0f)
				angle = 180f;

			transform.localEulerAngles = new Vector3 (0f, angle, 0f);
			rb.MovePosition (rb.position + transform.forward * moveSpeed * Time.deltaTime);

		}
	}

	private void Jumping() {//Reused from CBH
		//while on ground
		if (jumpOnGround) {
			if (Input.GetButton("Jump")){
				jumpHoldingTimer = 0f;
				jumpCurrentForce = jumpInitialSpeed;
				jumpHoldingGravity = jumpGravity * jumpAirTime;
				jumping = true;
				jumpOnGround = false;
			}
		}

		//When holding
		if (Input.GetButton("Jump") && !jumpOnGround && jumpHoldingTimer < 0.2f && Time.timeScale != 0f) {
			jumpHoldingTimer += Time.deltaTime;
			jumpHoldingGravity -= Time.deltaTime;
			jumpCurrentForce += jumpHoldingGravity * Time.deltaTime;
		}

		//During jumping and not yet touch the ground
		if (jumping && !jumpOnGround) {
			Vector3 moveDirection = Vector3.zero;
			jumpCurrentForce -= jumpGravity * Time.deltaTime * jumpDownForce;
			moveDirection.y = jumpCurrentForce;
			rb.velocity = moveDirection;
		}
	}

	private void OnCollisionStay(Collision other) {
		if (other.collider.tag == "Ground" || other.collider.tag == "Player") {
			jumpHoldingTimer = 0f;
			jumpOnGround = true;
			jumping = false;
		}
	}

	private void OnTriggerEnter(Collider other) {
		CameraMovement cameraMovement = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
		string[] strAry = other.gameObject.name.Split('_');
		string parentObject = other.gameObject.transform.parent.gameObject.name;

		if (parentObject == "Transform") {
			cameraMovement.setCameraOffset (float.Parse (strAry [1]), float.Parse (strAry [2]), float.Parse (strAry [3]));
		} else if (parentObject == "Quaternion") {
			cameraMovement.setCameraQuaternion (float.Parse (strAry [1]), float.Parse (strAry [2]), float.Parse (strAry [3]));
		}

	}
}
