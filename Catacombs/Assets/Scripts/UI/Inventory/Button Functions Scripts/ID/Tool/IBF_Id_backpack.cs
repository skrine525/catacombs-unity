using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IBF_Id_backpack : MonoBehaviour {
	private int index;
	private Button button;
	public PlayerEquipmentController playerEquip;
	public InventoryController playerInventory;
	private GameObject pickaxePrefab;
	private GameObject pickaxeDropPrefab;
	private ItemsFunctions itemsFunctions;
	private IBFController ibfController;

	// Use this for initialization
	void Start () {
		pickaxePrefab = Resources.Load<GameObject> ("Prefabs/Tools/pickaxe");
		pickaxeDropPrefab = Resources.Load<GameObject>("Prefabs/Tools/pickaxe_drop");
		itemsFunctions = GetComponentInParent<ItemsFunctions> ();
		playerInventory = itemsFunctions.PlayerInventory;
		ibfController = GetComponentInParent<IBFController> ();

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void DropItem(int _index){
		index = _index;
		playerInventory.pickUpTheSomeItems (index, 1);
	}

	public void UseItem(int _index){
		if(playerInventory.getItemAttribute(_index, "Inventory ID") != null){
			UINotificationController.createNewNotification("Backpack", playerInventory.getItemAttribute(index, "Inventory ID"), 15);
		} else {
			UINotificationController.createNewNotification("Backpack", "Inventory ID не обнаружен в attribs", 15);
		}
	}
}

