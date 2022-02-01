using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour {
	public int index;
	public Text countText;
	private bool isCursorFollow = false;
	private InventoryUI inventoryUI;
	private Camera _camera;
	private ItemsFunctions itemsFunctions;

	// Use this for initialization
	void Start () {
		inventoryUI = transform.parent.GetComponent<InventoryUI> ();
		_camera = inventoryUI.cameraBody.GetComponent<Camera> ();
		countText = transform.Find ("Count").GetComponent<Text> ();
		itemsFunctions = transform.parent.GetComponent<ItemsFunctions> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectCell(){
		Transform scriptFunctionObject_Transform = transform.Find("ScriptFunctionObject");
		if (scriptFunctionObject_Transform != null) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				scriptFunctionObject_Transform.BroadcastMessage ("DropItem", index);
			} else {
				scriptFunctionObject_Transform.BroadcastMessage ("UseItem", index);
			}
		}
	}
}
