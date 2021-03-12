using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class VFXParticleCount : MonoBehaviour
{

    private VisualEffect visualEffect;
    public int aliveParticleCount;

    // Start is called before the first frame update
    void OnEnable()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(visualEffect != null)
            aliveParticleCount = visualEffect.aliveParticleCount;
    }
}
