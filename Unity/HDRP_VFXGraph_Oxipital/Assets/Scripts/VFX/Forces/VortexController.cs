using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VortexController : ForceController
{
    [Header("Vortex")]
    [Range(0, 1)]
    public float intensityRandom;

    protected readonly int s_BufferID = Shader.PropertyToID("Vortex Graphics Buffer");

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string vortex = "Vortex";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(vortex + " Intensity" + m_suffix))
                visualEffect.SetFloat(vortex + " Intensity" + m_suffix, intensity);

            // Intensity Random
            if (visualEffect.HasFloat(vortex + " Intensity Random" + m_suffix))
                visualEffect.SetFloat(vortex + " Intensity Random" + m_suffix, intensityRandom);

            // Radius
            if (visualEffect.HasFloat(vortex + " Radius" + m_suffix))
                visualEffect.SetFloat(vortex + " Radius" + m_suffix, radius);

            // Axis
            if (visualEffect.HasVector3(vortex + " Axis" + m_suffix))
                visualEffect.SetVector3(vortex + " Axis" + m_suffix, axis);
            
            // Buffer size
            if (visualEffect.HasInt(vortex + " Buffer Size" + m_suffix))
                visualEffect.SetInt(vortex + " Buffer Size" + m_suffix, forceCount);

            // Buffer
            if (visualEffect.HasGraphicsBuffer(s_BufferID))
                visualEffect.SetGraphicsBuffer(s_BufferID, m_buffer);
        }
    }
}
