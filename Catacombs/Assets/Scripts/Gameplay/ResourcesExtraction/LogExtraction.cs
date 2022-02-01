using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogExtraction : MonoBehaviour {
	public int treeHealth;
	public int amountLogDeposited;
	[SerializeField] private GameObject logDepositedPrefab;
	private int maxTreeHealth;

	// Use this for initialization
	void Start () {
		maxTreeHealth = treeHealth;
	}

	// Update is called once per frame
	void Update () {
		treeHealth = Mathf.Clamp (treeHealth, 0, maxTreeHealth);

		if (treeHealth == 0) {
			DestroyTree ();
		}
	}

	public void Extraction(){
		treeHealth--;
	}

	public void DestroyTree(){
		GameObject depositedOre = Instantiate (logDepositedPrefab) as GameObject;
		depositedOre.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
		depositedOre.GetComponent<ItemPicker> ().count = amountLogDeposited;
		Destroy (gameObject);
	}
}
