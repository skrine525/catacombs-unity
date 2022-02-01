using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterections : MonoBehaviour {
	[SerializeField] private Camera _camera;
	[SerializeField] private InventoryUI inventoryUI;
	[SerializeField] private CraftUI craftUI;
	[SerializeField] private ChestUI chestUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			if (Input.GetAxis ("Inventory") != 0) {
				if (!inventoryUI.getActive ()) {
					inventoryUI.Draw ();
				} else {
					inventoryUI.Erase ();
				}
			} else if (Input.GetAxis ("Inventory") != 0) {
				if (inventoryUI.getActive ()) {
					inventoryUI.Erase ();
				}
			} else if (Input.GetAxis ("Cancel") != 0) {
				if (craftUI.getActive ()) {
					craftUI.Erase ();
				} else if (inventoryUI.getActive ()) {
					inventoryUI.Erase ();
				} else if (chestUI.getActive()){
					chestUI.Erase();
				} else {
					Application.Quit ();
				}
			}
		}

		if (Input.GetMouseButtonDown(1)) {
			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 3f)){
				GameObject hitObject = hit.transform.gameObject;
				if (hit.transform.GetComponent<ItemPicker> () != null) {
					hit.transform.GetComponent<ItemPicker> ().inventory = GetComponent<InventoryController> ();
					hit.transform.GetComponent<ItemPicker> ().Pick ();
				} else if (hitObject.tag == "CraftTable") {
					if (!craftUI.getActive ()) {
						craftUI.Draw ();
					}
				} else if (hitObject.tag == "Chest"){
					hitObject.GetComponent<ChestController>().DrawUI(chestUI);
				}
			}
		}
	}
}
