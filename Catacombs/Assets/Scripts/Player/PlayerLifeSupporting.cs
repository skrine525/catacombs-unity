using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSupporting : MonoBehaviour {
	[SerializeField] private float _health;
	[SerializeField] private float _hunger = 5f;
	[SerializeField] private float _thirst;
	private SaveID saveID;

	// Use this for initialization
	void Start () {
		StartCoroutine (spendFood ());
		StartCoroutine (spendWater ());
		saveID = GetComponent<SaveID> ();
	}
	
	// Update is called once per frame
	void Update () {
		_health = Mathf.Clamp (_health, 0f, 100f);
		_hunger = Mathf.Clamp (_hunger, 0f, 100f);
		_thirst = Mathf.Clamp (_thirst, 0f, 100f);
	}

	public void restoredFood(float count){
		_hunger += count;
	}

	public void restoredWater(float count){
		_thirst += count;
	}

	public float health{
		get { return _health; }
	}

	public float hunger{
		get { return _hunger; }
	}

	public float thirst{
		get { return _thirst; }
	}

	private IEnumerator spendFood(){
		while (true) {
			_hunger -= 0.04f;
			yield return new WaitForSeconds (1f);
		}
	}

	private IEnumerator spendWater(){
		while (true) {
			_thirst -= 0.05f;
			yield return new WaitForSeconds (1f);
		}
	}

	//////////////////////////////////////
	/// Saver
	/////////////////////////////////////
	private class SaverClassData{
		public float health;
		public float hunger;
		public float thirst;
	}
	[HideInInspector] private SaverClassData data = new SaverClassData();

	public void SaveGameData(){
		data.health = _health;
		data.hunger = _hunger;
		data.thirst = _thirst;
		SaveManager.save.SetData(saveID.ID, "player_life_stats", data);
	}

	public void LoadGameData(){
		data = new SaverClassData();
		data = SaveManager.save.GetData<SaverClassData>(saveID.ID, "player_life_stats");
		_health = data.health;
		_hunger = data.hunger;
		_thirst = data.thirst;
	}
}
