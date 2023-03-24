using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class AxialController : ForceController
{
    public float intensity;
    public Vector3 axis;
    public float radius;


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string axial = "Axial";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(axial + " Intensity" + suffix))
                visualEffect.SetFloat(axial + " Intensity" + suffix, intensity);

            // Axis
            if (visualEffect.HasVector3(axial + " Axis" + suffix))
                visualEffect.SetVector3(axial + " Axis" + suffix, axis);
        }
    }
}
