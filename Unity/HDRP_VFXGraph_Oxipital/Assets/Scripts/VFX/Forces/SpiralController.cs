using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpiralController : ForceController
{
    [Header("Spiral")]
    public float frequency;
    public float alpha = 0.6f;
    public float verticalForce = 1f;

	private void OnEnable()
	{
        key = "Spiral";
	}

	// Update is called once per frame
	protected override void Update()
    {
        base.Update();

        foreach (VisualEffect visualEffect in _vfxs)
        {
            // Frequency
            if (visualEffect.HasFloat(key + " Frequency" + _suffix))
                visualEffect.SetFloat(key + " Frequency" + _suffix, frequency);

            // Alpha
            if (visualEffect.HasFloat(key + " Alpha" + _suffix))
                visualEffect.SetFloat(key + " Alpha" + _suffix, alpha);

            // Vertical Force
            if (visualEffect.HasFloat(key + " Vertical Force" + _suffix))
                visualEffect.SetFloat(key + " Vertical Force" + _suffix, verticalForce);
        }
    }

    public override ForceControllerData StoreData()
    {
        ForceControllerData data = new ForceControllerData();
        data = base.StoreBaseData();

        PlayerPrefs.SetFloat(key + " frequency " + forceID, frequency);
        PlayerPrefs.SetFloat(key + " alpha " + forceID, alpha);
        PlayerPrefs.SetFloat(key + " vertical Force " + forceID,  verticalForce);

        return data;
    }

    public override void LoadData(ForceControllerData data)
    {
        base.LoadBaseData(data);

        //Spiral Parameters
        frequency = PlayerPrefs.GetFloat(key + " frequency " + forceID, 1f);
        alpha = PlayerPrefs.GetFloat(key + " alpha " + forceID, 1f);
        verticalForce = PlayerPrefs.GetFloat(key + " vertical Force " + forceID, 1f);
    }

}
