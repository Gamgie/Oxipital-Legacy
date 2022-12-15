using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[ExecuteInEditMode]
public class OrbGroup : MonoBehaviour
{
    public enum EmitterShape { Sphere, Plane, Torus, Cube, Pipe, Egg, Line, Circle, Merkaba }
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
    [ColorUsage(true,true)]
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


    private List<VisualEffect> visualEffects;
    private int emitterShapeIndex;


    // Start is called before the first frame update
    public void Initialize()
    {
        emitterShapeIndex = GetEmitterShapeIndex();

        // Initialize with the first orb
        visualEffects = new List<VisualEffect>();
        AddOrb();
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
            Mesh actualMesh = vfx.GetMesh("Emitter Mesh");
            if (actualMesh != meshArray[emitterShapeIndex])
            {
                vfx.SetMesh("Emitter Mesh", meshArray[emitterShapeIndex]);
                vfx.SetTexture("Collision SDF", sdfCollisionArray[emitterShapeIndex]);
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
        }

        return result;
    }

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
        if(visualEffects == null)
		{
            Debug.LogError("Try to add an Orb in a null list from " + gameObject.name);
            return;
		}

        VisualEffect o = Instantiate(orbPrefab).GetComponent<VisualEffect>();
        o.name = "Orb" + orbGroupId + "_" + visualEffects.Count;
        o.transform.parent = transform;
        visualEffects.Add(o);
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
}
