using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorQuality : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			UINotificationController.createNewNotification ("Debug", "Понижение графики для Editor", 12);
			QualitySettings.SetQualityLevel (2);
		}
	}
	
	// Update is called once per frame
	void Update () {
		////
	}
}