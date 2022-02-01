using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUKA : MonoBehaviour {

	public int id;

	// Use this for initialization
	void Start () {
		List<InventoryController.ItemAttributes> list = new List<InventoryController.ItemAttributes>();
		list.Add(InventoryController.newItemAttribute("Inventory ID", id.ToString()));
		GetComponent<ItemPicker>().itemAttribs = list;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
