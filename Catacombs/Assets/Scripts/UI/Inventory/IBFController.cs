using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBFController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void initScriptFunctionObject<T>(GameObject cell) where T : Component{
		Transform scriptFunctionObject_Transform = cell.transform.Find ("ScriptFunctionObject");
		if (scriptFunctionObject_Transform == null) {
			GameObject scriptFunctionObject = new GameObject ();
			scriptFunctionObject.name = "ScriptFunctionObject";
			scriptFunctionObject.transform.parent = cell.transform;
			scriptFunctionObject.AddComponent<T> ();
		}
	}

	public void resetScriptFunctionObject(GameObject cell){
		Transform scriptFunctionObject_Transform = cell.transform.Find ("ScriptFunctionObject");
		if (scriptFunctionObject_Transform != null) {
			Destroy (scriptFunctionObject_Transform.gameObject);
		}
	}
}
