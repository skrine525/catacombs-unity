using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IBF_Id_hammer : MonoBehaviour {
	private int index;
	private Button button;
	private PlayerEquipmentController playerEquip;
	private InventoryController playerInventory;
	private GameObject hammerPrefab;
	private GameObject hammerDropPrefab;
	private ItemsFunctions itemsFunctions;
	private IBFController ibfController;

	// Use this for initialization
	void Start () {
		hammerPrefab = Resources.Load<GameObject> ("Prefabs/Tools/hammer");
		hammerDropPrefab = Resources.Load<GameObject>("Prefabs/Tools/hammer_drop");
		itemsFunctions = GetComponentInParent<ItemsFunctions> ();
		playerInventory = itemsFunctions.PlayerInventory;
		playerEquip = playerInventory.GetComponent<PlayerEquipmentController> ();
		ibfController = GetComponentInParent<IBFController> ();

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void DropItem(int _index){
		index = _index;
		if (playerEquip.itemId == ItemsInfo.Id.hammer && playerEquip.cellIndex == index) {
			playerEquip.deleteTool ();
		}
		playerInventory.pickUpTheSomeItems (index, 1);
		GameObject drop = Instantiate (hammerDropPrefab) as GameObject;
		drop.transform.position = playerEquip.gameObject.transform.TransformPoint(new Vector3(0, 0, 2f));
		drop.transform.localEulerAngles = new Vector3(0, playerEquip.gameObject.transform.localEulerAngles.y + 180f, 0);
		drop.GetComponent<Rigidbody>().AddForce (drop.transform.forward * -10f, ForceMode.Impulse);
	}

	public void Equip(){
		playerEquip.takeTool (hammerPrefab, new Vector3(0.3f, 0.15f, 0), new Vector3 (25, 0, 90), ItemsInfo.Id.hammer, index);
	}

	public void Distract(){
		playerEquip.deleteTool ();
	}

	public void UseItem(int _index){
		index = _index;
		if (playerEquip.cellIndex == index && playerEquip.itemId == ItemsInfo.Id.hammer) {
			Distract ();
		} else {
			Equip ();
		}
	}
}
