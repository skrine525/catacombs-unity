using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject_Saver : MonoBehaviour {
	private SaveID saveID;
	[SerializeField] private string PathToPrefab;

	// Use this for initialization

	void Awake(){
		saveID = GetComponent<SaveID> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//////////////////////////////////////////
	///Saver

	[System.Serializable] public class TransformData{
		public Vector3 position;
		public Vector3 rotation;
		public Vector3 scale;
	}
	public class SaverClassData{
		public TransformData transformData = new TransformData();
		public string PathToPrefab;
	}
	public SaverClassData data = new SaverClassData();

	public void LoadGameData(){
		data = new SaverClassData();
		data = SaveManager.save.GetData<SaverClassData>(saveID.ID, "GameObject");
		if (data != null){
			transform.position = data.transformData.position;
			transform.localEulerAngles = data.transformData.rotation;
			transform.localScale = data.transformData.scale;
		} else {
			Destroy(gameObject);
		}
	}

	public void SaveGameData(){
		data.transformData.position = transform.position;
		data.transformData.rotation = transform.localEulerAngles;
		data.transformData.scale = transform.localScale;
		data.PathToPrefab = "Prefabs/" + PathToPrefab;
		SaveManager.save.SetData(saveID.ID, "GameObject", data);
	}
}
