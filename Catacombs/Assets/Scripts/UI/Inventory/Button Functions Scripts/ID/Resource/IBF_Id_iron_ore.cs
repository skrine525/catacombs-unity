using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBF_Id_iron_ore : MonoBehaviour {
	private int index;
	private GameObject player;
	private InventoryController playerInventory;
	private ItemsFunctions itemsFunctions;
	private GameObject dropPrefab;

	// Use this for initialization
	void Start () {
		dropPrefab = Resources.Load<GameObject>("Prefabs/Resources/iron_ore");
		itemsFunctions = GetComponentInParent<ItemsFunctions> ();
		playerInventory = itemsFunctions.PlayerInventory;
		player = playerInventory.gameObject;
	}

	// Update is called once per frame
	void Update () {
		index = itemsFunctions.selectedIndex;
	}

	public void DropItem(int _index){
		index = _index;
		playerInventory.pickUpTheSomeItems (index, 1);
		GameObject drop = Instantiate (dropPrefab) as GameObject;
		drop.transform.position = player.transform.TransformPoint(new Vector3(0, 0, 2f));
		drop.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y + 180f, 0);
		drop.GetComponent<Rigidbody>().AddForce (drop.transform.forward * -10f, ForceMode.Impulse);
	}
}
