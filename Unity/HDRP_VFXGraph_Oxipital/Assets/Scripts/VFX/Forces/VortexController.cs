using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VortexController : ForceController
{
    [Header("Vortex")]
    [Range(0, 1)]
    public float innerRadius = 0.5f;
    public bool clockwise = true;
    [Range(0, 1)]
    public float orthoradialIntensity = 1f;
    [Range(0, 1)]
    public float cylindricIntensity = 1f;

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

            // Orthoradial Intensity
            if (visualEffect.HasFloat(vortex + " Orthoradial Intensity" + m_suffix))
                visualEffect.SetFloat(vortex + " Orthoradial Intensity" + m_suffix, orthoradialIntensity);

            // Cylindric Intensity
            if (visualEffect.HasFloat(vortex + " Cylindric Intensity" + m_suffix))
                visualEffect.SetFloat(vortex + " Cylindric Intensity" + m_suffix, cylindricIntensity);

            // Radius
            if (visualEffect.HasFloat(vortex + " Radius" + m_suffix))
                visualEffect.SetFloat(vortex + " Radius" + m_suffix, radius);

            // Clockwise
            if (visualEffect.HasBool(vortex + " Clockwise" + m_suffix))
                visualEffect.SetBool(vortex + " Clockwise" + m_suffix, clockwise);

            // Axis
            if (visualEffect.HasVector3(vortex + " Axis" + m_suffix))
                visualEffect.SetVector3(vortex + " Axis" + m_suffix, axis);

            // Inner Radius
            if (visualEffect.HasFloat(vortex + " Inner Radius" + m_suffix))
                visualEffect.SetFloat(vortex + " Inner Radius" + m_suffix, innerRadius);

            // Buffer
            if (visualEffect.HasGraphicsBuffer(s_BufferID))
                visualEffect.SetGraphicsBuffer(s_BufferID, m_buffer);
        }
    }
}
