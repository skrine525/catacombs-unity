using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
	public float SensitivityHor = 9.0f;
	public float SensitivityVert = 9.0f;
	public float MaximumVert = 45.0f;
	public float MinimumVert = -45.0f;
	private float RotationY = 0;
	private float RotationX = 0;

	public enum RotationAxis { MouseY = 0, MouseX= 1 };

	public RotationAxis axis = RotationAxis.MouseY;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (axis == RotationAxis.MouseY) {
			RotationY = Input.GetAxis ("Mouse X") * SensitivityHor;
			transform.Rotate (0, RotationY, 0);
		} else if (axis == RotationAxis.MouseX) {
			RotationX -= Input.GetAxis ("Mouse Y") * SensitivityVert;
			RotationX = Mathf.Clamp (RotationX, MinimumVert, MaximumVert);
			RotationY = transform.localEulerAngles.y;
			transform.localEulerAngles = new Vector3 (RotationX, RotationY, 0);
		}
	}
}
