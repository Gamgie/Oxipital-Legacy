using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PerlinController : ForceController
{
    public enum PerlinNoiseType { Perlin, Cellular }

    [Header("Perlin")]
    public PerlinNoiseType noiseType = PerlinNoiseType.Perlin;
    [Range(0, 3f)]
    public float evolutionSpeed = 0;
    [Range(0,3)]
    public float frequency = 1;
    [Range(1,8)]
    public int octaves = 1;
    [Range(0,1)]
    public float roughness = 0.5f;
    [Range(0,5)]
    public float lacunarity = 2;
    [Range(-1,1)]
    public float minRange = -1;
    [Range(-1, 1)]
    public float maxRange = 1;

    private void OnEnable()
    {
        key = "Perlin";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        foreach (VisualEffect visualEffect in _vfxs)
        {
            // Evolution Speed
            if (visualEffect.HasFloat(key + " Evolution Speed" + _suffix))
                visualEffect.SetFloat(key + " Evolution Speed" + _suffix, evolutionSpeed);

            // Frequency
            if (visualEffect.HasFloat(key + " Frequency") == true)
                visualEffect.SetFloat(key + " Frequency", frequency);

            // Octaves
            if (visualEffect.HasInt(key + " Octaves") == true)
                visualEffect.SetInt(key + " Octaves", octaves);

            // Roughness
            if (visualEffect.HasFloat(key + " Roughness") == true)
                visualEffect.SetFloat(key + " Roughness", roughness);

            // Lacunarity
            if (visualEffect.HasFloat(key + " Lacunarity") == true)
                visualEffect.SetFloat(key + " Lacunarity", lacunarity);

            // Min Range
            if (visualEffect.HasFloat(key + " Min Range") == true)
                visualEffect.SetFloat(key + " Min Range", minRange);

            // Max Range
            if (visualEffect.HasFloat(key + " Max Range") == true)
                visualEffect.SetFloat(key + " Max Range", maxRange);

            // Noise Type
            if (visualEffect.HasInt(key + " Noise Type") == true)
                visualEffect.SetInt(key + " Noise Type", (int) noiseType);
        }
    }

    public override ForceControllerData StoreData()
    {
        ForceControllerData data = new ForceControllerData();
        base.StoreBaseData(data);

        PlayerPrefs.SetInt(key + " noiseType " + forceID, Convert.ToInt32(noiseType));
        PlayerPrefs.SetFloat(key + " frequency " + forceID, frequency);
        PlayerPrefs.SetFloat(key + " roughness " + forceID, roughness);
        PlayerPrefs.SetFloat(key + " lacunarity " + forceID, lacunarity);
        PlayerPrefs.SetFloat(key + " minRange " + forceID, minRange);
        PlayerPrefs.SetFloat(key + " minRange " + forceID, minRange);
        PlayerPrefs.SetInt(key + " octaves " + forceID, octaves);

        return data;
    }

    public override void LoadData(ForceControllerData data)
    {
        base.LoadBaseData(data);

        // Perlin Parameters
        noiseType = PerlinNoiseType.Perlin;
        evolutionSpeed = PlayerPrefs.GetFloat(key + " evolutionSpeed " + forceID, 0f);
        frequency = PlayerPrefs.GetFloat(key + " frequency " + forceID, 1f);
        octaves = PlayerPrefs.GetInt(key + " octaves " + forceID, 1);
        roughness = PlayerPrefs.GetFloat(key + " roughness " + forceID, 0.5f);
        lacunarity = PlayerPrefs.GetFloat(key + " lacunarity " + forceID, 2f);
        minRange = PlayerPrefs.GetFloat(key + " minRange " + forceID, -1f);
        maxRange = PlayerPrefs.GetFloat(key + " maxRange " + forceID, 1f);
    }
}
