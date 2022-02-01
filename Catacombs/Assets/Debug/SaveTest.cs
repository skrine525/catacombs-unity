using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F5)) {
			SaveManager.SaveGame ();
		} else if (Input.GetKeyDown (KeyCode.F6)) {
			SaveManager.LoadGame ();
		}
	}
}
