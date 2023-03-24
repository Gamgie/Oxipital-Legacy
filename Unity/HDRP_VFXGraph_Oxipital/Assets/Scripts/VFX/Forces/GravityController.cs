using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class GravityController : ForceController
{
    public float intensity;
    public Vector3 axis;
    public float radius;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        string gravity = "Gravity";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(gravity + suffix))
                visualEffect.SetFloat(gravity + suffix, intensity);

            // Axis
            if (visualEffect.HasVector3(gravity + " Axis" + suffix))
                visualEffect.SetVector3(gravity + " Axis" + suffix, axis);
        }
    }
}
