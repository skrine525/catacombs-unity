using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour {
	public Text notificationText;
	[SerializeField] private RectTransform nextPositionRectTransform;
	public float lifeTime = 5f;
	private Vector2 nextHorizontalPosition;
	private Vector2 nextVerticalPosition;
	[HideInInspector] public UINotificationController uiNotificationController;
	private bool isCanSendTrueToUINC = true;
	private RectTransform rectTransform;
	private bool isCanDestroy = false;
	[SerializeField] private float verticalSpeed = 200f;
	[SerializeField] private float horizontalSpeed = 200f;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform> ();
		nextHorizontalPosition = rectTransform.anchoredPosition;
		StartCoroutine (LifeTime ());
	}
	
	// Update is called once per frame
	void Update () {
		rectTransform.anchoredPosition = Vector2.MoveTowards (rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, nextVerticalPosition.y), Time.deltaTime * verticalSpeed);
		if (rectTransform.anchoredPosition.y == nextVerticalPosition.y && isCanSendTrueToUINC) {
			isCanSendTrueToUINC = false;
			uiNotificationController.isCanInitNewNotification = true;
		}
		if (rectTransform.anchoredPosition.x != nextHorizontalPosition.x) {
			rectTransform.anchoredPosition = Vector2.MoveTowards (rectTransform.anchoredPosition, new Vector2 (nextHorizontalPosition.x, rectTransform.anchoredPosition.y), Time.deltaTime * horizontalSpeed);
		} else if (rectTransform.anchoredPosition.x == nextHorizontalPosition.x && isCanDestroy) {
			Destroy (gameObject);
		}
	}

	public void NextPosition(){
		if (rectTransform != null) {
			nextVerticalPosition = new Vector2 (rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 40f);
		} else {
			rectTransform = GetComponent<RectTransform> ();
			nextVerticalPosition = new Vector2 (rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 40f);
		}
	}

	private IEnumerator LifeTime(){
		yield return new WaitForSeconds (lifeTime);
		nextHorizontalPosition = new Vector2 (rectTransform.anchoredPosition.x + 250f, rectTransform.anchoredPosition.y);
		isCanDestroy = true;
	}
}
