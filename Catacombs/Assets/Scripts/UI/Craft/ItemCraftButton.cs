using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCraftButton : MonoBehaviour {
	[HideInInspector] public ItemsInfo.Id itemId;
	[HideInInspector] public CraftUI craftUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClick(){
		craftUI.selectItem (itemId);
	}
}
