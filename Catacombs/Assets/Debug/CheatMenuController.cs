using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenuController : MonoBehaviour {
	[SerializeField] private InventoryCheatMenu cheat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F1)) {
			if (cheat.gameObject.activeSelf) {
				cheat.Erase ();
				UINotificationController.createNewNotification ("CheatMenuController", "Cheat Menu diactivated", 15);
			} else {
				cheat.Draw ();
				UINotificationController.createNewNotification ("CheatMenuController", "Cheat Menu activated", 15);
			}
		}
	}
}
