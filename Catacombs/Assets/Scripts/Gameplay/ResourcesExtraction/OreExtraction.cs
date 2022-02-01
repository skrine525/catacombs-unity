using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreExtraction : MonoBehaviour {
	public int oreHealth;
	public int amountOreDeposited;
	[SerializeField] private GameObject oreDepositedPrefab;
	private int maxOreHealth;

	// Use this for initialization
	void Start () {
		maxOreHealth = oreHealth;
	}
	
	// Update is called once per frame
	void Update () {
		oreHealth = Mathf.Clamp (oreHealth, 0, maxOreHealth);

		if (oreHealth == 0) {
			DestroyOre ();
		}
	}

	public void Extraction(){
		oreHealth--;
	}

	public void DestroyOre(){
		GameObject depositedOre = Instantiate (oreDepositedPrefab) as GameObject;
		depositedOre.transform.position = transform.position;
		depositedOre.GetComponent<ItemPicker> ().count = amountOreDeposited;
		Destroy (gameObject);
	}
}
