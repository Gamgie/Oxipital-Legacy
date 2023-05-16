using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpiralController : ForceController
{
    [Header("Spiral")]
    public float frequency;

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
        }
    }

    public override ForceControllerData StoreData()
    {
        ForceControllerData data = new ForceControllerData();
        data = base.StoreBaseData();

        PlayerPrefs.SetFloat(key + " frequency " + forceID, frequency);

        return data;
    }

    public override void LoadData(ForceControllerData data)
    {
        base.LoadBaseData(data);

        //Vortex Parameters
        frequency = PlayerPrefs.GetFloat(key + " frequency " + forceID, 1f);
    }

}
