using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public string fileName;
    public OrbsManager orbsMngr;
	public OrbGroupController[] orbsControllers;
	public BalletManager balletMngr;

	private string path;
	private OxipitalData loadedData;

	public OxipitalData LoadData()
	{
		if(path == null)
		{
			path = Application.dataPath;
		}

		if(loadedData == null)
		{
			string filepath = path + "/" + fileName + ".json";
			string orbsData = System.IO.File.ReadAllText(filepath);

			loadedData = JsonUtility.FromJson<OxipitalData>(orbsData);

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

		// Save data of orb mngr.
		OxipitalData data = new OxipitalData();
		data.orbCount = orbsMngr.orbGroupCount;

		// Save data for each OrbGroup.
		data.orbGroupData = new List<OrbGroupData>();
		foreach(OrbGroup o in orbsMngr.orbs)
		{
			data.orbGroupData.Add(o.StoreData());

		}

		// Save data for each OrbGroupController.
		data.orbGroupControllersData = new List<OrbGroupControllerData>();
		foreach(OrbGroupController oc in orbsControllers)
		{
			OrbGroupControllerData ogcData = new OrbGroupControllerData();
			ogcData.name = oc.name;
			ogcData.idControlled = oc.idControlled;
			data.orbGroupControllersData.Add(ogcData);
		}

		// Save patterns data of the Ballet 
		data.balletMngrData = new BalletManagerData();
		foreach(BalletPattern pattern in balletMngr.orbsPatterns)
		{
			data.balletMngrData.orbsPatternData.Add(pattern.StoreData());
		}

		// Save to file
		string orbsData = JsonUtility.ToJson(data);
		string filepath = path + "/" + fileName + ".json";
		System.IO.File.WriteAllText(filepath, orbsData);
		Debug.Log("file saved at " + filepath);
	}
}

[System.Serializable]
public class OxipitalData
{
	public int orbCount;
	public List<OrbGroupData> orbGroupData;
	public List<OrbGroupControllerData> orbGroupControllersData;
	public BalletManagerData balletMngrData;
}