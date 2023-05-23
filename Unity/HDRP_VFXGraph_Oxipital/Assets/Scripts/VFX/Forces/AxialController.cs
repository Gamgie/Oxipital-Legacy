using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class AxialController : ForceController
{
    private void OnEnable()
    {
        key = "Axial";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override ForceControllerData StoreData()
    {
        ForceControllerData data = new ForceControllerData();
        data = base.StoreBaseData();

        return data;
    }

    public override void LoadData(ForceControllerData data)
    {
        base.LoadBaseData(data);
    }
}
