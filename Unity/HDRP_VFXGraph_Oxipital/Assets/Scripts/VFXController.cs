using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[ExecuteInEditMode]
public class VFXController : MonoBehaviour
{
    private VisualEffect[] orbsVisualEffect;

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
    public float turbScale;
    public float turbXPosition;

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

    [Header("Swirl")]
    public float swirlIntensity;
    public Vector3 swirlAxis;
    public Vector3 swirlOrigin;
    public float swirlRadius;

    [Header("Axxial Force")]
    public float axialIntensity;
    public Vector3 axialAxis;
    public float axialIntensityVariance;

    [Header("Orbita Force")]
    public float orbitaIntensity;
    public Vector3 orbitaAxis;
    public Vector3 orbitaOrigin;

    // Start is called before the first frame update
    void OnEnable()
    {
        orbsVisualEffect = GetComponentsInChildren<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisualEffect();
    }

    void UpdateVisualEffect()
    {

        foreach(VisualEffect visualEffect in orbsVisualEffect)
        {
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
            visualEffect.SetFloat("Turb Scale", turbScale);
            visualEffect.SetFloat("Turb X Position", turbXPosition);

            // Vortex 1 parameter update
            visualEffect.SetFloat("Vortex Force", vortexIntensity);
            visualEffect.SetFloat("Vortex Force Random", vortexIntensityRandom);
            visualEffect.SetFloat("Vortex Radius", vortexRadius);
            visualEffect.SetVector3("Vortex Axis", vortexAxis);
            visualEffect.SetVector3("Vortex Target Position", vortexTargetPosition);

            // Gravity parameter update
            visualEffect.SetFloat("Gravity", zalemGravity);
            visualEffect.SetVector3("Gravity Axis", gravityAxis);

            // Swirl parameter update
            visualEffect.SetFloat("Swirl Intensity", swirlIntensity);
            visualEffect.SetVector3("Swirl Axis", swirlAxis);
            visualEffect.SetVector3("Swirl Origin", swirlOrigin);
            visualEffect.SetFloat("Swirl Radius", swirlRadius);

            // Axial parameter update
            visualEffect.SetFloat("Axial Intensity", axialIntensity);
            visualEffect.SetVector3("Axial Axis", axialAxis);
            visualEffect.SetFloat("Axial Intensity Variance" +
                "", axialIntensityVariance);

            // Orbita parameter update
            visualEffect.SetFloat("Orbita Intensity", orbitaIntensity);
            visualEffect.SetVector3("Orbita Axis", orbitaAxis);
            visualEffect.SetVector3("Orbita Origin", orbitaOrigin);
        }
    }
}
