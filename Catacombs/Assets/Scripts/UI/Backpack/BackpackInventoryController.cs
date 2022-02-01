using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackInventoryController : MonoBehaviour {

	private SaveID saveID;
	[HideInInspector] public int InventoryID;

	// Use this for initialization

	void Awake(){
		saveID = GetComponent<SaveID>();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
