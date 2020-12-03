using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[ExecuteInEditMode]
public class OrbController : MonoBehaviour
{
    [Header("PS Parameters")]
    [Range(0, 20000)]
    public float rate;

    [Range(0, 200)]
    public float life;
    [ColorUsage(true,true)]
    public Color color;
    public int colorIntensity;
    [Range(0, 1)]
    public float alpha;
    [Range(0, 5)]
    public float size;
    //[Range(0, 10)]
    public float drag;
    [Range(0, 1)]
    public float velocityDrag;

    [Header("Emitter Parameters")]
    public float sphereSize;

    private VisualEffect _visualEffect;

    // Start is called before the first frame update
    void OnEnable()
    {
        _visualEffect = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisualEffect();
    }

    void UpdateVisualEffect()
    {
        if (_visualEffect == null)
        {
            Debug.LogError(gameObject.name + " should have a visual effect attached to it.");
            return;
        }


        // Ps parameters update
        _visualEffect.SetInt("Rate", Convert.ToInt32(rate));
        _visualEffect.SetFloat("LifeTime", life);
        _visualEffect.SetFloat("Alpha", alpha);
        _visualEffect.SetFloat("Size", size);
        _visualEffect.SetFloat("Linear Drag", drag);
        _visualEffect.SetFloat("Velocity Drag", velocityDrag);

        float factor = Mathf.Pow(2, colorIntensity);
        _visualEffect.SetVector4("Color", new Vector3(color.r* factor, color.g* factor, color.b* factor));

        // Emitter Parameters
        _visualEffect.SetFloat("Sphere Size", sphereSize);
    }
}
