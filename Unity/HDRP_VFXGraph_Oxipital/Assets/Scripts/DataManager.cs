using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public string fileName;
    public OrbsManager orbsMngr;
	public OrbGroupController[] orbsControllers;
	private string path;
	private OrbManagerData loadedData;

	public OrbManagerData LoadData()
	{
		if(path == null)
		{
			path = Application.dataPath;
		}

		if(loadedData == null)
		{
			string filepath = path + "/" + fileName + ".json";
			string orbsData = System.IO.File.ReadAllText(filepath);

			loadedData = JsonUtility.FromJson<OrbManagerData>(orbsData);

			Debug.Log("file loaded at " + path + "/" + fileName);
		}

		return loadedData;
	}

	public void SaveData()
	{
		if(orbsMngr == null)
		{
			Debug.LogError("Can not save file because there is no vfxcontroller found");
			return;
		}

		// save data of orb mngr
		OrbManagerData data = new OrbManagerData();
		data.orbCount = orbsMngr.orbGroupCount;

		// save data for each OrbGroup
		data.orbGroupData = new List<OrbGroupData>();
		foreach(OrbGroup o in orbsMngr.orbs)
		{
			data.orbGroupData.Add(o.StoreData());

		}

		// save data for each OrbGroupController
		data.orbGroupControllersData = new List<OrbGroupControllerData>();
		foreach(OrbGroupController oc in orbsControllers)
		{
			OrbGroupControllerData ogcData = new OrbGroupControllerData();
			ogcData.name = oc.name;
			ogcData.idControlled = oc.idControlled;
			data.orbGroupControllersData.Add(ogcData);
		}

		// Save to file
		string orbsData = JsonUtility.ToJson(data);
		string filepath = path + "/" + fileName + ".json";
		System.IO.File.WriteAllText(filepath, orbsData);
		Debug.Log("file saved at " + filepath);
	}
}
