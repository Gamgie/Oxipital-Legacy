using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RadialController : ForceController
{
    [Header("Radial")]
    [Range(0, 3)]
    public float radialFrequency;
    [Range(0,1)]
    public float radialSmoothness = 1;
    [Range(0, 3)]
    public float sphericalFrequency;
    [Range(0, 1)]
    public float sphericalSmoothness = 1;

    private void OnEnable()
    {
        key = "Attractor";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        foreach (VisualEffect visualEffect in _vfxs)
        {

            // Radial Frequency
            if (visualEffect.HasFloat(key + " Radial Frequency" + _suffix))
                visualEffect.SetFloat(key + " Radial Frequency" + _suffix, radialFrequency);

            // Radial Smoothness
            if (visualEffect.HasFloat(key + " Radial Smoothness" + _suffix))
                visualEffect.SetFloat(key + " Radial Smoothness" + _suffix, radialSmoothness);

            // Spherical Frequency
            if (visualEffect.HasFloat(key + " Spherical Frequency" + _suffix))
                visualEffect.SetFloat(key + " Spherical Frequency" + _suffix, sphericalFrequency);

            // Spherical Smoothness
            if (visualEffect.HasFloat(key + " Spherical Smoothness" + _suffix))
                visualEffect.SetFloat(key + " Spherical Smoothness" + _suffix, sphericalSmoothness);
        }
    }

    public override ForceControllerData StoreData()
    {
        ForceControllerData data = new ForceControllerData();
        data = base.StoreBaseData();

        PlayerPrefs.SetFloat(key + " radialFrequency " + forceID, radialFrequency);
        PlayerPrefs.SetFloat(key + " radialSmoothness " + forceID, radialSmoothness);
        PlayerPrefs.SetFloat(key + " sphericalFrequency " + forceID, sphericalFrequency);
        PlayerPrefs.SetFloat(key + " sphericalSmoothness " + forceID, sphericalSmoothness);

        return data;
    }

    public override void LoadData(ForceControllerData data)
    {
        base.LoadBaseData(data);

        radialFrequency = PlayerPrefs.GetFloat(key + " radialFrequency " + forceID, 0f);
        radialSmoothness = PlayerPrefs.GetFloat(key + " radialSmoothness " + forceID, 1f);
        sphericalFrequency = PlayerPrefs.GetFloat(key + " sphericalFrequency " + forceID, 0f);
        sphericalSmoothness = PlayerPrefs.GetFloat(key + " sphericalSmoothness " + forceID, 1f);
    }
}
