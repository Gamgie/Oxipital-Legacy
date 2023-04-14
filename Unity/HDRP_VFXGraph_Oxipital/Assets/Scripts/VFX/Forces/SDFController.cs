using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SDFController : ForceController
{
    public Texture3D texture;
    public float attractionSpeed = 5;
    public float attractionForce = 20;
    public float stickDistance = 0.1f;
    public float stickForce = 50;
    public Texture3D[] textureAvailable;


    // Update is called once per frame
    protected override void Update()
    {
        string sdf = "SDF";

        foreach(VisualEffect vfx in m_vfxs)
        {
            // Texture
            if (vfx.HasTexture(sdf + " Texture" + m_suffix))
                vfx.SetTexture(sdf + " Texture" + m_suffix, texture);

            // Attraction Speed
            if (vfx.HasFloat(sdf + " Attraction Speed" + m_suffix))
                vfx.SetFloat(sdf + " Attraction Speed" + m_suffix, attractionSpeed);

            // Attraction Force
            if (vfx.HasFloat(sdf + " Attraction Force" + m_suffix))
                vfx.SetFloat(sdf + " Attraction Force" + m_suffix, attractionForce);

            // Stick Force
            if (vfx.HasFloat(sdf + " Stick Force" + m_suffix))
                vfx.SetFloat(sdf + " Stick Force" + m_suffix, stickForce);

            // Stick Distance
            if (vfx.HasFloat(sdf + " Stick Distance" + m_suffix))
                vfx.SetFloat(sdf + " Stick Distance" + m_suffix, stickDistance);
        }
    
    }
}
