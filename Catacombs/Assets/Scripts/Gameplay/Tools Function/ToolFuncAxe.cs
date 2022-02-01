using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolFuncAxe : MonoBehaviour {
	private bool isCanDoThis = true;
	private Animator animator;
	private AudioSource audioSource;
	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("Player").transform.Find ("Camera Body").Find ("Main Camera").GetComponent<Camera> ();
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartWork(){
		if (isCanDoThis) {
			isCanDoThis = false;
			animator.SetBool ("isAxe_Bump", true);
		}
	}

	public void StopBumpAnimation(){
		isCanDoThis = true;
		animator.SetBool ("isAxe_Bump", false);
	}

	public void AxeBump(){
		Vector3 point = new Vector3 (mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0);
		Ray ray = mainCamera.ScreenPointToRay (point);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 3f)) {
			GameObject hitObject = hit.transform.gameObject;
			if (hitObject.tag == "Tree") {
				audioSource.Play ();
				hitObject.GetComponent<LogExtraction> ().Extraction ();
			} 
		}
	}
}
