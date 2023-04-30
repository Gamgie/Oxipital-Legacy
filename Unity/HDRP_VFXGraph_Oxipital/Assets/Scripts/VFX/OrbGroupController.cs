using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGroupController : MonoBehaviour
{
    public DataManager dataMngr;
    public BalletManager balletMngr;
    public int idControlled;
    public int orbCount;
    public OrbGroupController[] orbGroupControllers; // They need to know each other to not control the same orbgroup at the same time

    [Header("PS Parameters")]
    [Range(0, 200000)]
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
    [Range(0, 20)]
    public float noisyDrag;
    [Range(0, 5)]
    public float noisyDragFrequency;
    public bool staticParticle;
    public bool stationaryTransparent;
    [Range(0,30)]
    public float stationaryMaxSpeed; // When in stationary, we interpolat alpha according to speed. This the max speed for alpha to reach value 1.

    [Header("Emitter Parameters")]
    public OrbGroup.EmitterShape emitterShape;
    public OrbGroup.EmitterPlacementMode emitterPlacementMode;
    public float emitterSize;
    public bool emitFromInside;
    public bool activateCollision;
    public bool showMesh;
    public Vector3 emitterRotation; // Rotation in euler angle of the emitter


    [Header("Ballet Pattern Parameters")]
    public BalletPattern.BalletPatternType patternType = BalletPattern.BalletPatternType.Circle;
    public Vector3 position; // Position of this pattern
    public Vector3 patternRotation = Vector3.zero; // Rotation in euler angle of this pattern
    [Range(0, 10)]
    public float patternSize = 1; // Size of this pattern
    [Range(0, 30)]
    public float speed = 1f; // speed of the choreography
    [Range(0, 20)]
    public float lerpDuration = 3f; // Time for moving from a pattern to another
    [Range(0, 1)]
    public float phase; // Rotation phase

    [Header("Size LFO")]
    [Range(0, 2)]
    public float sizeLFOFrequency;
    [Range(0, 3)]
    public float sizeLFOAmplitude;

    [Header("Circle Parameter")]
    [Range(0, 5)]
    public float verticalOffset;

    [Header("Position Alteration")]
    [Range(0, 5)]
    public float LFOFrequency = 0;
    public Vector3 LFODirection = Vector3.zero;
    [Range(0, 3)]
    public float noiseAmplitude = 0;
    [Range(0, 10)]
    public float noiseSpeed = 0;

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
                noisyDrag = 0;
                noisyDragFrequency = 0;
                staticParticle = false;
                stationaryTransparent = false;
                stationaryMaxSpeed = 0;
                emitterSize = 0;
                emitFromInside = false;
                activateCollision = false;
                showMesh = false;

                position = Vector3.zero;
                patternRotation = Vector3.zero;
                emitterRotation = Vector3.zero;
                size = 0;
                speed = 0;
                lerpDuration = 0;
                phase = 0;
                sizeLFOFrequency = 0;
                sizeLFOAmplitude = 0;
                verticalOffset = 0;
                LFOFrequency = 0;
                LFODirection = Vector3.zero;
                noiseAmplitude = 0;
                noiseSpeed = 0;

                Debug.Log("Cannot control an OrbGroup already controlled");

                _idControlled = -1;

                return;
            }

            // Update data from the newly connected orbGroup
            foreach (OrbGroup oG in _orbsManager.orbs)
            {
                if (oG.orbGroupId == idControlled)
                {
                    // Get Orb parameters
                    orbCount = oG.GetOrbCount();
                    rate = oG.rate;
                    life = oG.life;
                    color = oG.color;
                    colorIntensity = oG.colorIntensity;
                    alpha = oG.alpha;
                    size = oG.size;
                    drag = oG.drag;
                    velocityDrag = oG.velocityDrag;
                    noisyDrag = oG.noisyDrag;
                    noisyDragFrequency = oG.noisyDragFrequency;
                    staticParticle = oG.staticParticle;
                    stationaryTransparent = oG.stationaryTransparent;
                    stationaryMaxSpeed = oG.stationaryMaxSpeed;
                    emitterShape = oG.emitterShape;
                    emitterPlacementMode = oG.emitterPlacementMode;
                    emitterSize = oG.emitterSize;
                    emitFromInside = oG.emitFromInside;
                    activateCollision = oG.activateCollision;
                    showMesh = oG.showMesh;
                    emitterRotation = oG.emitterRotation;

                    // Get ballet pattern parameters
                    BalletPattern balletPattern = balletMngr.GetPattern(BalletManager.PatternGroup.Orb,idControlled);
                    patternType = balletPattern.patternType;
                    position = balletPattern.position;
                    patternRotation = balletPattern.rotation;
                    patternSize = balletPattern.size;
                    speed = balletPattern.speed;
                    lerpDuration = balletPattern.lerpDuration;
                    phase = balletPattern.phase;
                    sizeLFOFrequency = balletPattern.sizeLFOFrequency;
                    sizeLFOAmplitude = balletPattern.sizeLFOAmplitude;
                    verticalOffset = balletPattern.verticalOffset;
                    LFOFrequency = balletPattern.LFOFrequency;
                    LFODirection = balletPattern.LFODirection;
                    noiseAmplitude = balletPattern.noiseAmplitude;
                    noiseSpeed = balletPattern.noiseSpeed;

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
                    // Update orb parameters
                    oG.SetOrbCount(orbCount);
                    oG.rate = rate;
                    oG.life = life;
                    oG.color = color;
                    oG.colorIntensity = colorIntensity;
                    oG.alpha = alpha;
                    oG.size = size;
                    oG.drag = drag;
                    oG.velocityDrag = velocityDrag;
                    oG.noisyDrag = noisyDrag;
                    oG.noisyDragFrequency = noisyDragFrequency;
                    oG.staticParticle = staticParticle;
                    oG.stationaryTransparent = stationaryTransparent;
                    oG.stationaryMaxSpeed = stationaryMaxSpeed;
                    oG.emitterShape = emitterShape;
                    oG.emitterPlacementMode = emitterPlacementMode;
                    oG.emitterSize = emitterSize;
                    oG.emitFromInside = emitFromInside;
                    oG.activateCollision = activateCollision;
                    oG.showMesh = showMesh;
                    oG.emitterRotation = emitterRotation;

                    // Update ballet pattern
                    BalletPattern balletPattern = balletMngr.GetPattern(BalletManager.PatternGroup.Orb,idControlled);
                    balletPattern.patternType = patternType;
                    balletPattern.position = position;
                    balletPattern.rotation = patternRotation;
                    balletPattern.size = patternSize;
                    balletPattern.speed = speed;
                    balletPattern.lerpDuration = lerpDuration;
                    balletPattern.phase = phase;
                    balletPattern.sizeLFOFrequency = sizeLFOFrequency;
                    balletPattern.sizeLFOAmplitude = sizeLFOAmplitude;
                    balletPattern.verticalOffset = verticalOffset;
                    balletPattern.LFOFrequency = LFOFrequency;
                    balletPattern.LFODirection = LFODirection;
                    balletPattern.noiseAmplitude = noiseAmplitude;
                    balletPattern.noiseSpeed = noiseSpeed;
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