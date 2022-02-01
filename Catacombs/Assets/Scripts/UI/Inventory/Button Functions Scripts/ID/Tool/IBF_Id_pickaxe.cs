using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IBF_Id_pickaxe : MonoBehaviour {
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
		playerEquip = itemsFunctions.PlayerInventory.GetComponent<PlayerEquipmentController> ();
		playerInventory = itemsFunctions.PlayerInventory;
		ibfController = GetComponentInParent<IBFController> ();

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void DropItem(int _index){
		index = _index;
		if (playerEquip.itemId == ItemsInfo.Id.pickaxe && playerEquip.cellIndex == index) {
			playerEquip.deleteTool ();
		}

		GameObject drop = Instantiate (pickaxeDropPrefab) as GameObject;
		if(playerInventory.items[index].attribs.Count > 0){
			drop.GetComponent<ItemPicker>().itemAttribs = playerInventory.items[index].attribs.GetRange(0, playerInventory.items[index].attribs.Count);
		}
		playerInventory.pickUpTheSomeItems (index, 1);
		drop.transform.position = playerEquip.gameObject.transform.TransformPoint(new Vector3(0, 0, 2f));
		drop.transform.localEulerAngles = new Vector3(-90, playerEquip.gameObject.transform.localEulerAngles.y + 180f, 0);
		drop.GetComponent<Rigidbody>().AddForce (drop.transform.up * 10f, ForceMode.Impulse);
	}

	public void Equip(){
		playerEquip.takeTool (pickaxePrefab, new Vector3(0.3f, -0.5f, 0f), new Vector3 (0f, -80f, -15f), ItemsInfo.Id.pickaxe, index);
	}

	public void UseItem(int _index){
		index = _index;
		if (playerEquip.cellIndex == index && playerEquip.itemId == ItemsInfo.Id.pickaxe) {
			Distract ();
		} else {
			Equip ();
		}
	}

	public void Distract(){
		playerEquip.deleteTool ();
	}
}
