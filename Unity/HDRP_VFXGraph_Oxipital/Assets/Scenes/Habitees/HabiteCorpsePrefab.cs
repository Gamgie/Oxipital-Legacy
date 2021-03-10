using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class HabiteCorpsePrefab : HabitePrefab
{
    public GameObject lightRoot;
    public VisualEffect vfx;

    public override void Init(habiteMngr m)
    {
        Light[] lights = lightRoot.GetComponentsInChildren<Light>();

        // Set id to corpse prefab
        id = 1;

        // Set manager reference for update purpose
        m_mngr = m;

        // Turn on lights
        foreach (Light l in lights)
        {
            l.DOIntensity(0, transitionDuration).From();
            Debug.Log(l.gameObject.name);
        }

        // Start emitting smoothly.
        DOTween.To(() => transform.GetComponentInChildren<OrbController>().rate,
                    x => transform.GetComponentInChildren<OrbController>().rate = x,
                    0,
                    transitionDuration).From();
    }

    public override void ToBeDestroy()
    {
        Light[] lights = lightRoot.GetComponentsInChildren<Light>();

        // Turn off lights
        foreach(Light l in lights)
        {
            l.DOIntensity(0, transitionDuration);
            Debug.Log(l.gameObject.name);
        }

        // Stop Emitting smoothly
        transform.GetComponentInChildren<OrbController>().rate = 0;
        DOTween.To(() => transform.GetComponentInChildren<OrbController>().size,
                    x => transform.GetComponentInChildren<OrbController>().size = x,
                    0,
                    transitionDuration).SetDelay(transitionDuration / 2);

        Destroy(this.gameObject, transitionDuration * 1.2f);
    }

    public override void UpdatePrefab()
    {
        if (vfx.HasFloat("Noise Intensity") == true)
            vfx.SetFloat("Noise Intensity", m_mngr.noiseIntensity);

        if (vfx.HasFloat("Noise Frequency") == true)
            vfx.SetFloat("Noise Frequency", m_mngr.noiseFrequency);

        if (vfx.HasFloat("Turb Intensity") == true)
            vfx.SetFloat("Turb Intensity", m_mngr.turbIntensity);

        if (vfx.HasFloat("Turb Frequency") == true)
            vfx.SetFloat("Turb Frequency", m_mngr.turbFrequency);

        if (vfx.HasFloat("Left Swirl") == true)
            vfx.SetFloat("Left Swirl", m_mngr.leftSwirl);

        if (vfx.HasFloat("Right Swirl") == true)
            vfx.SetFloat("Right Swirl", m_mngr.rightSwirl);

        if (vfx.HasFloat("Orbita") == true)
            vfx.SetFloat("Orbita", m_mngr.orbita);

    }
}
