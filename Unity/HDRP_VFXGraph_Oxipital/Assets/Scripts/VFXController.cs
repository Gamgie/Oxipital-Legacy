using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class VFXController : MonoBehaviour
{
    private VisualEffect visualEffect;

    public int id;

    [Header("PS Parameters")]

    public int aliveParticleCount;

    [Range(0,20000)]
    public float rate;

    [Range(0, 200)]
    public float life;
    public Color color;
    [Range(0,1)]
    public float alpha;
    [Range(0,5)]
    public float size;
    //[Range(0, 10)]
    public float drag;

    [Header("Attractor")]
    public Vector3 attractorTargetPosition;
    public float attractorIntensity;
    public float attractorRadius;

    [Header("Repulsor")]
    public Vector3 repulsorTargetPosition;
    public float repulsorIntensity;
    public float repulsorRadius;

    [Header("Turbulence")]
    public float turbIntensity;
    public float turbFrequency;
    [Range(1, 8)]
    public int turbOctave;
    [Range(0,1f)]
    public float turbroughness;
    public float turbLacunarity;

    [Header("Vortex 1")]
    public float vortexIntensity;
    [Range(0,1)]
    public float vortexIntensityRandom;
    public float vortexRadius;
    public Vector3 vortexAxis;
    public Vector3 vortexTargetPosition;

    [Header("Zalem Gravity")]
    public float zalemGravity;
    public Vector3 gravityAxis;

    // Start is called before the first frame update
    void OnEnable()
    {
        visualEffect = GetComponent<VisualEffect>();
        UpdateVisualEffect();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisualEffect();

        aliveParticleCount = visualEffect.aliveParticleCount;
    }

    void UpdateVisualEffect()
    {
        // Ps parameters update
        visualEffect.SetFloat("Rate", rate);
        visualEffect.SetFloat("LifeTime", life);
        visualEffect.SetVector4("Color", new Vector3(color.r, color.g, color.b));
        visualEffect.SetFloat("Alpha", alpha);
        visualEffect.SetFloat("Size", size);
        visualEffect.SetFloat("Linear Drag", drag);

        // Attractor update
        visualEffect.SetVector3("Attractor Target Position", attractorTargetPosition);
        visualEffect.SetFloat("Attractor Intensity", attractorIntensity);
        visualEffect.SetFloat("Attractor Radius", attractorRadius);

        // Repulsor update
        visualEffect.SetVector3("Repulsor Target Position", attractorTargetPosition);
        visualEffect.SetFloat("Repulsor Intensity", repulsorIntensity);
        visualEffect.SetFloat("Repulsor Radius", repulsorRadius);

        // Turb parameter update
        visualEffect.SetFloat("Turb Intensity", turbIntensity);
        visualEffect.SetFloat("Turb Frequency", turbFrequency);
        visualEffect.SetInt("Octave", turbOctave);
        visualEffect.SetFloat("Roughness", turbroughness);
        visualEffect.SetFloat("Lacunarity", turbLacunarity);

        // Vortex 1 parameter update
        visualEffect.SetFloat("Vortex Force", vortexIntensity);
        visualEffect.SetFloat("Vortex Force Random", vortexIntensityRandom);
        visualEffect.SetFloat("Vortex Radius", vortexRadius);
        visualEffect.SetVector3("Vortex Axis", vortexAxis);
        visualEffect.SetVector3("Vortex Target Position", vortexTargetPosition);

        // Gravity parameter update
        visualEffect.SetFloat("Gravity", zalemGravity);
        visualEffect.SetVector3("Gravity Axis", gravityAxis);
    }

}
