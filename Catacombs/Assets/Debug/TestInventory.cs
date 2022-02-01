using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour {
    private InventoryController inventory;

	// Use this for initialization
	void Start () {
        inventory = GetComponent<InventoryController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			inventory.giveTheItem (ItemsInfo.Id.craft_table, 1);
		} else if (Input.GetKeyDown (KeyCode.X)) {
			inventory.giveTheItem (ItemsInfo.Id.bed, 1);
		} else if (Input.GetKeyDown (KeyCode.C)) {
			inventory.giveTheItem (ItemsInfo.Id.chest, 1);
		} else if (Input.GetKeyDown (KeyCode.V)) {
			inventory.giveTheItem (ItemsInfo.Id.log, 1);
		} else if (Input.GetKeyDown (KeyCode.B)) {
			inventory.giveTheItem (ItemsInfo.Id.board, 1);
		} else if (Input.GetKeyDown (KeyCode.N)) {
			inventory.giveTheItem (ItemsInfo.Id.strawberry, 1);
		} else if (Input.GetKeyDown (KeyCode.M)) {
			inventory.giveTheItem (ItemsInfo.Id.cherry, 1);
		} else if (Input.GetKeyDown (KeyCode.RightControl)) {
			inventory.giveTheItem (ItemsInfo.Id.water_bottle, 1);
		}  else if (Input.GetKeyDown (KeyCode.P)) {
			Debug.Log(inventory.giveTheItem (ItemsInfo.Id.iron_ore, 1));
		} else if (Input.GetKeyDown (KeyCode.O)) {
			inventory.giveTheItem (ItemsInfo.Id.stick, 1);
		} else if (Input.GetKeyDown (KeyCode.I)) {
			inventory.giveTheItem (ItemsInfo.Id.coal, 1);
		} 
	}
}
