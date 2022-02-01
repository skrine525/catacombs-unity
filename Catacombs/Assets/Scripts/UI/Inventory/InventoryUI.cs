using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
	private Image image;
	private bool isActive;
	public bool isCanDraw = true;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject pointer;
	[SerializeField] private CraftUI craftUI;
	public GameObject cameraBody;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		isActive = gameObject.activeSelf;
	}
	
	// Update is called once per frame
	void Update () {
		InventoryController.InventoryCell[] playerItems = player.GetComponent<InventoryController> ().items;
		for (int i = 0; i < playerItems.Length; i++) {
			GameObject itemButtonObject = null;
			for (int j = 0; j < transform.childCount; j++) {
				if (transform.GetChild (j).name == "ItemButton") {
					if (transform.GetChild (j).GetComponent<ItemIcon> ().index == i) {
						itemButtonObject = transform.GetChild (j).gameObject;
					}
				}
			}

			if (itemButtonObject != null) {
				itemButtonObject.GetComponent<Image> ().sprite = ItemsInfo.getInfo (playerItems [i].id).image;
				if (playerItems [i].count != 0) {
					itemButtonObject.GetComponent<ItemIcon> ().countText.text = playerItems [i].count.ToString ();
				} else {
					itemButtonObject.GetComponent<ItemIcon> ().countText.text = "";
				}
			}
		}
	}

	public bool getActive(){
		return gameObject.activeSelf;
	}

	public void Draw(){
		if (isCanDraw) {
			pointer.SetActive (false);
			gameObject.SetActive (true);
			player.GetComponent<FPSInput> ().isInput = false;
			player.GetComponent<MouseLook> ().enabled = false;
			cameraBody.GetComponent<MouseLook> ().enabled = false;
			cameraBody.transform.Find ("Tool Body").GetComponent<ToolFunc> ().enabled = false;
			craftUI.isCanDraw = false;
			ItemsFunctions itemsFunctions = GetComponent<ItemsFunctions> ();
			itemsFunctions.selectedIndex = -1;
			//itemsFunctions.ResetCell ();
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			isActive = true;
		}
	}

	public void Erase(){
		if (isCanDraw) {
			gameObject.SetActive (false);
			pointer.SetActive (true);
			player.GetComponent<FPSInput> ().isInput = true;
			player.GetComponent<MouseLook> ().enabled = true;
			cameraBody.GetComponent<MouseLook> ().enabled = true;
			cameraBody.transform.Find ("Tool Body").GetComponent<ToolFunc> ().enabled = true;
			craftUI.isCanDraw = true;
			ItemsFunctions itemsFunctions = GetComponent<ItemsFunctions> ();
			itemsFunctions.selectedIndex = -1;
			itemsFunctions.ResetCells ();
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			for (int i = 0; i < transform.childCount; i++) {
				if (transform.GetChild (i).name == "ItemButton") {
					ItemIcon icon = transform.GetChild (i).GetComponent<ItemIcon> ();
					//icon.GetComponent<Image> ().sprite = null;
				}
			}
			isActive = false;
		}
	}
}
