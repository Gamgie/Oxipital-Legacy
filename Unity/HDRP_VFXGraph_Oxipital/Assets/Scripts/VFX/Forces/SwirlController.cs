using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SwirlController : ForceController
{
    protected readonly int s_BufferID = Shader.PropertyToID("Swirl Graphics Buffer");

    [Header("Swirl")]
    public bool clockwise;
    [Range(0,1)]
    public float centralVertical;

    private void OnEnable()
    {
        key = "Swirl";
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        foreach (VisualEffect visualEffect in _vfxs)
        {
            // Clockwise
            if (visualEffect.HasBool(key + " Rotation Clockwise") == true)
                visualEffect.SetBool(key + " Rotation Clockwise", clockwise);

            // Central Vertical
            if (visualEffect.HasFloat(key + " Central Vertical") == true)
                visualEffect.SetFloat(key + " Central Vertical", centralVertical);
        }
    }

    public override ForceControllerData StoreData()
    {
        ForceControllerData data = new ForceControllerData();
        base.StoreBaseData(data);

        PlayerPrefs.SetInt(key + " clockwise " + forceID, Convert.ToInt32(clockwise));
        PlayerPrefs.SetFloat(key + " centralVertical " + forceID, centralVertical);

        return data;
    }

    public override void LoadData(ForceControllerData data)
    {
        base.LoadBaseData(data);

        //Vortex Parameters
        centralVertical = PlayerPrefs.GetFloat(key + " centralVertical " + forceID, 0f);
        clockwise = Convert.ToBoolean(PlayerPrefs.GetInt(key + " clockwise " + forceID, 1));
    }
}
