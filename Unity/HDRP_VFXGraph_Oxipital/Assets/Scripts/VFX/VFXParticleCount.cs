using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class VFXParticleCount : MonoBehaviour
{

    public VisualEffect[] visualEffect;
    public int aliveParticleCount;

    // Update is called once per frame
    void Update()
    {
        int totalCount = 0;
        if (visualEffect.Length != 0)
        {
            foreach(VisualEffect vfx in visualEffect)
            {
                if(vfx != null)
                    totalCount += vfx.aliveParticleCount;
            }
        }
            aliveParticleCount = totalCount;
    }
}
