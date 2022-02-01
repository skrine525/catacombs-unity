using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IBF : MonoBehaviour {
	public GameObject ScriptFunctionObject;
	public string methodName;
	public string buttonText;
	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = buttonText;
	}

	public void OnClickButton(){
		ScriptFunctionObject.BroadcastMessage (methodName);
	}
}
