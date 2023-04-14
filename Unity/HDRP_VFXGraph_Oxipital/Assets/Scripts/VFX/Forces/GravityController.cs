using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class GravityController : ForceController
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string gravity = "Gravity";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(gravity + m_suffix))
                visualEffect.SetFloat(gravity + m_suffix, intensity);

            // Axis
            if (visualEffect.HasVector3(gravity + " Axis" + m_suffix))
                visualEffect.SetVector3(gravity + " Axis" + m_suffix, axis);
        }
    }
}
