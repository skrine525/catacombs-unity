using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour {
	[SerializeField] private Text _text;
	private InventoryController inventory;

	// Use this for initialization
	void Start () {
		inventory = GetComponent<InventoryController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sss(){
		Debug.Log ("Suka");
	}
}
