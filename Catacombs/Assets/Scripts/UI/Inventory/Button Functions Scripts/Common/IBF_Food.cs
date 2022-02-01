using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IBF_Food : MonoBehaviour {
	private InventoryController playerInventory;
	private int index;
	public GameObject dropPrefab;
	private PlayerLifeSupporting playerLifeSupporting;
	private ItemsFunctions itemsFunctions;
	private GameObject player;
	private AudioClip eatingAudio;
	private AudioClip drinkingAudio;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		itemsFunctions = GetComponentInParent<ItemsFunctions> ();
		playerInventory = itemsFunctions.PlayerInventory;
		player = playerInventory.gameObject;
		playerLifeSupporting = playerInventory.GetComponent<PlayerLifeSupporting> ();
		eatingAudio = Resources.Load ("FXs/Inventory/eating") as AudioClip;
		drinkingAudio = Resources.Load ("FXs/Inventory/drinking") as AudioClip;
		audioSource = gameObject.AddComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Employ(){
		FoodParameters.FoodInfo foodInfo = FoodParameters.getFoodInfo (playerInventory.items[index].id);
		string itemName = ItemsInfo.getInfo (playerInventory.items [index].id).name;
		if (playerInventory.pickUpTheSomeItems (index, 1) == InventoryController.Results.Ok) {
			if (foodInfo.restoredFood > foodInfo.restoredWater) {
				AudioPlayer2D.PlayOneShot(eatingAudio, 1f, "Food FX");
				UINotificationController.createNewNotification ("Food", "Вы съели " + itemName, 15);
			} else if (foodInfo.restoredFood < foodInfo.restoredWater) {
				AudioPlayer2D.PlayOneShot(drinkingAudio, 1f, "Food FX");
				UINotificationController.createNewNotification ("Food", "Вы выпили " + itemName, 15);
			}
			playerLifeSupporting.restoredFood (foodInfo.restoredFood);
			playerLifeSupporting.restoredWater (foodInfo.restoredWater);
			if (foodInfo.inventoryEmployItemDropId != ItemsInfo.Id.empty) {
				playerInventory.giveTheItem (foodInfo.inventoryEmployItemDropId, 1);
			}
		}
	}

	public void DropItem(int _index){
		index = _index;
		FoodParameters.FoodInfo foodInfo = FoodParameters.getFoodInfo (playerInventory.items[index].id);
		dropPrefab = foodInfo.dropPrefab;
		playerInventory.pickUpTheSomeItems (index, 1);
		GameObject drop = Instantiate (dropPrefab) as GameObject;
		drop.transform.position = player.transform.TransformPoint(new Vector3(0, 0, 2f));
		drop.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y + 180f, 0);
		drop.GetComponent<Rigidbody>().AddForce (drop.transform.forward * -10f, ForceMode.Impulse);
		index = _index;
	}

	public void UseItem(int _index){
		index = _index;
		Employ ();
	}
}
