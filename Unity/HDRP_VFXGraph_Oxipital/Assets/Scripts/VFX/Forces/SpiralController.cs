using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpiralController : ForceController
{
    [Header("Spiral")]
    public float frequency;

    protected readonly int s_BufferID = Shader.PropertyToID("Spiral Graphics Buffer");

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string spiral = "Spiral";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(spiral + " Intensity" + m_suffix))
                visualEffect.SetFloat(spiral + " Intensity" + m_suffix, intensity);

            // Radius
            if (visualEffect.HasFloat(spiral + " Radius" + m_suffix))
                visualEffect.SetFloat(spiral + " Radius" + m_suffix, radius);

            // Axis
            if (visualEffect.HasVector3(spiral + " Axis" + m_suffix))
                visualEffect.SetVector3(spiral + " Axis" + m_suffix, axis);

            // Frequency
            if (visualEffect.HasFloat(spiral + " Frequency" + m_suffix))
                visualEffect.SetFloat(spiral + " Frequency" + m_suffix, frequency);

            // Buffer
            if (visualEffect.HasGraphicsBuffer(s_BufferID))
                visualEffect.SetGraphicsBuffer(s_BufferID, m_buffer);
        }
    }

    
}
