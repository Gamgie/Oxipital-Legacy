using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public VFXController vfxController;
	private string path;

	public VFXControllerData LoadData(string fileName)
	{
		if(path == null)
		{
			path = Application.dataPath;
		}

		string filepath = path + "/" + fileName;
		string orbsData =  System.IO.File.ReadAllText(path + "/" + fileName);

		VFXControllerData data = JsonUtility.FromJson<VFXControllerData>(orbsData);

		Debug.Log("file loaded at " + path + "/" + fileName);

		return data;
	}

	public void SaveData(string fileName)
	{
		if(vfxController == null)
		{
			Debug.LogError("Can not save file because there is no vfxcontroller found");
			return;
		}

		// save data of vfxcontroller
		VFXControllerData data = new VFXControllerData();
		data.orbCount = vfxController.orbGroupCount;

		// save data for each OrbGroup
		data.orbGroupData = new List<OrbGroupData>();
		foreach(OrbGroup o in vfxController.orbs)
		{
			data.orbGroupData.Add(o.StoreData());

		}

		// Save to file
		string orbsData = JsonUtility.ToJson(data);
		string filepath = path + "/" + fileName;
		System.IO.File.WriteAllText(filepath, orbsData);
		Debug.Log("file saved at " + filepath);
	}
}
