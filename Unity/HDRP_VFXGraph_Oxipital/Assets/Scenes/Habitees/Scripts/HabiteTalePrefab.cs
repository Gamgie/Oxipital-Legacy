using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HabiteTalePrefab : HabitePrefab
{
    VFXController m_vfxCtrlr;
    public VortexController vortex0;
    public VortexController vortex1;


    public override void ToBeDestroy()
    {
        DOTween.To(() => transform.GetComponentInChildren<Orb>().rate,
                    x => transform.GetComponentInChildren<Orb>().rate = x,
                    0,
                    transitionDuration / 10);

        DOTween.To(() => transform.GetComponentInChildren<Orb>().size,
                    x => transform.GetComponentInChildren<Orb>().size = x,
                    0,
                    transitionDuration / 2).SetDelay(transitionDuration / 2);

        Destroy(this.gameObject, transitionDuration * 2.2f);
    }

    public override void Init(habiteMngr m)
    {
        m_mngr = m;

        // Set id to tale prefab
        id = 2;

        // Start emitting smoothly.
        DOTween.To(() => transform.GetComponentInChildren<Orb>().rate,
                    x => transform.GetComponentInChildren<Orb>().rate = x,
                    0,
                    transitionDuration*2).From();

        m_vfxCtrlr = GetComponent<VFXController>();
    }

    public override void UpdatePrefab()
    {
        if (m_vfxCtrlr == null)
        {
            m_vfxCtrlr = GetComponent<VFXController>();
            return;
        }

        //m_vfxCtrlr.turbIntensity = m_mngr.taleTurbence;
        vortex0.intensity = m_mngr.vortex0Intensity;
        vortex1.intensity = m_mngr.vortex1Intensity;

    }
}
