using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsFunctions : MonoBehaviour {
	[SerializeField] private InventoryController playerInventory;
	[SerializeField] private ItemIcon[] itemIcons;
	[HideInInspector] public int selectedIndex;
	private int lastSelectedIndex;
	private ItemsInfo.Id lastSelectedItemId;
	private IBFController ibfController;
	private ItemsInfo.Id[] lastPlayersItemsIds = new ItemsInfo.Id[10];

	// Use this for initialization
	void Start () {
		selectedIndex = -1;
		lastSelectedIndex = selectedIndex;
		lastSelectedItemId = ItemsInfo.Id.empty;
		ibfController = GetComponent<IBFController> ();
		for (int i = 0; i < playerInventory.items.Length; i++) {
			lastPlayersItemsIds [i] = playerInventory.items [i].id;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < itemIcons.Length; i++) {
			if (playerInventory.items [i].id != lastPlayersItemsIds [i]) {
				ibfController.resetScriptFunctionObject (itemIcons [i].gameObject);
				lastPlayersItemsIds [i] = playerInventory.items [i].id;
			}
			UpdateCell (itemIcons[i].index, itemIcons[i].gameObject);
		}
	}

	public InventoryController PlayerInventory{
		get { return playerInventory; }
	}

	public void ResetCells(){
		for (int i = 0; i < itemIcons.Length; i++) {
			ibfController.resetScriptFunctionObject (itemIcons [i].gameObject);
		}
	}

	private void UpdateCell(int index, GameObject cell){ // Распеределение по типам предметов
		ItemsInfo.Id itemId = playerInventory.getIdOfTheItem (index);
		ItemsInfo.Items itemInfo = ItemsInfo.getInfo (itemId);
		ItemsInfo.ItemType itemType = itemInfo.type;

		if (itemType == ItemsInfo.ItemType.Equipment) {
			Equipment (itemId, cell);
		} else if (itemType == ItemsInfo.ItemType.Food) {
			Food (itemId, cell);
		} else if (itemType == ItemsInfo.ItemType.Resource) {
			Resource (itemId, cell);
		} else if (itemType == ItemsInfo.ItemType.Tool) {
			Tool (itemId, cell);
		} else if (itemType == ItemsInfo.ItemType.WorldItem) {
			WorldItem (itemId, cell);
		} else if (itemType == ItemsInfo.ItemType.Null) { // Если тип пустой, то убрать кнопки
			ibfController.resetScriptFunctionObject(cell);
		}
	}
		
	///////////////////////////////////////////////////////////////////////
	/// Функция для Equipment

	private void Equipment(ItemsInfo.Id itemId, GameObject cell){
		
	}

	///////////////////////////////////////////////////////////////////////
	/// Функция для Food

	private void Food(ItemsInfo.Id itemId, GameObject cell){
		ibfController.initScriptFunctionObject<IBF_Food> (cell);
	}

	///////////////////////////////////////////////////////////////////////
	/// Функция для Resource

	private void Resource(ItemsInfo.Id itemId, GameObject cell){
		switch (itemId) {
		case ItemsInfo.Id.iron_ingot:
			ibfController.initScriptFunctionObject<IBF_Id_iron_ingot> (cell);
			break;
		case ItemsInfo.Id.iron_ore:
			ibfController.initScriptFunctionObject<IBF_Id_iron_ore> (cell);
			break;
		case ItemsInfo.Id.coal:
			ibfController.initScriptFunctionObject<IBF_Id_coal> (cell);
			break;
		case ItemsInfo.Id.log:
			ibfController.initScriptFunctionObject<IBF_Id_log> (cell);
			break;
		case ItemsInfo.Id.board:
			ibfController.initScriptFunctionObject<IBF_Id_board> (cell);
			break;
		case ItemsInfo.Id.iron_rod:
			ibfController.initScriptFunctionObject<IBF_Id_iron_rod> (cell);
			break;
		case ItemsInfo.Id.wooden_cup:
			ibfController.initScriptFunctionObject<IBF_Id_wooden_cup> (cell);
			break;
		case ItemsInfo.Id.coin:
			ibfController.initScriptFunctionObject<IBF_Id_coin> (cell);
			break;
		}
	}

	///////////////////////////////////////////////////////////////////////
	/// Функция для Toll

	private void Tool(ItemsInfo.Id itemId, GameObject cell){
		switch(itemId){
		case ItemsInfo.Id.axe:
			ibfController.initScriptFunctionObject<IBF_Id_axe> (cell);
			break;
		case ItemsInfo.Id.pickaxe:
			ibfController.initScriptFunctionObject<IBF_Id_pickaxe> (cell);
			break;
		case ItemsInfo.Id.hammer:
			ibfController.initScriptFunctionObject<IBF_Id_hammer> (cell);
			break;
		case ItemsInfo.Id.backpack:
			ibfController.initScriptFunctionObject<IBF_Id_backpack>(cell);
			break;
		}
	}

	///////////////////////////////////////////////////////////////////////
	/// Функция для WorldItem

	private void WorldItem(ItemsInfo.Id itemId, GameObject cell){

	}
}
