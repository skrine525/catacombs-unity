using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCheatMenu : MonoBehaviour {
	[SerializeField] private Dropdown IdSelecter;
	[SerializeField] private InputField Count;
	[SerializeField] private InventoryController playerInventory;
	private List<ItemsInfo.Id> itemsIds = new List<ItemsInfo.Id>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Draw(){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		gameObject.SetActive (true);
		List<string> itemsNames = new List<string> ();
		for (int i = 0; i < ItemsInfo.items.Length; i++) {
			if (ItemsInfo.items [i].id != ItemsInfo.Id.empty) {
				itemsNames.Add (ItemsInfo.items [i].name);
				itemsIds.Add (ItemsInfo.items [i].id);
			}
		}
		IdSelecter.AddOptions (itemsNames);
	}

	public void Erase(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		gameObject.SetActive (false);
		List<string> itemsNames = new List<string> ();
		IdSelecter.ClearOptions();
		itemsIds = new List<ItemsInfo.Id> ();
	}

	public void giveItem(){
		int count = int.Parse (Count.text);
		if (count != 0) {
			InventoryController.Results result = playerInventory.giveTheItem (itemsIds [IdSelecter.value], count);
			if (result == InventoryController.Results.Ok) {
				UINotificationController.createNewNotification ("CheatMenu", ItemsInfo.getInfo (itemsIds [IdSelecter.value]).name + " x" + count, 15);
			} else if (result == InventoryController.Results.There_is_no_place) {
				UINotificationController.createNewNotification ("CheatMenu", "There is no place", 15);
			}
		} else {
			UINotificationController.createNewNotification ("CheatMenu", "You can't use 0 in Count Input Field", 15);
		}
	}
}
