using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ConformToSphereController : ForceController
{
    public Transform sphereCenter;
    public float attractionSpeed = 5;
    public float attractionForce = 20;
    public float stickDistance = 0.1f;
    public float stickForce = 50;

    /*[System.Serializable]
    public class VFXFloatElement
    {
        public String name;
        public float value;
    }

    public List<VFXFloatElement> floatList;
    public List<Vector3> vector3List;*/





    // Update is called once per frame
    protected override void Update()
    {
        if (m_vfxs == null)
            return;

        foreach (VisualEffect visualEffect in m_vfxs)
        {
            if (visualEffect.HasVector3("CTS Center" + m_suffix))
                visualEffect.SetVector3("CTS Center" + m_suffix, sphereCenter.position);

            if (visualEffect.HasFloat("CTS Radius" + m_suffix))
                visualEffect.SetFloat("CTS Radius" + m_suffix, radius);

            if (visualEffect.HasFloat("CTS Attraction Speed" + m_suffix))
                visualEffect.SetFloat("CTS Attraction Speed" + m_suffix, attractionSpeed);

            if (visualEffect.HasFloat("CTS Attraction Force" + m_suffix))
                visualEffect.SetFloat("CTS Attraction Force" + m_suffix, attractionForce);

            if (visualEffect.HasFloat("CTS Stick Distance" + m_suffix))
                visualEffect.SetFloat("CTS Stick Distance" + m_suffix, stickDistance);

            if (visualEffect.HasFloat("CTS Stick Force" + m_suffix))
                visualEffect.SetFloat("CTS Stick Force" + m_suffix, stickForce);

            /*VFXFloatElement fElem = floatList[0];
            if (visualEffect.HasFloat(fElem.name))
                visualEffect.SetFloat(fElem.name, fElem.value);*/
        }
    }
}
