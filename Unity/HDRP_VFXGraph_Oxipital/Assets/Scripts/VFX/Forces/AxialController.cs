using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class AxialController : ForceController
{
    protected readonly int s_BufferID = Shader.PropertyToID("Axial Graphics Buffer");

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string axial = "Axial";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(axial + " Intensity" + m_suffix))
                visualEffect.SetFloat(axial + " Intensity" + m_suffix, intensity);

            // Radius
            if (visualEffect.HasFloat(axial + " Radius" + m_suffix))
                visualEffect.SetFloat(axial + " Radius" + m_suffix, radius);

            // Axis
            if (visualEffect.HasVector3(axial + " Axis" + m_suffix))
                visualEffect.SetVector3(axial + " Axis" + m_suffix, axis);

            // Buffer
            if (visualEffect.HasGraphicsBuffer(s_BufferID))
                visualEffect.SetGraphicsBuffer(s_BufferID, m_buffer);
        }
    }
}
