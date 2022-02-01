using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager{
	public static SaveClass save = new SaveClass();
	public static int CountOfSaveIDObjects = 0;
	public static int[] ObjectSaveID;

	public static GameObject FindGameObjectBySaveID(int id){
		GameObject returnObject = null;
		SaveID[] ids = GameObject.FindObjectsOfType<SaveID> ();
		for(int i = 0; i < ids.Length; i++){
			if(ids[i].ID == id){
				returnObject = ids[i].gameObject;
			}
		}
		return returnObject;
	}

	public static void SaveGame(){
		save.ClearSave();
		SaveID[] saveIDs = GameObject.FindObjectsOfType<SaveID> ();
		CountOfSaveIDObjects = saveIDs.Length;
		for(int i = 0; i < saveIDs.Length; i++){
			saveIDs[i].SaveGameDataSystem();
		}
		save.SaveInFile("Save_" + 0);

		UINotificationController.createNewNotification ("Save", "Игра сохранена!", 15);
	}
	public static void LoadGame(){
		save.LoadFromFile("Save_" + 0);
		SaveID[] saveIDs = GameObject.FindObjectsOfType<SaveID> ();
		for(int i = 0; i < save.Save.Count; i++){
			bool checkObject = true;
			for(int j = 0; j < saveIDs.Length; j++){
				if(save.Save[i].save_id == saveIDs[j].ID){
					checkObject = false;
					break;
				}
			}
			if(checkObject){
				GameObject_Saver.SaverClassData data = save.GetData<GameObject_Saver.SaverClassData>(save.Save[i].save_id, "GameObject");
				if(data != null){
					GameObject prefab = Resources.Load(data.PathToPrefab) as GameObject;
					if(prefab != null){
						GameObject ObjectPrefab = GameObject.Instantiate(prefab) as GameObject;
						ObjectPrefab.transform.position = data.transformData.position;
						ObjectPrefab.transform.localEulerAngles = data.transformData.rotation;
						ObjectPrefab.transform.localEulerAngles = data.transformData.rotation;
						Debug.Log("Object spawned!");
						SaveClass.SaveStruct saveStruct = save.Save[i];
						saveStruct.save_id = ObjectPrefab.GetComponent<SaveID>().ID;
						Debug.Log(save.Save[i].save_id + " " + saveStruct.save_id);
						Debug.Log(save.Save[i].save_id);
						ObjectPrefab.GetComponent<SaveID>().ChangeID(save.Save[i].save_id);
						//ObjectPrefab.GetComponent<SaveID>().LoadGameDataSystem();
					}
				}
			}
		}
		
		saveIDs = GameObject.FindObjectsOfType<SaveID> ();
		for(int i = 0; i < saveIDs.Length; i++){
			saveIDs[i].LoadGameDataSystem();
		}

		UINotificationController.createNewNotification ("Save", "Игра загружена!", 15);
	}

	/////////////////////////////////////////////////

	public class SaveClass{
		[System.Serializable] public struct SaveData{
			public string name;
			public string json_data;
		}
		[System.Serializable] public struct SaveStruct{
			public int save_id;
			public List<SaveData> data;
		}

		public List<SaveStruct> Save = new List<SaveStruct>();

		private SaveStruct newSaveStruct(int save_id){
			SaveStruct Struct = new SaveStruct();
			foreach(SaveStruct save_temp in Save){
				if(save_temp.save_id == save_id){
					return Struct;
				}
			}
			Struct.save_id = save_id;
			Struct.data = new List<SaveData>();
			return Struct;
		}

		private SaveData newSaveData(string name, object obj){
			SaveData saveData = new SaveData();
			saveData.name = name;
			saveData.json_data = JsonUtility.ToJson(obj);
			return saveData;
		}

		public T GetData<T>(int save_id, string data_name) where T: class{
			T returnObject = null;
			for(int i = 0; i < Save.Count; i++){
				if(Save[i].save_id == save_id){
					for(int j = 0; j < Save[i].data.Count; j++){
						if(Save[i].data[j].name == data_name){
							returnObject = JsonUtility.FromJson<T>(Save[i].data[j].json_data);
							break;
						}
					}
				}
			}
			return returnObject;
		}

		public void SetData(int save_id, string name, object obj){
			int main_data_index = -1;
			for(int i = 0; i < Save.Count; i++){
				if(Save[i].save_id == save_id){
					main_data_index = i;
					break;
				}
			}
			if(main_data_index != -1){
				int data_index = -1;
				for(int i = 0; i < Save[main_data_index].data.Count; i++){
					if(Save[main_data_index].data[i].name == name){
						data_index = i;
						break;
					}
				}
				if(data_index != -1){
					Save[main_data_index].data.Remove(Save[main_data_index].data[data_index]);
					Save[main_data_index].data.Add(newSaveData(name, obj));
				} else {
					Save.Add(newSaveStruct(save_id));
					Save[main_data_index].data.Add(newSaveData(name, obj));
				}
			} else {
				Save.Add(newSaveStruct(save_id));
				Save[Save.Count - 1].data.Add(newSaveData(name, obj));
			}
		}

		public void ClearSave(){
			List<SaveStruct> saveStruct = new List<SaveStruct>();
			Save = saveStruct;
		}

		public bool isExistsSaveID(int save_id){
			bool isExists = false;
			for(int i = 0; i < Save.Count; i++){
				if(Save[i].save_id == save_id){
					isExists = true;
					break;
				}
			}
			return isExists;
		}

		public void SaveInFile(string file_name){
			Debug.Log(JsonUtility.ToJson(this));
			if(File.Exists("C:\\Saves\\" + file_name + ".json")){
				File.WriteAllText("C:\\Saves\\" + file_name + ".json", JsonUtility.ToJson(this));
			} else {
				File.Create("C:\\Saves\\" + file_name + ".json");
				File.WriteAllText("C:\\Saves\\" + file_name + ".json", JsonUtility.ToJson(this));
			}
			PlayerPrefs.Save();
		}
		public void LoadFromFile(string file_name){
			//string str_save = PlayerPrefs.GetString(file_name);
			string str_save = File.ReadAllText("C:\\Saves\\" + file_name + ".json");
			SaveClass save_tmp = new SaveClass();
			save_tmp = JsonUtility.FromJson<SaveClass>(str_save);
			Save = save_tmp.Save;
		}
	}
}