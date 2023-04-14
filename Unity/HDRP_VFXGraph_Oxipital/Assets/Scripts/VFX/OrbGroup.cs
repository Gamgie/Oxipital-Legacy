using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[ExecuteInEditMode]
public class OrbGroup : MonoBehaviour
{
    public enum EmitterShape { Sphere, Plane, Torus, Cube, Pipe, Egg, Line, Circle, Merkaba, Pyramid }
    public enum EmitterPlacementMode
    {
        Surface,
        Edge
    }

    public int orbGroupId;
    public VisualEffect orbPrefab;
    public int patternID = -1;

    [Header("PS Parameters")]
    [Range(0, 4000)]
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
    public float drag;
    [Range(0, 1)]
    public float velocityDrag;
    public bool staticParticle;
    public bool stationaryTransparent;

    [Header("Emitter Parameters")]
    public EmitterShape emitterShape;
    public EmitterPlacementMode emitterPlacementMode;
    public Vector3 emitterPosition;
    public Vector3 emitterRotation;
    public float emitterSize;
    public Mesh[] meshArray;
    public Texture[] sdfCollisionArray;
    public bool emitFromInside;
    public bool activateCollision;

    public OrbGroupData data = new OrbGroupData();

    private List<VisualEffect> visualEffects;
    private int emitterShapeIndex;
    private OrbsManager orbsMngr;
    private BalletPattern pattern;
    private int orbCount = 0;

    public void Initialize(OrbsManager orbsMngr)
    {
        this.orbsMngr = orbsMngr;        
        visualEffects = new List<VisualEffect>();

        // Add a pattern linked to this orbGroup.
        pattern = orbsMngr.balletMngr.AddPattern();
        patternID = pattern.id;

        // Initialize with the first orb
        SetOrbCount(orbCount);

        // Update shape according to index
        SetEmitterShape();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEmitter();

        UpdateVisualEffect();

        // Update pattern link
        if(patternID != -1) // Update only if we have selected a pattern id
		{
            if(pattern != null)
            {
                if(pattern.id != patternID)
				{
                    pattern = orbsMngr.GetPattern(patternID); // Id are diferent so we should update
                }
            }
            else // If pattern is null, it means we have to update also
			{
                pattern = orbsMngr.GetPattern(patternID);
            }

            if (pattern == null)
                patternID = -1;
        }

        UpdatePositions();
    }

    void UpdateVisualEffect()
    {
        foreach (VisualEffect vfx in visualEffects)
        {
            if (vfx == null)
            {
                Debug.LogError(vfx.gameObject.name + " should have a visual effect attached to it.");
                return;
            }

            // Ps parameters update
            if (vfx.HasInt("Rate") == true)
                vfx.SetInt("Rate", Convert.ToInt32(rate));

            if (vfx.HasFloat("LifeTime") == true)
                vfx.SetFloat("LifeTime", life);

            if (vfx.HasFloat("Alpha") == true)
                vfx.SetFloat("Alpha", alpha);

            if (vfx.HasFloat("Size") == true)
                vfx.SetFloat("Size", size);

            if (vfx.HasFloat("Linear Drag") == true)
                vfx.SetFloat("Linear Drag", drag);

            if (vfx.HasFloat("Velocity Drag") == true)
                vfx.SetFloat("Velocity Drag", velocityDrag);

            float factor = Mathf.Pow(2, colorIntensity);
            if (vfx.HasVector4("Color") == true)
                vfx.SetVector4("Color", new Vector3(color.r * factor, color.g * factor, color.b * factor));

            if (vfx.HasBool("Static Particle") == true)
                vfx.SetBool("Static Particle", staticParticle);

            if (vfx.HasBool("Stationary Transparent") == true)
                vfx.SetBool("Stationary Transparent", stationaryTransparent);
        }
    }

    void UpdateEmitter()
    {
        foreach (VisualEffect vfx in visualEffects)
        {
            // If no emitter mesh in graph then no need to go further
            if (vfx.HasMesh("Emitter Mesh") == false)
                return;

            // Update Emitter transform
            if (vfx.HasVector3("Emitter Angles") == true)
                vfx.SetVector3("Emitter Angles", emitterRotation);

            if (vfx.HasFloat("Emitter Scale") == true)
                vfx.SetFloat("Emitter Scale", emitterSize);

            // Check if we need to update mesh in graph
            emitterShapeIndex = GetEmitterShapeIndex();
            if(emitterShape == EmitterShape.Line)
			{
                if (vfx.HasBool("isCircle") == true)
                    vfx.SetBool("isCircle", false);

                if (vfx.HasBool("isLine") == true)
                    vfx.SetBool("isLine", true);
            }
            else if (emitterShape == EmitterShape.Circle)
			{
                if (vfx.HasBool("isLine") == true)
                    vfx.SetBool("isLine", false);

                if (vfx.HasBool("isCircle") == true)
                    vfx.SetBool("isCircle", true);
            }
			else
            {
                if (vfx.HasBool("isLine") == true)
                    vfx.SetBool("isLine", false);

                if (vfx.HasBool("isCircle") == true)
                    vfx.SetBool("isCircle", false);

                Mesh actualMesh = vfx.GetMesh("Emitter Mesh");
                if (actualMesh != meshArray[emitterShapeIndex])
                {
                    vfx.SetMesh("Emitter Mesh", meshArray[emitterShapeIndex]);
                    vfx.SetTexture("Collision SDF", sdfCollisionArray[emitterShapeIndex]);
                }
            }

            if (vfx.HasBool("Emit From Inside") == true)
                vfx.SetBool("Emit From Inside", emitFromInside);

            if (vfx.HasBool("Activate Collision") == true)
                vfx.SetBool("Activate Collision", activateCollision);

            if (vfx.HasInt("Emitter Placement Mode") == true)
            {
                int switchPlacementMode = -1;

                switch (emitterPlacementMode)
                {
                    case EmitterPlacementMode.Surface:
                        switchPlacementMode = 0;
                        break;
                    case EmitterPlacementMode.Edge:
                        switchPlacementMode = 1;
                        break;
                    default:
                        break;
                }

                vfx.SetInt("Emitter Placement Mode", switchPlacementMode);
            }
        }
    }

    void UpdatePositions()
	{
        if (pattern == null)
            return;

        for(int i = 0; i < visualEffects.Count; i++)
		{
            if (visualEffects[i].HasVector3("Emitter Position") == true)
            {
                visualEffects[i].SetVector3("Emitter Position", pattern.GetPosition(i));
            }
		}
	}

    private int GetEmitterShapeIndex()
    {
        int result = -1;

        switch (emitterShape)
        {
            case EmitterShape.Sphere:
                result = 0;
                break;
            case EmitterShape.Plane:
                result = 1;
                break;
            case EmitterShape.Cube:
                result = 2;
                break;
            case EmitterShape.Torus:
                result = 3;
                break;
            case EmitterShape.Pipe:
                result = 4;
                break;
            case EmitterShape.Egg:
                result = 5;
                break;
            case EmitterShape.Line:
                result = 6;
                break;
            case EmitterShape.Circle:
                result = 7;
                break;
            case EmitterShape.Merkaba:
                result = 8;
                break;
            case EmitterShape.Pyramid:
                result = 9;
                break;
        }

        return result;
    }

    private void SetEmitterShape()
	{
        switch (emitterShapeIndex)
        {
            case 0 :
                emitterShape = EmitterShape.Sphere;
                break;
            case 1:
                emitterShape = EmitterShape.Plane;
                break;
            case 2:
                emitterShape = EmitterShape.Cube;
                break;
            case 3:
                emitterShape = EmitterShape.Torus;
                break;
            case 4:
                emitterShape = EmitterShape.Pipe;
                break;
            case 5:
                emitterShape = EmitterShape.Egg;
                break;
            case 6:
                emitterShape = EmitterShape.Line;
                break;
            case 7:
                emitterShape = EmitterShape.Circle;
                break;
            case 8:
                emitterShape = EmitterShape.Merkaba;
                break;
            case 9:
                emitterShape = EmitterShape.Pyramid;
                break;
        }
    }

    // Reset all particle system in this orbgroup
    public void Reinit()
    {
        foreach (VisualEffect vfx in visualEffects)
        {
            if (vfx == null)
            {
                Debug.LogError("Trying to reinit an invalid object");
                return;
            }

            vfx.Reinit();
        }
    }

    void AddOrb()
    {
        if (visualEffects == null)
        {
            Debug.LogError("Try to add an Orb in a null list from " + gameObject.name);
            return;
        }

        VisualEffect o = Instantiate(orbPrefab).GetComponent<VisualEffect>();
        o.name = "Orb" + orbGroupId + "_" + visualEffects.Count;
        o.transform.parent = transform;
        visualEffects.Add(o);

        // Add a dancer in pattern
        if(pattern != null)
		{
            if(pattern.dancerCount < GetOrbCount())
                pattern.AddDancer();
		}
        else
		{
            Debug.LogError("No pattern found in " + this.gameObject.name);
		}

        // Send a message to the manager
        orbsMngr.GetOnOrbCreated().Invoke();
        Debug.Log(name + " / " + o.name + " created.");

        // Update OrbCount accordingly
        orbCount = visualEffects.Count;
    }

    void DestroyOrb(int index = -1)
    {
        if (visualEffects == null)
        {
            Debug.LogError("Try to destroy an Orb in a null list from " + gameObject.name);
            return;
        }

        VisualEffect orbToBeDestroyed = null;

        if (index == -1) // Remove last one
		{
            orbToBeDestroyed = visualEffects[visualEffects.Count-1];
            pattern.RemoveDancer();
            index = visualEffects.Count - 1;
        }
        else // Remove at index
		{
            orbToBeDestroyed = visualEffects[index];
            pattern.RemoveDancer(index);
        }
 
        Destroy(orbToBeDestroyed.gameObject);
        visualEffects.RemoveAt(index);

        // Update OrbCount accordingly
        orbCount = visualEffects.Count;
        orbsMngr.GetOnOrbCreated().Invoke();
    }

    public int GetOrbCount()
	{
        return orbCount;
	}

    public void SetOrbCount(int count)
	{
        pattern.UpdateDancerCount(count);

        // Update list of vfx.
        if (count > visualEffects.Count)
        {
            int instanceCount = pattern.dancerCount - visualEffects.Count;
            for (int i = 0; i < instanceCount; i++)
            {
                AddOrb();
            }
        }
        else
        {
            int removeCount = visualEffects.Count - count;
            for (int i = 0; i < removeCount; i++)
            {
                DestroyOrb();
            }
        }
    }

	private void OnDestroy()
	{
        orbsMngr.balletMngr.RemovePattern(orbGroupId);
    }

	#region ManageData
	public OrbGroupData StoreData()
    { 
        data.orbGroupId = orbGroupId;
        data.name = name;
        data.orbCount = GetOrbCount();
        data.rate = rate;
        data.life = life;
        data.colorR = color.r;
        data.colorG = color.g;
        data.colorB = color.b;
        data.colorIntensity = colorIntensity;
        data.alpha = alpha;
        data.size = size;
        data.drag = drag;
        data.velocityDrag = velocityDrag;
        data.staticParticle = staticParticle;
        data.stationaryTransparent = stationaryTransparent;
        data.emitterShapeIndex = GetEmitterShapeIndex();
        data.emitterPlacementMode = (int)emitterPlacementMode;
        data.emitterPositionX = emitterPosition.x;
        data.emitterPositionY = emitterPosition.y;
        data.emitterPositionZ = emitterPosition.z;
        data.emitterOrientationX = emitterRotation.x;
        data.emitterOrientationY = emitterRotation.y;
        data.emitterOrientationZ = emitterRotation.z;
        data.emitterSize = emitterSize;
        data.emitFromInside = emitFromInside;
        data.activateCollision = activateCollision;

        return data;
    }

    public void LoadData()
    {
        orbGroupId = data.orbGroupId;
        name = data.name;
        orbCount = data.orbCount;
        rate = data.rate;
        life = data.life;
        color = new Color(data.colorR, data.colorG, data.colorB);
        colorIntensity = data.colorIntensity;
        alpha = data.alpha;
        size = data.size;
        drag = data.drag;
        velocityDrag = data.velocityDrag;
        staticParticle = data.staticParticle;
        stationaryTransparent = data.stationaryTransparent;
        emitterShapeIndex = data.emitterShapeIndex;
        emitterPlacementMode = (EmitterPlacementMode)data.emitterPlacementMode;
        emitterPosition = new Vector3(data.emitterPositionX, data.emitterPositionY, data.emitterPositionZ);
        emitterRotation = new Vector3(data.emitterOrientationX, data.emitterOrientationY, data.emitterOrientationZ);
        emitterSize = data.emitterSize;
        emitFromInside = data.emitFromInside;
        activateCollision = data.activateCollision;
    }
	#endregion
}

[System.Serializable]
public class OrbGroupData
{
    public int orbGroupId;
    public string name;
    public int orbCount;
    public float rate;
    public float life; 
    public float colorR;
    public float colorG;
    public float colorB;
    public int colorIntensity;
    public float alpha;
    public float size;
    public float drag;
    public float velocityDrag;
    public bool staticParticle;
    public bool stationaryTransparent;
    public int emitterShapeIndex;
    public int emitterPlacementMode;
    public float emitterPositionX;
    public float emitterPositionY;
    public float emitterPositionZ;
    public float emitterOrientationX;
    public float emitterOrientationY;
    public float emitterOrientationZ;
    public float emitterSize;
    public bool emitFromInside;
    public bool activateCollision;
}
