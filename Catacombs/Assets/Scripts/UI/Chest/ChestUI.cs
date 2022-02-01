using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestUI : MonoBehaviour {
	[SerializeField] private InventoryController playerInventory;
	[SerializeField] private GameObject chestItemButtonPrefab;
	[SerializeField] private Transform chestItemScrollViewContent;
	[SerializeField] private Transform chestItemButtonConfig;
	[SerializeField] private GameObject[] playerItemCells;
	[SerializeField] private CraftUI craftUI;
	[SerializeField] private InventoryUI inventoryUI;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject pointer;
	public bool isCanDraw = true;
	private GameObject[] chestItemButtons;
	private InventoryController ChestInventory;
	private bool isCanUpdateCells = true;

	// Use this for initialization

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(ChestInventory != null){
			UpdateCells();
		}
	}

	public bool getActive(){
		return gameObject.activeSelf;
	}

	public void Draw(InventoryController chestInventory){
		if(isCanDraw){
			gameObject.SetActive(true);
			inventoryUI.isCanDraw = false;
			craftUI.isCanDraw = false;
			player.GetComponent<FPSInput>().enabled = false;
			player.GetComponent<MouseLook>().enabled = false;
			player.transform.Find("Camera Body").GetComponent<MouseLook>().enabled = false;
			pointer.SetActive(false);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			ChestInventory = chestInventory;
			chestItemButtons = new GameObject[ChestInventory.items.Length];
			for(int i = 0; i < chestItemButtons.Length; i++){
					chestItemButtons[i] = Instantiate(chestItemButtonPrefab) as GameObject;
					chestItemButtons[i].transform.parent = chestItemScrollViewContent;
					chestItemButtons[i].transform.localScale = chestItemButtonConfig.transform.localScale;
					ChestItemButtonController itemControl = chestItemButtons[i].GetComponent<ChestItemButtonController>();
					itemControl.chestUI = this;
					itemControl.index = i;
					ItemsInfo.Items item = ItemsInfo.getInfo(ChestInventory.items[i].id);
					chestItemButtons[i].transform.Find("Button").GetComponent<Image>().sprite = item.image;
					if(ChestInventory.items[i].count == 0){
						chestItemButtons[i].transform.Find("Count").GetComponent<Text>().text = "";
					} else {
						chestItemButtons[i].transform.Find("Count").GetComponent<Text>().text = ChestInventory.items[i].count.ToString();
					}
			}
			for(int i = 0; i < playerItemCells.Length; i++){
				ChestItemButtonController item = playerItemCells[i].GetComponent<ChestItemButtonController>();
				item.chestUI = this;
				item.index = i;
			}
		}
	}

	public void Erase(){
		gameObject.SetActive(false);
		ChestInventory = null;
		inventoryUI.isCanDraw = true;
			craftUI.isCanDraw = true;
			player.GetComponent<FPSInput>().enabled = true;
			player.GetComponent<MouseLook>().enabled = true;
			player.transform.Find("Camera Body").GetComponent<MouseLook>().enabled = true;
			pointer.SetActive(true);
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		for(int i = 0; i < chestItemButtons.Length; i++){
			Destroy(chestItemButtons[i]);
		}
	}

	public void TransferOne_ChestToPlayer(int index){
		ChestInventory.translateItemsToOtherInventory(playerInventory, index, 1);
	}

	public void TransferAll_ChestToPlayer(int index){
		ChestInventory.translateItemsToOtherInventory(playerInventory, index, ChestInventory.items[index].count);
	}

	public void TransferOne_PlayerToChest(int index){
		playerInventory.translateItemsToOtherInventory(ChestInventory, index, 1);
	}

	public void TransferAll_PlayerToChest(int index){
		playerInventory.translateItemsToOtherInventory(ChestInventory, index, playerInventory.items[index].count);
	}

	private void UpdateCells(){
		if(isCanUpdateCells){
			isCanUpdateCells = false;
			for(int i = 0; i < chestItemButtons.Length; i++){
					ItemsInfo.Items item = ItemsInfo.getInfo(ChestInventory.items[i].id);
					chestItemButtons[i].transform.Find("Button").GetComponent<Image>().sprite = item.image;
					if(ChestInventory.items[i].count == 0){
						chestItemButtons[i].transform.Find("Count").GetComponent<Text>().text = "";
					} else {
						chestItemButtons[i].transform.Find("Count").GetComponent<Text>().text = ChestInventory.items[i].count.ToString();
					}
			}
			for(int i = 0; i < playerItemCells.Length; i++){
				ItemsInfo.Items itemInfo = ItemsInfo.getInfo(playerInventory.items[i].id);
				playerItemCells[i].GetComponent<Image>().sprite = itemInfo.image;
				if(playerInventory.items[i].count == 0){
					playerItemCells[i].transform.Find("Count").GetComponent<Text>().text = "";
				} else {
					playerItemCells[i].transform.Find("Count").GetComponent<Text>().text = playerInventory.items[i].count.ToString();
				}
			}
			isCanUpdateCells = true;
		}
	}
}
