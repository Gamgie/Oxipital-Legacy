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
            if (visualEffect.HasFloat(swirl + " Intensity" + suffix))
                visualEffect.SetFloat(swirl + " Intensity" + suffix, intensity);

            // Axis
            if (visualEffect.HasVector3(swirl + " Axis" + suffix))
                visualEffect.SetVector3(swirl + " Axis" + suffix, axis);

            if (visualEffect.HasFloat(swirl + " Radius") == true)
                visualEffect.SetFloat(swirl + " Radius", radius);

            if (visualEffect.HasBool(swirl + " Rotation Clockwise") == true)
                visualEffect.SetBool(swirl + " Rotation Clockwise", clockwise);

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
            if (visualEffect.HasVector3(swirl + " Position" + suffix))
                visualEffect.SetVector3(swirl + " Position" + suffix, target);
        }
    }
}
