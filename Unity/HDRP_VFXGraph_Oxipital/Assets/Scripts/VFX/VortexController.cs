using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VortexController : ForceController
{
    public float intensity;
    [Range(0, 1)]
    public float intensityRandom;
    public float radius;

    public Vector3 axis;


    public Transform targetObject;
    public bool useVector3;
    public Vector3 targetVector3;

    // Update is called once per frame
    void Update()
    {
        string vortex = "Vortex";

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            // Intensity
            if (visualEffect.HasFloat(vortex + " Intensity" + suffix))
                visualEffect.SetFloat(vortex + " Intensity" + suffix, intensity);

            // Intensity Random
            if (visualEffect.HasFloat(vortex + " Intensity Random" + suffix))
                visualEffect.SetFloat(vortex + " Intensity Random" + suffix, intensityRandom);

            // Radius
            if (visualEffect.HasFloat(vortex + " Radius" + suffix))
                visualEffect.SetFloat(vortex + " Radius" + suffix, radius);

            // Axis
            if (visualEffect.HasVector3(vortex + " Axis" + suffix))
                visualEffect.SetVector3(vortex + " Axis" + suffix, axis);

            // Target
            Vector3 target = Vector3.zero;
            if (useVector3)
            {
                target = targetVector3;
            }
            else
            {
                if (targetObject != null)
                    target = targetObject.position;
            }
            if (visualEffect.HasVector3(vortex + " Position" + suffix))
                visualEffect.SetVector3(vortex + " Position" + suffix, target);

        }
    }
}
