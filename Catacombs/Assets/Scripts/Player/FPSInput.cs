using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {
	private Vector3 Movement;
	public bool isInput = true;
	public float WalkSpeed = 5.0f;
	public float RunSpeed = 10.0f;
	private float Speed = 0;
	public float Gravity = -9.8f;
	private float GravityTemp = 0;
	private CharacterController charController;
	private float DeltaX;
	private float DeltaZ;

	// Use this for initialization
	void Start () {
		charController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isInput) {
			if (Input.GetAxis ("Run") != 0) {
				Speed = RunSpeed;
			} else if (!Input.GetKeyUp (KeyCode.LeftShift)) {
				Speed = WalkSpeed;
			}

			DeltaZ = Input.GetAxis ("Vertical") * Speed;
			DeltaX = Input.GetAxis ("Horizontal") * Speed;
		} else {
			DeltaZ = 0;
			DeltaX = 0;
		}
		Movement = new Vector3 (DeltaX, 0, DeltaZ);
		Movement = Vector3.ClampMagnitude (Movement, Speed);
		Movement.y = Gravity;
		Movement *= Time.deltaTime;
		Movement = transform.TransformDirection (Movement);
		charController.Move (Movement);
	}
}
