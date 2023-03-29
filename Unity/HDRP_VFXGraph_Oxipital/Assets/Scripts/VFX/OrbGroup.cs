using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[ExecuteInEditMode]
public class OrbGroup : MonoBehaviour
{
    public enum EmitterShape { Sphere, Plane, Torus, Cube, Pipe, Egg, Line, Circle, Merkaba, Pyramid, Triangle }
    public enum EmitterPlacementMode
    {
        Surface,
        Edge
    }

    public int orbGroupId;
    public VisualEffect orbPrefab;

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

    [Header("Emitter Parameters")]
    public EmitterShape emitterShape;
    public EmitterPlacementMode emitterPlacementMode;
    public Vector3 emitterPosition;
    public Vector3 emitterOrientation;
    public float emitterSize;
    public Mesh[] meshArray;
    public Texture[] sdfCollisionArray;
    public bool emitFromInside;
    public bool activateCollision;

    public OrbGroupData data = new OrbGroupData();

    private List<VisualEffect> visualEffects;
    private int emitterShapeIndex;
    private OrbsManager _orbsMngr;


    // Start is called before the first frame update
    public void Initialize(OrbsManager orbsMngr)
    {
        _orbsMngr = orbsMngr;

        // Initialize with the first orb
        visualEffects = new List<VisualEffect>();
        AddOrb();

        // Update shape according to index
        SetEmitterShape();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEmitter();

        UpdateVisualEffect();
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
            if (vfx.HasVector3("Emitter Position") == true)
            {
                vfx.SetVector3("Emitter Position", emitterPosition);
            }
            if (vfx.HasVector3("Emitter Angles") == true)
            {
                vfx.SetVector3("Emitter Angles", emitterOrientation);
            }
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
            case EmitterShape.Triangle:
                result = 10;
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
            case 10:
                emitterShape = EmitterShape.Triangle;
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

        _orbsMngr.GetOnOrbCreated().Invoke();
        Debug.Log(name + " / " + o.name + " created.");
    }

    void DestroyOrb(int index)
    {
        if (visualEffects == null)
        {
            Debug.LogError("Try to destroy an Orb in a null list from " + gameObject.name);
            return;
        }

        VisualEffect orbToBeDestroyed = visualEffects[index];
        Destroy(orbToBeDestroyed.gameObject);
        visualEffects.RemoveAt(index);
    }

	#region ManageData
    public OrbGroupData StoreData()
    { 
        data.orbGroupId = orbGroupId;
        data.name = name;
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
        data.emitterShapeIndex = GetEmitterShapeIndex();
        data.emitterPlacementMode = (int)emitterPlacementMode;
        data.emitterPositionX = emitterPosition.x;
        data.emitterPositionY = emitterPosition.y;
        data.emitterPositionZ = emitterPosition.z;
        data.emitterOrientationX = emitterOrientation.x;
        data.emitterOrientationY = emitterOrientation.y;
        data.emitterOrientationZ = emitterOrientation.z;
        data.emitterSize = emitterSize;
        data.emitFromInside = emitFromInside;
        data.activateCollision = activateCollision;

        return data;
    }

    public void LoadData()
    {
        orbGroupId = data.orbGroupId;
        name = data.name;
        rate = data.rate;
        life = data.life;
        color = new Color(data.colorR, data.colorG, data.colorB);
        colorIntensity = data.colorIntensity;
        alpha = data.alpha;
        size = data.size;
        drag = data.drag;
        velocityDrag = data.velocityDrag;
        emitterShapeIndex = data.emitterShapeIndex;
        emitterPlacementMode = (EmitterPlacementMode)data.emitterPlacementMode;
        emitterPosition = new Vector3(data.emitterPositionX, data.emitterPositionY, data.emitterPositionZ);
        emitterOrientation = new Vector3(data.emitterOrientationX, data.emitterOrientationY, data.emitterOrientationZ);
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
