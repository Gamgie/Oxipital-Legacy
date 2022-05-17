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
            if (visualEffect.HasFloat("Turb Intensity") == true)
                visualEffect.SetFloat("Turb Intensity", turbIntensity);

            if (visualEffect.HasFloat("Turb Frequency") == true)
                visualEffect.SetFloat("Turb Frequency", turbFrequency);

            if (visualEffect.HasInt("Octave") == true)
                visualEffect.SetInt("Octave", turbOctave);

            if (visualEffect.HasFloat("Roughness") == true)
                visualEffect.SetFloat("Roughness", turbroughness);

            if (visualEffect.HasFloat("Lacunarity") == true)
                visualEffect.SetFloat("Lacunarity", turbLacunarity);

            if (visualEffect.HasFloat("Turb Scale") == true)
                visualEffect.SetFloat("Turb Scale", turbScale);

            // Gravity parameter update
            if (visualEffect.HasFloat("Gravity") == true)
                visualEffect.SetFloat("Gravity", zalemGravity);

            if(visualEffect.HasVector3("Gravity Axis"))
                visualEffect.SetVector3("Gravity Axis", gravityAxis);


            // Swirl parameter update
            if (visualEffect.HasFloat("Swirl Intensity") == true)
                visualEffect.SetFloat("Swirl Intensity", swirlIntensity);

            if (visualEffect.HasVector3("Swirl Axis"))
                visualEffect.SetVector3("Swirl Axis", swirlAxis);

            if (visualEffect.HasVector3("Swirl Origin"))
                visualEffect.SetVector3("Swirl Origin", swirlOrigin);

            if (visualEffect.HasFloat("Swirl Radius") == true)
                visualEffect.SetFloat("Swirl Radius", swirlRadius);


            // Axial parameter update
            if (visualEffect.HasFloat("Axial Intensity") == true)
                visualEffect.SetFloat("Axial Intensity", axialIntensity);

            if (visualEffect.HasVector3("Axial Axis"))
                visualEffect.SetVector3("Axial Axis", axialAxis);

            // Orbita parameter update
            if (visualEffect.HasFloat("Orbita Intensity") == true)
                visualEffect.SetFloat("Orbita Intensity", orbitaIntensity);

            if (visualEffect.HasVector3("Orbita Axis"))
                visualEffect.SetVector3("Orbita Axis", orbitaAxis);

            if (visualEffect.HasVector3("Orbita Origin"))
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
