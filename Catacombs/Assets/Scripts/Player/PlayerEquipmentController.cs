using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour {
	[SerializeField] private GameObject toolBody;
	private InventoryController playerInventory;
	private GameObject outfittedTool;
	private ItemsInfo.Id _itemId = ItemsInfo.Id.empty;
	private int _cellIndex = -1;
	private SaveID saveID;


	// Use this for initialization
	void Start () {
		saveID = GetComponent<SaveID> ();
		playerInventory = GetComponent<InventoryController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_itemId != ItemsInfo.Id.empty && playerInventory.items[_cellIndex].id == ItemsInfo.Id.empty){
			deleteTool();
			_itemId = ItemsInfo.Id.empty;
			_cellIndex = -1;
		}
	}

	public void takeTool(GameObject toolPrefab, Vector3 position, Vector3 rotation, ItemsInfo.Id itemId, int cellIndex){
		if (outfittedTool != null) {
			deleteTool ();
		}
		outfittedTool = Instantiate (toolPrefab) as GameObject;
		outfittedTool.name = "Tool";
		outfittedTool.transform.parent = toolBody.transform;
		outfittedTool.transform.localPosition = position;
		outfittedTool.transform.localEulerAngles = rotation;
		_itemId = itemId;
		_cellIndex = cellIndex;
		UINotificationController.createNewNotification("Equipment", "Экипировано: " + ItemsInfo.getInfo(_itemId).name, 15);
		Component c = new Component();
	}

	public void deleteTool(){
		UINotificationController.createNewNotification("Equipment", "Снято: " + ItemsInfo.getInfo(_itemId).name, 15);
		_itemId = ItemsInfo.Id.empty;
		_cellIndex = -1;
		Destroy(outfittedTool);
	}

	public ItemsInfo.Id itemId{
		get { return _itemId; }
	}

	public int cellIndex{
		get { return _cellIndex; }
	}
		
	//////////////////////////////////////
	/// Saver
	/////////////////////////////////////

	public void SaveGameData(){
		
	}

	public void LoadGameData(){
		
	}
}
