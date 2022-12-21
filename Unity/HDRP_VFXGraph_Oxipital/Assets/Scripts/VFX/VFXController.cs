using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using System;

public class VFXController : MonoBehaviour
{
    public List<OrbGroup> orbs;

    [Header("Orbs Positions")]
    public int orbGroupCount;
    public OrbGroup orbGroupPrefab;
    public Transform emitterOrbRoot;
    public DataManager dataManager;

    private UnityEvent OnOrbCreated;

	void OnEnable()
	{
        OnOrbCreated = new UnityEvent();
    }

	// Start is called before the first frame update
	void Start()
    {
        if(orbs != null)
        {
            foreach(OrbGroup o in orbs)
            {
                Destroy(o.gameObject);
            }
        }

        InitOrbs();
    }

    // Update is called once per frame
    void Update()
    {
        if(orbGroupCount > orbs.Count)
        {
            AddOrbGroup();
        }
        else if(orbGroupCount < orbs.Count)
        {
            DestroyOrbGroup(orbs.Count - 1);
        }
    }


    private void OnDestroy()
    {
        // Save how many orbs were there
        dataManager.SaveData("default.json");
    }

    public void KillAllParticles()
    {
        if (orbs == null)
            return;

        foreach (OrbGroup o in orbs)
        {
            o.Reinit();
        }
    }

    public void InitOrbs()
	{
        orbs = new List<OrbGroup>();
        VFXControllerData loadedData = dataManager.LoadData("default.json");
        orbGroupCount = loadedData.orbCount;

        for (int i = 0; i < orbGroupCount; i++)
        {
            if (orbGroupCount > orbs.Count)
            {
                AddOrbGroup(loadedData.orbGroupData[i]);
            }
        }
    }
    public void AddOrbGroup(OrbGroupData orbData = null)
    {
        if (orbs == null)
        {
            Debug.LogError("Try to add an orb in a null list");
            return;
        }

        OrbGroup o = Instantiate(orbGroupPrefab);
        if(orbData == null)
		{
            o.gameObject.name = "OrbGroup" + orbs.Count;
            o.orbGroupId = orbs.Count;
        }
        else
		{
            o.data = orbData;
            o.LoadData();
		}
        
        o.transform.parent = emitterOrbRoot;
        o.Initialize(this);
        orbs.Add(o);
    }

    public void DestroyOrbGroup(int index)
    {
        if (orbs == null)
        {
            Debug.LogError("Try to destroy an orb in a null list");
            return;
        }

        OrbGroup orbToBeDestroyed = orbs[index];
        Destroy(orbToBeDestroyed.gameObject);
        orbs.RemoveAt(index);
    }

    public UnityEvent GetOnOrbCreated()
	{
        if(OnOrbCreated == null)
		{
            OnOrbCreated = new UnityEvent();
        }

        return OnOrbCreated;
	}
}

[System.Serializable]
public class VFXControllerData
{
    public int orbCount;
    public List<OrbGroupData> orbGroupData;
}



//private void UpdateOrbPosition()
//{
//    if (orbsVisualEffect == null)
//    {
//        Debug.LogError("No orb list found");
//        return;
//    }

//    for (int i = 0; i < orbsVisualEffect.Count; i++)
//    {
//        float xPos = orbPositionRadius * Mathf.Cos(i * 360 / orbGroupCount * Mathf.Deg2Rad);
//        float yPos = orbPositionRadius * Mathf.Sin(i * 360 / orbGroupCount * Mathf.Deg2Rad);
//        orbsVisualEffect[i].emitterPosition = new Vector3(xPos,yPos,0);
//    }
//}

//public void ResetOrbPosition()
//{
//    emitterOrb.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0,0,0));
//}