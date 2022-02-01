using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {
	private InventoryController inventory;

	// Use this for initialization
	void Start () {
		inventory = GetComponent<InventoryController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DrawUI(ChestUI chestUI){
		chestUI.Draw(inventory);
	}
}
