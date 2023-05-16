using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VortexController : ForceController
{
    [Header("Vortex")]
    [Range(0, 1)]
    public float innerRadius = 0.5f;
    public bool clockwise = true;
    [Range(0, 1)]
    public float orthoradialIntensity = 1f;
    [Range(0, 1)]
    public float cylindricIntensity = 1f;

	private void OnEnable()
	{
        key = "Vortex";
    }

	// Update is called once per frame
	protected override void Update()
    {
        base.Update();

        foreach (VisualEffect visualEffect in _vfxs)
        {
            // Orthoradial Intensity
            if (visualEffect.HasFloat(key + " Orthoradial Intensity" + _suffix))
                visualEffect.SetFloat(key + " Orthoradial Intensity" + _suffix, orthoradialIntensity);

            // Cylindric Intensity
            if (visualEffect.HasFloat(key + " Cylindric Intensity" + _suffix))
                visualEffect.SetFloat(key + " Cylindric Intensity" + _suffix, cylindricIntensity);

            // Clockwise
            if (visualEffect.HasBool(key + " Clockwise" + _suffix))
                visualEffect.SetBool(key + " Clockwise" + _suffix, clockwise);

            // Inner Radius
            if (visualEffect.HasFloat(key + " Inner Radius" + _suffix))
                visualEffect.SetFloat(key + " Inner Radius" + _suffix, innerRadius);
        }
    }

    public override ForceControllerData StoreData()
	{
        ForceControllerData data = new ForceControllerData();
        data = base.StoreBaseData();

        // Vortex Param
        PlayerPrefs.SetFloat(key + " innerRadius " + forceID, innerRadius);
        PlayerPrefs.SetInt(key + " clockwise " + forceID, Convert.ToInt32(clockwise));
        PlayerPrefs.SetFloat(key + " orthoradialIntensity " + forceID, orthoradialIntensity);
        PlayerPrefs.SetFloat(key + " cylindricIntensity " + forceID, cylindricIntensity);

        return data;
    }

    public override void LoadData(ForceControllerData data)
	{
        base.LoadBaseData(data);

        //Vortex Parameters
        innerRadius = PlayerPrefs.GetFloat(key + " innerRadius " + forceID, 0.5f);
        clockwise = Convert.ToBoolean(PlayerPrefs.GetInt(key + " clockwise " + forceID, 1));
        orthoradialIntensity = PlayerPrefs.GetFloat(key + " orthoradialIntensity " + forceID, 1f);
        cylindricIntensity = PlayerPrefs.GetFloat(key + " cylindricIntensity " + forceID, 1f);
    }
}