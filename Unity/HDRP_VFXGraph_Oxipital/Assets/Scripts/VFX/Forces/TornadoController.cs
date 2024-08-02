using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class TornadoController : ForceController
{
    [Header("Tornado")]
    public bool clockwise;
    public bool bothHemisphere;
    [Header("Vertical Force")]
    [Range(0,1)]
    public float verticalIntensity;
    [Header("Orthogonal Force")]
    [Range(0, 1)]
    public float orthoIntensity;
    public bool orthoSquaredLength;
    [Range(0, 1)]
    public float orthoInnerRadius;
    [Header("Axial Force")]
    [Range(0, 1)]
    public float axialIntensity;
    [Header("Turbulence Force")]
    [Range(0, 1)]
    public float turbulenceIntensity;
    [Range(0, 10)]
    public float turbulenceFrequency;
    [Range(0, 1)]
    public float turbulenceEvolution;

    private void OnEnable()
	{
        key = "Tornado";
    }

	// Update is called once per frame
	protected override void Update()
    {
        base.Update();

        foreach (VisualEffect visualEffect in _vfxs)
        {
            // Clockwise
            if (visualEffect.HasBool(key + " Clockwise" + _suffix))
                visualEffect.SetBool(key + " Clockwise" + _suffix, clockwise);

            // Both Hemisphere
            if (visualEffect.HasBool(key + " Both Hemisphere" + _suffix))
                visualEffect.SetBool(key + " Both Hemisphere" + _suffix, bothHemisphere);

            // Vertical Intensity
            if (visualEffect.HasFloat(key + " Vertical Intensity" + _suffix))
                visualEffect.SetFloat(key + " Vertical Intensity" + _suffix, verticalIntensity);

            // Ortho Intensity
            if (visualEffect.HasFloat(key + " Ortho Intensity" + _suffix))
                visualEffect.SetFloat(key + " Ortho Intensity" + _suffix, orthoIntensity);

            // Ortho Squared Length
            if (visualEffect.HasBool(key + " Ortho Squared Length" + _suffix))
                visualEffect.SetBool(key + " Ortho Squared Length" + _suffix, orthoSquaredLength);

            // Ortho Inner Radius
            if (visualEffect.HasFloat(key + " Ortho Inner Radius" + _suffix))
                visualEffect.SetFloat(key + " Ortho Inner Radius" + _suffix, orthoInnerRadius);

            // Axial Intensity
            if (visualEffect.HasFloat(key + " Axial Intensity" + _suffix))
                visualEffect.SetFloat(key + " Axial Intensity" + _suffix, axialIntensity);

            // Turbulence Intensity
            if (visualEffect.HasFloat(key + " Turbulence Intensity" + _suffix))
                visualEffect.SetFloat(key + " Turbulence Intensity" + _suffix, turbulenceIntensity);

            // Turbulence Frequency
            if (visualEffect.HasFloat(key + " Turbulence Frequency" + _suffix))
                visualEffect.SetFloat(key + " Turbulence Frequency" + _suffix, turbulenceFrequency);

            // Turbulence Evolution
            if (visualEffect.HasFloat(key + " Turbulence Evolution" + _suffix))
                visualEffect.SetFloat(key + " Turbulence Evolution" + _suffix, turbulenceEvolution);
        }
    }

    public override ForceControllerData StoreData()
	{
        ForceControllerData data = new ForceControllerData();
        data = base.StoreBaseData();

        PlayerPrefs.SetInt(key + " Clockwise " + forceID, Convert.ToInt32(clockwise));
        PlayerPrefs.SetInt(key + " Both Hemisphere " + forceID, Convert.ToInt32(bothHemisphere));
        PlayerPrefs.SetFloat(key + " Vertical Intensity " + forceID, verticalIntensity);
        PlayerPrefs.SetFloat(key + " Ortho Intensity " + forceID, orthoIntensity);
        PlayerPrefs.SetInt(key + " Ortho Squared Length " + forceID, Convert.ToInt32(orthoSquaredLength));
        PlayerPrefs.SetFloat(key + " Ortho Inner Radius " + forceID, orthoInnerRadius);
        PlayerPrefs.SetFloat(key + " Axial Intensity " + forceID, axialIntensity);
        PlayerPrefs.SetFloat(key + " Turbulence Intensity " + forceID, turbulenceIntensity);
        PlayerPrefs.SetFloat(key + " Turbulence Frequency " + forceID, turbulenceFrequency);
        PlayerPrefs.SetFloat(key + " Turbulence Evolution " + forceID, turbulenceEvolution);

        return data;
    }

    public override void LoadData(ForceControllerData data)
	{
        base.LoadBaseData(data);


        clockwise = Convert.ToBoolean(PlayerPrefs.GetInt(key + " Clockwise " + forceID, 1));
        bothHemisphere = Convert.ToBoolean(PlayerPrefs.GetInt(key + " Both Hemisphere " + forceID, 1));
        verticalIntensity = PlayerPrefs.GetFloat(key + " Vertical Intensity " + forceID, 0.5f);
        orthoIntensity = PlayerPrefs.GetFloat(key + " Ortho Intensity " + forceID, 0.5f);
        orthoSquaredLength = Convert.ToBoolean(PlayerPrefs.GetInt(key + " Ortho Squared Length " + forceID, 1));
        orthoInnerRadius = PlayerPrefs.GetFloat(key + " Ortho Inner Radius " + forceID, 0.5f);
        axialIntensity = PlayerPrefs.GetFloat(key + " Axial Intensity " + forceID, 0.5f);
        turbulenceIntensity = PlayerPrefs.GetFloat(key + " Turbulence Intensity " + forceID, 0.5f);
        turbulenceFrequency = PlayerPrefs.GetFloat(key + " Turbulence Frequency " + forceID, 0.5f);
        turbulenceEvolution = PlayerPrefs.GetFloat(key + " Turbulence Evolution " + forceID, 0.5f);
}
}