using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour {
	[SerializeField] private GameObject SVContent;
	[SerializeField] private GameObject ICBPrefab;
	[SerializeField] private InventoryUI inventoryUI;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject pointer;
	[SerializeField] private GameObject ICB_Config;
	[SerializeField] private GameObject craftRecipeSVContent;
	[SerializeField] private GameObject itemRecipeImagePrefab;
	[SerializeField] private GameObject craftButton;
	[SerializeField] private GameObject[] inventoryItemsImages;
	public bool isCanDraw = true;
	private bool isActive;
	private Vector3 defaultButtonScale;
	private ItemsInfo.Id selectedItem;
	private InventoryController playerInventory;


	void Awake(){
		defaultButtonScale = ICB_Config.transform.localScale;
		Destroy (ICB_Config);
	}

	// Use this for initialization
	void Start () {
		isActive = gameObject.activeSelf;
		playerInventory = player.GetComponent<InventoryController> ();
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < inventoryItemsImages.Length; i++) {
			inventoryItemsImages [i].transform.Find ("Image").GetComponent<Image> ().sprite = ItemsInfo.getInfo (playerInventory.items[i].id).image;
			if (playerInventory.items [i].count != 0) {
				inventoryItemsImages [i].transform.Find ("Count").GetComponent<Text> ().text = playerInventory.items [i].count.ToString ();
			} else {
				inventoryItemsImages [i].transform.Find ("Count").GetComponent<Text> ().text = "";
			}
		}
	}

	public bool getActive(){
		return isActive;
	}

	public void Draw(){
		if (isCanDraw) {
			gameObject.SetActive (true);
			inventoryUI.isCanDraw = false;
			player.GetComponent<FPSInput> ().enabled = false;
			player.GetComponent<MouseLook> ().enabled = false;
			GameObject cameraBody = player.transform.Find ("Camera Body").gameObject;
			cameraBody.GetComponent<MouseLook> ().enabled = false;
			cameraBody.transform.Find ("Tool Body").GetComponent<ToolFunc> ().enabled = false;
			pointer.SetActive (false);
			selectedItem = ItemsInfo.Id.empty;
			craftButton.SetActive (false);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			for(int i = 0; i < ItemsCraftInfo.recipes.Length; i++){
				ItemsInfo.Id itemId = ItemsCraftInfo.recipes [i].itemId;
				int itemCount = ItemsCraftInfo.recipes [i].count;
				List<ItemsCraftInfo.RecipePart> recipe = ItemsCraftInfo.recipes [i].recipe;
				GameObject itemButton = Instantiate (ICBPrefab) as GameObject;
				itemButton.transform.parent = SVContent.transform;
				itemButton.transform.localScale = defaultButtonScale;
				itemButton.transform.Find("Button").GetComponent<Image> ().sprite = ItemsInfo.getInfo (itemId).image;
				itemButton.transform.Find("Button").GetComponent<ItemCraftButton> ().itemId = itemId;
				itemButton.transform.Find("Button").GetComponent<ItemCraftButton> ().craftUI = GetComponent<CraftUI> ();
				itemButton.transform.Find ("Count").GetComponent<Text> ().text = itemCount.ToString ();
			}
			isActive = true;
		}
	}

	public void Erase(){
		if (isCanDraw) {
			gameObject.SetActive (false);
			inventoryUI.isCanDraw = true;
			player.GetComponent<FPSInput> ().enabled = true;
			player.GetComponent<MouseLook> ().enabled = true;
			GameObject cameraBody = player.transform.Find ("Camera Body").gameObject;
			cameraBody.GetComponent<MouseLook> ().enabled = true;
			cameraBody.transform.Find ("Tool Body").GetComponent<ToolFunc> ().enabled = true;
			pointer.SetActive (true);
			selectedItem = ItemsInfo.Id.empty;
			craftButton.SetActive (false);
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			for(int i = 0; i < SVContent.transform.childCount; i++){
				Destroy (SVContent.transform.GetChild(i).gameObject);
			}
			for (int i = 0; i < craftRecipeSVContent.transform.childCount; i++) {
				Destroy (craftRecipeSVContent.transform.GetChild(i).gameObject);
			}
			isActive = false;
		}
	}

	public void selectItem(ItemsInfo.Id itemId){
		selectedItem = itemId;
		craftButton.SetActive (true);
		for (int i = 0; i < craftRecipeSVContent.transform.childCount; i++) {
			Destroy (craftRecipeSVContent.transform.GetChild(i).gameObject);
		}
		ItemsCraftInfo.Recipe recipe = ItemsCraftInfo.getRecipe (itemId);

		for(int i = 0; i < recipe.recipe.Count; i++){
			GameObject itemImage = Instantiate (itemRecipeImagePrefab) as GameObject;
			itemImage.transform.parent = craftRecipeSVContent.transform;
			itemImage.transform.Find ("ItemImage").GetComponent<Image> ().sprite = ItemsInfo.getInfo (recipe.recipe [i].id).image;
			itemImage.transform.localScale = defaultButtonScale;
			itemImage.transform.Find ("Count").GetComponent<Text> ().text = recipe.recipe [i].count.ToString ();
		}
	}

	public void Craft(){
		bool isContinue = true;
		List<ItemsCraftInfo.RecipePart> recipe = ItemsCraftInfo.getRecipe(selectedItem).recipe;
		for(int i = 0; i < recipe.Count; i++){
			if (!(recipe [i].count <= playerInventory.getCountById (recipe [i].id))) {
				isContinue = false;
			}
		}

		if (isContinue) {
			InventoryController.Results result = playerInventory.giveTheItem (ItemsCraftInfo.getRecipe (selectedItem).itemId, ItemsCraftInfo.getRecipe (selectedItem).count);
			if (result == InventoryController.Results.Ok) {
				for (int i = 0; i < recipe.Count; i++) {
					playerInventory.pickUpTheSomeItems (recipe [i].id, recipe [i].count);
				}
				UINotificationController.createNewNotification ("Craft", ItemsInfo.getInfo(ItemsCraftInfo.getRecipe (selectedItem).itemId).name + " x" + ItemsCraftInfo.getRecipe (selectedItem).count, 15);
			} else if (result == InventoryController.Results.There_is_no_place) {
				UINotificationController.createNewNotification ("Craft", "There is no place", 15);
			}
		} else {
			UINotificationController.createNewNotification ("Craft", "Insufficient Resources", 15);
		}
	}
}
