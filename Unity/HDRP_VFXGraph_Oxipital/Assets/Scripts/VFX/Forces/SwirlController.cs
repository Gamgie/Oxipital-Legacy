using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SwirlController : ForceController
{
    protected readonly int s_BufferID = Shader.PropertyToID("Swirl Graphics Buffer");

    [Header("Swirl")]
    public bool clockwise;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string swirl = "Swirl";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(swirl + " Intensity" + m_suffix))
                visualEffect.SetFloat(swirl + " Intensity" + m_suffix, intensity);

            // Axis
            if (visualEffect.HasVector3(swirl + " Axis" + m_suffix))
                visualEffect.SetVector3(swirl + " Axis" + m_suffix, axis);

            if (visualEffect.HasFloat(swirl + " Radius") == true)
                visualEffect.SetFloat(swirl + " Radius", radius);

            if (visualEffect.HasBool(swirl + " Rotation Clockwise") == true)
                visualEffect.SetBool(swirl + " Rotation Clockwise", clockwise);

            // Buffer size
            if (visualEffect.HasInt(swirl + " Buffer Size" + m_suffix))
                visualEffect.SetInt(swirl + " Buffer Size" + m_suffix, forceCount);

            // Buffer
            if (visualEffect.HasGraphicsBuffer(s_BufferID))
                visualEffect.SetGraphicsBuffer(s_BufferID, m_buffer);
        }
    }
}
