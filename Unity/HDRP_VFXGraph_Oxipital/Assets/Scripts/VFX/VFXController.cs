using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

public class VFXController : MonoBehaviour
{
    private List<Orb> m_orbsVisualEffect;

    [Header("Orbs Positions")]
    public float orbPositionRadius;
    public float orbAngle;
    public int orbCount;
    public Orb OrbPrefab;

    [Header("Turbulence")]
    public float turbIntensity;
    public float turbFrequency;
    [Range(1, 8)]
    public int turbOctave;
    [Range(0,1f)]
    public float turbroughness;
    public float turbLacunarity;
    public float turbScale;

    [Header("Zalem Gravity")]
    public float zalemGravity;
    public Vector3 gravityAxis;

    [Header("Swirl")]
    public float swirlIntensity;
    public Vector3 swirlAxis;
    public Vector3 swirlOrigin;
    public float swirlRadius;

    [Header("Axial Force")]
    public float axialIntensity;
    public Vector3 axialAxis;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_orbsVisualEffect = new List<Orb>();

        for(int i=0; i<orbCount;i++)
        {
            AddOrb();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(orbCount > m_orbsVisualEffect.Count)
        {
            AddOrb();
        }
        else if(orbCount < m_orbsVisualEffect.Count)
        {
            DestroyOrb(orbCount - 1);
        }
            UpdateVisualEffect();
    }

    void UpdateVisualEffect()
    {
        if (m_orbsVisualEffect == null)
            return;

        foreach(Orb o in m_orbsVisualEffect)
        {
            // Turb parameter update
            if (o.Vfx.HasFloat("Turb Intensity") == true)
                o.Vfx.SetFloat("Turb Intensity", turbIntensity);

            if (o.Vfx.HasFloat("Turb Frequency") == true)
                o.Vfx.SetFloat("Turb Frequency", turbFrequency);

            if (o.Vfx.HasInt("Octave") == true)
                o.Vfx.SetInt("Octave", turbOctave);

            if (o.Vfx.HasFloat("Roughness") == true)
                o.Vfx.SetFloat("Roughness", turbroughness);

            if (o.Vfx.HasFloat("Lacunarity") == true)
                o.Vfx.SetFloat("Lacunarity", turbLacunarity);

            if (o.Vfx.HasFloat("Turb Scale") == true)
                o.Vfx.SetFloat("Turb Scale", turbScale);

            // Gravity parameter update
            if (o.Vfx.HasFloat("Gravity") == true)
                o.Vfx.SetFloat("Gravity", zalemGravity);

            if(o.Vfx.HasVector3("Gravity Axis"))
                o.Vfx.SetVector3("Gravity Axis", gravityAxis);


            // Swirl parameter update
            if (o.Vfx.HasFloat("Swirl Intensity") == true)
                o.Vfx.SetFloat("Swirl Intensity", swirlIntensity);

            if (o.Vfx.HasVector3("Swirl Axis"))
                o.Vfx.SetVector3("Swirl Axis", swirlAxis);

            if (o.Vfx.HasVector3("Swirl Origin"))
                o.Vfx.SetVector3("Swirl Origin", swirlOrigin);

            if (o.Vfx.HasFloat("Swirl Radius") == true)
                o.Vfx.SetFloat("Swirl Radius", swirlRadius);


            // Axial parameter update
            if (o.Vfx.HasFloat("Axial Intensity") == true)
                o.Vfx.SetFloat("Axial Intensity", axialIntensity);

            if (o.Vfx.HasVector3("Axial Axis"))
                o.Vfx.SetVector3("Axial Axis", axialAxis);
        }
    }

    public void KillAllParticles()
    {
        if (m_orbsVisualEffect == null)
            return;

        foreach (Orb o in m_orbsVisualEffect)
        {
            o.Vfx.Reinit();
        }
    }

    public void AddOrb()
    {
        if (m_orbsVisualEffect == null)
        {
            Debug.LogError("Try to add an orb in a null list");
            return;
        }

        Orb o = Instantiate(OrbPrefab);
        o.gameObject.name = "Orb" + m_orbsVisualEffect.Count;
        o.transform.parent = this.transform;
        m_orbsVisualEffect.Add(o);
    }

    public void DestroyOrb(int index)
    {
        if (m_orbsVisualEffect == null)
        {
            Debug.LogError("Try to destroy an orb in a null list");
            return;
        }
    }
}
