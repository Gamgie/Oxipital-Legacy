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
    [Range(0, 1f)]
    public float turbEvolutionSpeed = 1;

    private void OnEnable()
    {
        key = "Turb";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        foreach (VisualEffect visualEffect in _vfxs)
        {
            // Turb parameter update
            if (visualEffect.HasFloat(key + " Frequency") == true)
                visualEffect.SetFloat(key + " Frequency", turbFrequency);

            if (visualEffect.HasInt("Octave") == true)
                visualEffect.SetInt("Octave", turbOctave);

            if (visualEffect.HasFloat("Roughness") == true)
                visualEffect.SetFloat("Roughness", turbroughness);

            if (visualEffect.HasFloat("Lacunarity") == true)
                visualEffect.SetFloat("Lacunarity", turbLacunarity);

            if (visualEffect.HasFloat(key + " Scale") == true)
                visualEffect.SetFloat(key + " Scale", turbScale);

            if (visualEffect.HasFloat(key + " Evolution Speed") == true)
                visualEffect.SetFloat(key + " Evolution Speed", turbEvolutionSpeed);
        }
    }

    public override ForceControllerData StoreData()
    {
        ForceControllerData data = new ForceControllerData();
        data = base.StoreBaseData();

        // Turbulence Parameters
        PlayerPrefs.SetFloat(key + " turbFrequency " + forceID, turbFrequency);
        PlayerPrefs.SetInt(key + " turbOctave " + forceID, turbOctave);
        PlayerPrefs.SetFloat(key + " turbroughness " + forceID, turbroughness);
        PlayerPrefs.SetFloat(key + " turbLacunarity " + forceID, turbLacunarity);
        PlayerPrefs.SetFloat(key + " turbScale " + forceID, turbScale);
        PlayerPrefs.SetFloat(key + " turbEvolutionSpeed " + forceID, turbEvolutionSpeed);

        return data;
    }

    public override void LoadData(ForceControllerData data)
    {
        base.LoadBaseData(data);

        // Turbulence Parameters
        turbFrequency =      PlayerPrefs.GetFloat(key + " turbFrequency " + forceID, 1f);
        turbOctave =         PlayerPrefs.GetInt(key + " turbOctave " + forceID, 1);
        turbroughness =      PlayerPrefs.GetFloat(key + " turbroughness " + forceID, 0.5f);
        turbLacunarity =     PlayerPrefs.GetFloat(key + " turbLacunarity " + forceID, 2f);
        turbScale =          PlayerPrefs.GetFloat(key + " turbScale " + forceID, 1f);
        turbEvolutionSpeed = PlayerPrefs.GetFloat(key + " turbEvolutionSpeed " + forceID, 1f);
    }
}
