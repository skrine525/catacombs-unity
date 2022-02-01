using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolFuncPickaxe : MonoBehaviour {
	private Camera mainCamera;
	[SerializeField] private GameObject sparksParticalPrefab;
	private bool isCanDoThis = true;
	private float delayBetweenSwing = 0.5f;
	private Animator animator;
	private AudioSource audioSource;
	private GameObject playerObject;
	private InventoryController playerInventory;
	private int _cellIndex;
	private int Durability = 10;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("Player").transform.Find ("Camera Body").Find ("Main Camera").GetComponent<Camera> ();
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
		playerObject = transform.parent.parent.parent.gameObject;
		_cellIndex = playerObject.GetComponent<PlayerEquipmentController>().cellIndex;
		playerInventory = playerObject.GetComponent<InventoryController>();
		string dur = playerInventory.getItemAttribute(_cellIndex, "Durability");
		if(dur != null){
			Durability = int.Parse(dur);
		} else {
			playerInventory.setItemAttribute(_cellIndex, "Durability", Durability.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Durability == 0){
			UINotificationController.createNewNotification("Pickaxe_Destroy", "Кирка сломалась", 15);
			playerInventory.deleteTheItem(_cellIndex);
			playerObject.GetComponent<PlayerEquipmentController>().deleteTool();
		}
	}

	public void StartWork(){
		if (isCanDoThis) {
			animator.SetBool ("isPickaxe_Bump", true);
			isCanDoThis = false;
		}
	}

	public void StopBumpAnimation(){
		animator.SetBool ("isPickaxe_Bump", false);
		isCanDoThis = true;
	}

	private void PickaxeBump(){
		Vector3 point = new Vector3 (mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0);
		Ray ray = mainCamera.ScreenPointToRay (point);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 3f)) {
			GameObject hitObject = hit.transform.gameObject;
			if (hitObject.tag == "Ore") {
				audioSource.Play ();
				hitObject.GetComponent<OreExtraction> ().Extraction ();
				GameObject sparks = Instantiate (sparksParticalPrefab) as GameObject;
				sparks.transform.position = hit.point;
				Durability--;
				playerInventory.setItemAttribute(_cellIndex, "Durability", Durability.ToString());
			} 
		}
	}
}
