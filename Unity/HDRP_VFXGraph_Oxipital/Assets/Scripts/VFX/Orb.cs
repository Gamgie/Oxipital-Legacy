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
    public bool useCustomEmitterShape;
    public EmitterShape emitterShape;
    public EmitterPlacementMode emitterPlacementMode;
    public GameObject emitterGO;
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
        if (useCustomEmitterShape)
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
        
        if(!useCustomEmitterShape)
        {
            if (Vfx.HasFloat("Emitter Size") == true)
                Vfx.SetFloat("Emitter Size", emitterSize);
        }
    }

    void UpdateEmitter()
    {
        if (emitterGO == null)
            return;

        emitterGO.transform.position = emitterPosition;
        emitterGO.transform.eulerAngles= emitterOrientation;
        emitterGO.transform.localScale= new Vector3(emitterSize, emitterSize, emitterSize);

        // If no emitter mesh in graph then no need to go further
        if (Vfx.HasMesh("Emitter Mesh") == false)
            return;

        // Check if we need to update mesh in graph
        emitterShapeIndex = GetEmitterShapeIndex();
        Mesh actualMesh = Vfx.GetMesh("Emitter Mesh");
        if(actualMesh != meshArray[emitterShapeIndex])
        {
            emitterGO.GetComponent<MeshFilter>().mesh = meshArray[emitterShapeIndex];
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
