using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RadialController : ForceController
{
    public enum RadialType
    {
        ATTRACTOR,
        REPULSOR
    }
    [Header("Radial")]
    public RadialType radialType;


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string radial = " ";

        if(radialType == RadialType.ATTRACTOR)
        {
            radial = "Attractor";
        }
        else
        {
            radial = "Repulsor";
        }

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(radial + " Intensity" + m_suffix))
                visualEffect.SetFloat(radial + " Intensity" + m_suffix, intensity);

            // Radius
            if (visualEffect.HasFloat(radial + " Radius" + m_suffix))
                visualEffect.SetFloat(radial + " Radius" + m_suffix, radius); 
        }
    }
}
