using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBF_Id_coin : MonoBehaviour {
	private int index;
	private GameObject player;
	private InventoryController playerInventory;
	private GameObject dropPrefab;
	private ItemsFunctions itemsFunctions;

	// Use this for initialization
	void Start () {
		dropPrefab = Resources.Load<GameObject>("Prefabs/Resources/coin");
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

	public void UseItem(int _index){
		index = _index;
		UINotificationController.createNewNotification ("Coin", "Вы подбросили монетку", 15);
		GameObject Object = new GameObject();
		Object.name = "Toss Coin Proccess Object";
		Object.AddComponent<TossCoin> ().playerInventory = playerInventory;
		playerInventory.pickUpTheSomeItems (ItemsInfo.Id.coin, 1);
	}
}

public class TossCoin : MonoBehaviour{
	public InventoryController playerInventory;
	
	void Start(){
		StartCoroutine (Toss ());
	}

	private IEnumerator Toss(){
		yield return new WaitForSeconds (1);
		int result = Random.Range (0, 2);
		if (result == 0) {
			UINotificationController.createNewNotification ("Coin", "Выпал Орел", 15);
		} else {
			UINotificationController.createNewNotification ("Coin", "Выпала Решка", 15);
		}
		playerInventory.giveTheItem (ItemsInfo.Id.coin, 1);
		Destroy (gameObject);
	}
}