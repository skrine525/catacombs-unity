using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemIconDescription : EventTrigger {
	private InventoryController playerInventory;
	private ItemIcon itemIcon;
	private bool isDescriptionActive = false;
	private bool isOnPointer = false;
	private GameObject textDescriptionPrefab;
	private GameObject ItemDescription;
	private RectTransform ItemDescriptionRT;
	private Text descriptionText;

	void Start(){
		itemIcon = GetComponent<ItemIcon> ();
		playerInventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<InventoryController> ();
		textDescriptionPrefab = Resources.Load<GameObject> ("Prefabs/UI/ItemDescription");
	}

	void Update(){
		if (descriptionText != null) {
			descriptionText.text = ItemsInfo.getInfo (playerInventory.items [itemIcon.index].id).name;
		}
	}

	public override void OnPointerEnter(PointerEventData data){
		isOnPointer = true;
		StartCoroutine (DrawDescription());
	}

	public override void OnPointerExit(PointerEventData data){
		isOnPointer = false;
		if (isDescriptionActive) {
			Destroy (ItemDescription);
			isDescriptionActive = false;
		}
	}

	private IEnumerator DrawDescription(){
		yield return new WaitForSeconds (0.5f);
		if (isOnPointer) {
			if (ItemDescription != null) {
				Destroy (ItemDescription);
			}
			isDescriptionActive = true;
			ItemDescription = Instantiate (textDescriptionPrefab) as GameObject;
			ItemDescription.transform.parent = transform.parent;
			ItemDescriptionRT = ItemDescription.GetComponent<RectTransform> ();
			descriptionText = ItemDescription.GetComponent<Text>();
		}
	}
}
