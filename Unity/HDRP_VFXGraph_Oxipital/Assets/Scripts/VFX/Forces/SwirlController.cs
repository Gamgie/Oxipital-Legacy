using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class SwirlController : ForceController
{
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
            
        }
    }
}
