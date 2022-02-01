using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolFunc : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			if (Input.GetAxis ("Interection 1") != 0) {
				if (transform.Find ("Tool") != null) {
					transform.Find ("Tool").BroadcastMessage ("StartWork");
				}
			}
		}
	}
}
