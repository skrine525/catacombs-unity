using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINotificationController : MonoBehaviour {
	private struct  NotificationGroup
	{
		public string notificationName;
		public string notificationText;
		public int textFontSize;
	}
	[SerializeField] private GameObject notificationPrefab;
	[SerializeField] private GameObject notificationDefaultSet;
	[SerializeField] private GameObject notificationsParent;
	[HideInInspector] public bool isCanInitNewNotification = true;
	private static List<NotificationGroup> notificationList = new List<NotificationGroup> ();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isCanInitNewNotification && notificationList.Count != 0) {
			isCanInitNewNotification = false;
			NotificationGroup notification = notificationList [0];
			notificationList.Remove (notification);
			initNewNotification (notification.notificationName, notification.notificationText, notification.textFontSize);
		}
	}

	public static void createNewNotification(string notificationName, string notificationText, int textFontSize){
		NotificationGroup notification;
		notification.notificationName = notificationName;
		notification.notificationText = notificationText;
		notification.textFontSize = textFontSize;
		notificationList.Add (notification);
	}

	private void initNewNotification(string notificationName, string notificationText, int textFontSize){
		GameObject notification = Instantiate (notificationPrefab) as GameObject;
		notification.transform.localScale = notificationDefaultSet.transform.lossyScale;
		notification.transform.parent = notificationsParent.transform;
		notification.transform.position = notificationDefaultSet.transform.position;
		notification.name = "Notification_" + notificationName;
		NotificationController notificationController = notification.GetComponent<NotificationController> ();
		notificationController.notificationText.text = notificationText;
		notificationController.notificationText.fontSize = textFontSize;
		notificationController.uiNotificationController = this;
		for (int i = 0; i < notificationsParent.transform.childCount; i++) {
			notificationsParent.transform.GetChild (i).GetComponent<NotificationController> ().NextPosition ();
		}
	}

	private void NotificationsNextPosition (){
		GameObject notificationsTranslater = new GameObject ();
		notificationsTranslater.transform.parent = notificationsParent.transform;
		notificationsTranslater.transform.position = new Vector2 (0, 0);
		for (int i = 0; i < notificationsParent.transform.childCount; i++) {
			notificationsParent.transform.GetChild (i).parent = notificationsTranslater.transform;
		}
	}
}
