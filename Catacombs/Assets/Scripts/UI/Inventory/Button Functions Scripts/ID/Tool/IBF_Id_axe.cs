using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IBF_Id_axe : MonoBehaviour {
	private int index;
	private Button button;
	public PlayerEquipmentController playerEquip;
	public InventoryController playerInventory;
	private GameObject axePrefab;
	private GameObject axeDropPrefab;
	private ItemsFunctions itemsFunctions;
	private IBFController ibfController;

	// Use this for initialization
	void Start () {
		axePrefab = Resources.Load<GameObject> ("Prefabs/Tools/axe");
		axeDropPrefab = Resources.Load<GameObject>("Prefabs/Tools/axe_drop");
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
		if (playerEquip.itemId == ItemsInfo.Id.axe && playerEquip.cellIndex == index) {
			playerEquip.deleteTool ();
		}
		playerInventory.pickUpTheSomeItems (index, 1);
		GameObject drop = Instantiate (axeDropPrefab) as GameObject;
		drop.transform.position = playerEquip.gameObject.transform.TransformPoint(new Vector3(0, 0, 2f));
		drop.transform.localEulerAngles = new Vector3(0, playerEquip.gameObject.transform.localEulerAngles.y + 270f, 0);
		drop.GetComponent<Rigidbody>().AddForce (drop.transform.right * 10f, ForceMode.Impulse);
	}

	public void Equip(){
		playerEquip.takeTool (axePrefab, new Vector3(0.35f, -0.25f, 0), new Vector3 (0, -90, 60), ItemsInfo.Id.axe, index);
	}

	public void Distract(){
		playerEquip.deleteTool ();
	}

	public void UseItem(int _index){
		index = _index;
		if (playerEquip.cellIndex == index && playerEquip.itemId == ItemsInfo.Id.axe) {
			Distract ();
		} else {
			Equip ();
		}
	}
}
