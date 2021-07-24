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

    public float intensity;
    public RadialType radialType;
    public float radius;
    public Transform targetObject;
    public bool useVector3;
    public Vector3 targetVector3;


    // Update is called once per frame
    void Update()
    {
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
            if (visualEffect.HasFloat(radial + " Intensity" + suffix))
                visualEffect.SetFloat(radial + " Intensity" + suffix, intensity);

            // Radius
            if (visualEffect.HasFloat(radial + " Radius" + suffix))
                visualEffect.SetFloat(radial + " Radius" + suffix, radius);

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
            if (visualEffect.HasVector3(radial + " Position" + suffix))
                visualEffect.SetVector3(radial + " Position" + suffix, target);
        }
    }
}
