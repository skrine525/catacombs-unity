using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemButtonController : MonoBehaviour {
	private enum Mode { ChestInventory, PlayerInventory }
	[HideInInspector] public ChestUI chestUI;
	[HideInInspector] public int index;
	[SerializeField] private Mode mode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Click(){
		if(Input.GetKey(KeyCode.LeftShift)){
			if(mode == Mode.ChestInventory){
				chestUI.TransferAll_ChestToPlayer(index);
			} else if (mode == Mode.PlayerInventory) {
				chestUI.TransferAll_PlayerToChest(index);
			}
		} else {
			if(mode == Mode.ChestInventory){
				chestUI.TransferOne_ChestToPlayer(index);
			} else if (mode == Mode.PlayerInventory) {
				chestUI.TransferOne_PlayerToChest(index);
			}
		}
	}
}
