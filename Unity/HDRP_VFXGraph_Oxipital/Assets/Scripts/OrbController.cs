using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[ExecuteInEditMode]
public class OrbController : MonoBehaviour
{
    public enum EmitterShape { Sphere, Plane, Torus, Cube}

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
        if (_visualEffect.HasInt("Rate") == true)
            _visualEffect.SetInt("Rate", Convert.ToInt32(rate));

        if (_visualEffect.HasFloat("LifeTime") == true)
            _visualEffect.SetFloat("LifeTime", life);

        if (_visualEffect.HasFloat("Alpha") == true)
            _visualEffect.SetFloat("Alpha", alpha);

        if (_visualEffect.HasFloat("Size") == true)
            _visualEffect.SetFloat("Size", size);

        if (_visualEffect.HasFloat("Linear Drag") == true)
            _visualEffect.SetFloat("Linear Drag", drag);

        if (_visualEffect.HasFloat("Velocity Drag") == true)
            _visualEffect.SetFloat("Velocity Drag", velocityDrag);

        float factor = Mathf.Pow(2, colorIntensity);
        if(_visualEffect.HasVector4("Color") == true)
            _visualEffect.SetVector4("Color", new Vector3(color.r* factor, color.g* factor, color.b* factor));

        // Emitter Parameters
        if (_visualEffect.HasFloat("Sphere Size") == true)
            _visualEffect.SetFloat("Sphere Size", sphereSize);
    }
}
