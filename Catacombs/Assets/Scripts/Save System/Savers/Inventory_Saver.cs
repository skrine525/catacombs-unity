using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Saver : MonoBehaviour {
	private SaveID saveID;
	private InventoryController inventory;

	// Use this for initialization

	void Awake(){
		saveID = GetComponent<SaveID> ();
		inventory = GetComponent<InventoryController> ();
		data.cells = new InventoryController.InventoryCell[inventory.items.Length];
	}
	
	// Update is called once per frame
	void Update () {
	}

	///////////////////////////////////////////
	///Saver
	private class SaverClassData{
		public InventoryController.InventoryCell[] cells;
	}
	private SaverClassData data = new SaverClassData();

	public void SaveGameData(){
		data = new SaverClassData();
		data.cells = new InventoryController.InventoryCell[inventory.items.Length];
		for(int i = 0; i < inventory.items.Length; i++){
			data.cells[i] = inventory.items[i];
		}
		SaveManager.save.SetData(saveID.ID, "inventory", data);
	}

	public void LoadGameData(){
		data = SaveManager.save.GetData<SaverClassData>(saveID.ID, "inventory");
		if(data != null){
			for(int i = 0; i < data.cells.Length; i++){
				inventory.pickUpTheSomeItems(i, inventory.items[i].count);
				inventory.giveTheItem(data.cells[i].id, data.cells[i].count, data.cells[i].attribs);
			}
		}
	}
}