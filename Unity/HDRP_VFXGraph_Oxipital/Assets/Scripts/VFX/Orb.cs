using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[ExecuteInEditMode]
public class Orb : MonoBehaviour
{
    public enum EmitterShape { Sphere, Plane, Torus, Cube, Pipe, Egg, Line, Circle, Merkaba }
    public enum EmitterPlacementMode
    {
        Surface,
        Edge
    }
    public int orbId;

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
    public bool emitFromInside;


    private VisualEffect _visualEffect;
    private int emitterShapeIndex;

    public VisualEffect Vfx { get => _visualEffect; set => _visualEffect = value; }

    // Start is called before the first frame update
    void OnEnable()
    {
        Vfx = GetComponent<VisualEffect>();
        emitterShapeIndex = GetEmitterShapeIndex();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEmitter();
        
        UpdateVisualEffect();        
    }

    void UpdateVisualEffect()
    {
        if (Vfx == null)
        {
            Debug.LogError(gameObject.name + " should have a visual effect attached to it.");
            return;
        }

        // Ps parameters update
        if (Vfx.HasInt("Rate") == true)
            Vfx.SetInt("Rate", Convert.ToInt32(rate));

        if (Vfx.HasFloat("LifeTime") == true)
            Vfx.SetFloat("LifeTime", life);

        if (Vfx.HasFloat("Alpha") == true)
            Vfx.SetFloat("Alpha", alpha);

        if (Vfx.HasFloat("Size") == true)
            Vfx.SetFloat("Size", size);

        if (Vfx.HasFloat("Linear Drag") == true)
            Vfx.SetFloat("Linear Drag", drag);

        if (Vfx.HasFloat("Velocity Drag") == true)
            Vfx.SetFloat("Velocity Drag", velocityDrag);

        float factor = Mathf.Pow(2, colorIntensity);
        if(Vfx.HasVector4("Color") == true)
            Vfx.SetVector4("Color", new Vector3(color.r* factor, color.g* factor, color.b* factor));
    }

    void UpdateEmitter()
    {
        // If no emitter mesh in graph then no need to go further
        if (Vfx.HasMesh("Emitter Mesh") == false)
            return;

        // Update Emitter transform
        if(Vfx.HasVector3("Emitter Position") == true)
        {
            Vfx.SetVector3("Emitter Position", emitterPosition);
        }
        if (Vfx.HasVector3("Emitter Angles") == true)
        {
            Vfx.SetVector3("Emitter Angles", emitterOrientation);
        }
        if (Vfx.HasFloat("Emitter Scale") == true)
            Vfx.SetFloat("Emitter Scale", emitterSize);

        // Check if we need to update mesh in graph
        emitterShapeIndex = GetEmitterShapeIndex();
        Mesh actualMesh = Vfx.GetMesh("Emitter Mesh");
        if(actualMesh != meshArray[emitterShapeIndex])
        {
            Vfx.SetMesh("Emitter Mesh", meshArray[emitterShapeIndex]);
        }

        if(Vfx.HasBool("Emit From Inside") == true)
        {
            Vfx.SetBool("Emit From Inside", emitFromInside);
        }

        if (Vfx.HasInt("Emitter Placement Mode") == true)
        {
            int switchPlacementMode = -1;

            switch(emitterPlacementMode)
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

            Vfx.SetInt("Emitter Placement Mode", switchPlacementMode);
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
}
