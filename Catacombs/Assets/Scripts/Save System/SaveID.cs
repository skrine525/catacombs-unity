using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveID : MonoBehaviour {
	private int id;
	[SerializeField] private bool isCanChangeID = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Awake(){
		id = gameObject.GetInstanceID();
	}

	public void SaveGameDataSystem(){
		BroadcastMessage ("SaveGameData");
	}

	public void LoadGameDataSystem(){
		BroadcastMessage ("LoadGameData");
	}

	public void ChangeID(int newID){
		id = newID;
	}

	public int ID{
		get { return id; }
	}
}
