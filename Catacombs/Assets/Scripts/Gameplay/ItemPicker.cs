using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour {
	public ItemsInfo.Id itemId;
	public int count;
	public List<InventoryController.ItemAttributes> itemAttribs;
	[HideInInspector] public InventoryController inventory;
	public AudioClip pickUpAudio;
	private SaveID saveID;
	[Tooltip("This item is responsible for using the standard pick-up FX, provided that the Pick Up Audio variable is empty")] public bool userDefaultFX = true;

	void Awake(){
		saveID = GetComponent<SaveID>();
	}
	
	// Use this for initialization
	void Start () {
		if (userDefaultFX && pickUpAudio == null) {
			pickUpAudio = Resources.Load ("FXs/default_pickup") as AudioClip;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pick(){
		InventoryController.Results pickResult = inventory.giveTheItem (itemId, count, itemAttribs);
		if (pickResult == InventoryController.Results.Ok) {
			UINotificationController.createNewNotification ("ItemPicker", ItemsInfo.getInfo (itemId).name + " x" + count, 15);
			if (pickUpAudio != null) {
				PlayPickUpAudio ();
			}
			Destroy (gameObject);
		} else if (pickResult == InventoryController.Results.There_is_no_place) {
			UINotificationController.createNewNotification ("ItemPicker", "Нет места", 15);
		}
	}

	private void PlayPickUpAudio(){
		AudioPlayer2D.PlayOneShot (pickUpAudio, 1, "Pick Up Audio Object");
	}

	////////////////////////////////////////////
	///Сохранение itemAttribs
	////////////////////////////////////////////

	[System.Serializable] public class ItemPickerData{
		public List<InventoryController.ItemAttributes> itemAttribs;
	}
	ItemPickerData data;

	public void LoadGameData(){
		Debug.Log(saveID.ID);
		data = new ItemPickerData();
		data = SaveManager.save.GetData<ItemPickerData>(saveID.ID, "ItemPicker");
		if(data != null){
			itemAttribs = data.itemAttribs.GetRange(0, data.itemAttribs.Count);
		}
	}

	public void SaveGameData(){
		if(itemAttribs.Count > 0){
			data = new ItemPickerData();
			if(itemAttribs.Count > 0){
				data.itemAttribs = itemAttribs.GetRange(0, itemAttribs.Count);
				SaveManager.save.SetData(saveID.ID, "ItemPicker", data);
			}
		}
	}
}