using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class GravityController : ForceController
{
    protected readonly int s_BufferID = Shader.PropertyToID("Gravity Graphics Buffer");

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string gravity = "Gravity";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(gravity + " Intensity" + m_suffix))
                visualEffect.SetFloat(gravity + " Intensity" + m_suffix, intensity);

            // Axis
            if (visualEffect.HasVector3(gravity + " Axis" + m_suffix))
                visualEffect.SetVector3(gravity + " Axis" + m_suffix, axis);

            // Radius
            if (visualEffect.HasFloat(gravity + " Radius" + m_suffix))
                visualEffect.SetFloat(gravity + " Radius" + m_suffix, radius);

            // Buffer
            if (visualEffect.HasGraphicsBuffer(s_BufferID))
                visualEffect.SetGraphicsBuffer(s_BufferID, m_buffer);
        }
    }
}
