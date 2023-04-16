using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RadialController : ForceController
{
    [Header("Radial")]
    public float radialFrequency;
    public float radialSmoothness;
    public float sphericalFrequency;
    public float sphericalSmoothness;

    protected readonly int s_BufferID = Shader.PropertyToID("Radial Graphics Buffer");

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string radial = "Attractor";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(radial + " Intensity" + m_suffix))
                visualEffect.SetFloat(radial + " Intensity" + m_suffix, intensity);

            // Radius
            if (visualEffect.HasFloat(radial + " Radius" + m_suffix))
                visualEffect.SetFloat(radial + " Radius" + m_suffix, radius);

            // Radial Frequency
            if (visualEffect.HasFloat(radial + " Radial Frequency" + m_suffix))
                visualEffect.SetFloat(radial + " Radial Frequency" + m_suffix, radius);

            // Radial Smoothness
            if (visualEffect.HasFloat(radial + " Radial Smoothness" + m_suffix))
                visualEffect.SetFloat(radial + " Radial Smoothness" + m_suffix, radius);

            // Spherical Frequency
            if (visualEffect.HasFloat(radial + " Spherical Frequency" + m_suffix))
                visualEffect.SetFloat(radial + " Spherical Frequency" + m_suffix, radius);

            // Spherical Smoothness
            if (visualEffect.HasFloat(radial + " Spherical Smoothness" + m_suffix))
                visualEffect.SetFloat(radial + " Spherical Smoothness" + m_suffix, radius);

            if (visualEffect.HasGraphicsBuffer(s_BufferID))
                visualEffect.SetGraphicsBuffer(s_BufferID, m_buffer);

        }
    }
}
