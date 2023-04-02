using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGroupController : MonoBehaviour
{
    public DataManager dataMngr;
    public int idControlled;
    public int orbCount;
    public OrbGroupController[] orbGroupControllers; // They need to know each other to not control the same orbgroup at the same time

    [Header("PS Parameters")]
    [Range(0, 400000)]
    public float rate;
    [Range(0, 200)]
    public float life;
    [ColorUsage(true, true)]
    public Color color;
    public int colorIntensity;
    [Range(0, 1)]
    public float alpha;
    [Range(0, 100)]
    public float size;
    [Range(0, 10)]
    public float drag;
    [Range(0, 1)]
    public float velocityDrag;

    [Header("Emitter Parameters")]
    public OrbGroup.EmitterShape emitterShape;
    public OrbGroup.EmitterPlacementMode emitterPlacementMode;
    public float emitterSize;
    public bool emitFromInside;
    public bool activateCollision;

    private OrbsManager _orbsManager;
    private int _idControlled = -1;

    // Start is called before the first frame update
    void Start()
    {
        _orbsManager = transform.parent.GetComponent<OrbsManager>();

        OxipitalData loadedData = dataMngr.LoadData();
        foreach(OrbGroupControllerData ogcData in loadedData.orbGroupControllersData)
		{
            if(ogcData.name == this.name)
			{
                idControlled = ogcData.idControlled;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {

        OrbGroup selectedOrbGroup = null;

        // Our id was updated then we need to update controller according to values
        if(idControlled != _idControlled)
		{
            // If it is already controlled by another OrbGroupController, don't update parameters
            if(!CheckAvailability())
            {
                rate = 0;
                life = 0;
                orbCount = 0;
                color = Color.black;
                colorIntensity = 0;
                alpha = 0;
                size = 0;
                drag = 0;
                velocityDrag = 0;
                emitterSize = 0;
                emitFromInside = false;
                activateCollision = false;

                Debug.Log("Cannot control an OrbGroup already controlled");

                _idControlled = -1;

                return;
            }

            // Update data from the newly connected orbGroup
            foreach (OrbGroup oG in _orbsManager.orbs)
            {
                if (oG.orbGroupId == idControlled)
                {
                    orbCount = oG.GetOrbCount();
                    rate = oG.rate;
                    life = oG.life;
                    color = oG.color;
                    colorIntensity = oG.colorIntensity;
                    alpha = oG.alpha;
                    size = oG.size;
                    drag = oG.drag;
                    velocityDrag = oG.velocityDrag;
                    emitterShape = oG.emitterShape;
                    emitterPlacementMode = oG.emitterPlacementMode;
                    emitterSize = oG.emitterSize;
                    emitFromInside = oG.emitFromInside;
                    activateCollision = oG.activateCollision;

                    _idControlled = idControlled;
                }
            }
        }
        else
		{
            foreach (OrbGroup oG in _orbsManager.orbs)
            {
                if (oG.orbGroupId == idControlled)
                { 
                    selectedOrbGroup = oG;
                    oG.SetOrbCount(orbCount);
                    oG.rate = rate;
                    oG.life = life;
                    oG.color = color;
                    oG.colorIntensity = colorIntensity;
                    oG.alpha = alpha;
                    oG.size = size;
                    oG.drag = drag;
                    oG.velocityDrag = velocityDrag;
                    oG.emitterShape = emitterShape;
                    oG.emitterPlacementMode = emitterPlacementMode;
                    oG.emitterSize = emitterSize;
                    oG.emitFromInside = emitFromInside;
                    oG.activateCollision = activateCollision;
                }
            }
        }
    }

    public bool CheckAvailability()
	{
        bool result = true;

        foreach(OrbGroupController ogc in orbGroupControllers)
		{
            if(ogc.idControlled == idControlled)
            {
                result = false;
            }
		}

        return result;
	}
}

[System.Serializable]
public class OrbGroupControllerData
{
    public int idControlled;
    public string name;
}