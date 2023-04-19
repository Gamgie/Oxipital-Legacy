using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class TurbulenceController : ForceController
{
    [Header("Turbulence")]
    public float turbFrequency = 1;
    [Range(1, 8)]
    public int turbOctave = 1;
    [Range(0, 1f)]
    public float turbroughness = 0.5f;
    [Range(0, 5f)]
    public float turbLacunarity = 2;
    [Range(0, 5f)]
    public float turbScale = 1;
    [Range(0, 5f)]
    public float turbEvolutionSpeed = 1;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string turb = "Turb";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Turb parameter update
            if (visualEffect.HasFloat(turb + " Intensity") == true)
                visualEffect.SetFloat(turb + " Intensity", intensity);

            if (visualEffect.HasFloat(turb + " Frequency") == true)
                visualEffect.SetFloat(turb + " Frequency", turbFrequency);

            if (visualEffect.HasInt("Octave") == true)
                visualEffect.SetInt("Octave", turbOctave);

            if (visualEffect.HasFloat("Roughness") == true)
                visualEffect.SetFloat("Roughness", turbroughness);

            if (visualEffect.HasFloat("Lacunarity") == true)
                visualEffect.SetFloat("Lacunarity", turbLacunarity);

            if (visualEffect.HasFloat(turb + " Scale") == true)
                visualEffect.SetFloat(turb + " Scale", turbScale);

            if (visualEffect.HasFloat(turb + " Evolution Speed") == true)
                visualEffect.SetFloat(turb + " Evolution Speed", turbEvolutionSpeed);
        }
    }
}
