using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[ExecuteInEditMode]
public class VFXController : MonoBehaviour
{
    private VisualEffect[] m_orbsVisualEffect;

    [Header("Turbulence")]
    public float turbIntensity;
    public float turbFrequency;
    [Range(1, 8)]
    public int turbOctave;
    [Range(0,1f)]
    public float turbroughness;
    public float turbLacunarity;
    public float turbScale;

    [Header("Zalem Gravity")]
    public float zalemGravity;
    public Vector3 gravityAxis;

    [Header("Swirl")]
    public float swirlIntensity;
    public Vector3 swirlAxis;
    public Vector3 swirlOrigin;
    public float swirlRadius;

    [Header("Axial Force")]
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
        m_orbsVisualEffect = GetComponentsInChildren<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisualEffect();
    }

    void UpdateVisualEffect()
    {

        foreach(VisualEffect visualEffect in m_orbsVisualEffect)
        {
            // Turb parameter update
            visualEffect.SetFloat("Turb Intensity", turbIntensity);
            visualEffect.SetFloat("Turb Frequency", turbFrequency);
            visualEffect.SetInt("Octave", turbOctave);
            visualEffect.SetFloat("Roughness", turbroughness);
            visualEffect.SetFloat("Lacunarity", turbLacunarity);
            visualEffect.SetFloat("Turb Scale", turbScale);

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

    public void KillAllParticles()
    {
        foreach (VisualEffect visualEffect in m_orbsVisualEffect)
        {
            visualEffect.Reinit();
        }
    }
}
