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

    protected readonly int s_BufferID = Shader.PropertyToID("Perlin Graphics Buffer");

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string perlin = "Perlin";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(perlin + " Intensity" + m_suffix))
                visualEffect.SetFloat(perlin + " Intensity" + m_suffix, intensity);

            // Evolution Speed
            if (visualEffect.HasFloat(perlin + " Evolution Speed" + m_suffix))
                visualEffect.SetFloat(perlin + " Evolution Speed" + m_suffix, evolutionSpeed);

            // Radius
            if (visualEffect.HasFloat(perlin + " Radius" + m_suffix))
                visualEffect.SetFloat(perlin + " Radius" + m_suffix, radius);

            // Frequency
            if (visualEffect.HasFloat(perlin + " Frequency") == true)
                visualEffect.SetFloat(perlin + " Frequency", frequency);

            // Octaves
            if (visualEffect.HasInt(perlin + " Octaves") == true)
                visualEffect.SetInt(perlin + " Octaves", octaves);

            // Roughness
            if (visualEffect.HasFloat(perlin + " Roughness") == true)
                visualEffect.SetFloat(perlin + " Roughness", roughness);

            // Lacunarity
            if (visualEffect.HasFloat(perlin + " Lacunarity") == true)
                visualEffect.SetFloat(perlin + " Lacunarity", lacunarity);

            // Min Range
            if (visualEffect.HasFloat(perlin + " Min Range") == true)
                visualEffect.SetFloat(perlin + " Min Range", minRange);

            // Max Range
            if (visualEffect.HasFloat(perlin + " Max Range") == true)
                visualEffect.SetFloat(perlin + " Max Range", maxRange);

            // Noise Type
            if (visualEffect.HasInt(perlin + " Noise Type") == true)
                visualEffect.SetInt(perlin + " Noise Type", (int) noiseType);

            // Buffer
            if (visualEffect.HasGraphicsBuffer(s_BufferID))
                visualEffect.SetGraphicsBuffer(s_BufferID, m_buffer);
        }
    }
}
