using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptionMove : MonoBehaviour {
	private RectTransform rectTransform;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform> ();
		rectTransform.position = Input.mousePosition;
		rectTransform.anchoredPosition = new Vector2 (rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - 30f);
	}
	
	// Update is called once per frame
	void Update () {
		rectTransform.position = Input.mousePosition;
		rectTransform.anchoredPosition = new Vector2 (rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - 30f);
	}

	void OnDisable(){
		if (gameObject != null) {
			Destroy (gameObject);
		}
	}
}
