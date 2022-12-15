using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXParticleCount : MonoBehaviour
{
    public int aliveParticleCount;

    // Update is called once per frame
    void Update()
    {
        VisualEffect[] vfxList = GetComponentsInChildren<VisualEffect>();

        int totalCount = 0;
        if (vfxList.Length != 0)
        {
            foreach(VisualEffect vfx in vfxList)
            {
                if(vfx != null)
                    totalCount += vfx.aliveParticleCount;
            }
        }
        aliveParticleCount = totalCount;
    }
}
