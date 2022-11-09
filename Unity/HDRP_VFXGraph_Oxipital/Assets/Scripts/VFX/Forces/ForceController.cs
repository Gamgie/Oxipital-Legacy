using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class ForceController : MonoBehaviour
{

    public int forceID = 0;
    public VFXController orbs;

    protected VisualEffect[] m_vfxs;
    protected string suffix = "";

    private void OnEnable()
    {
        UpdateVfxArray();

        // Set suffix to handle same multiple force on the same object.
        if (forceID != 0)
        {
            suffix = " " + forceID.ToString();
        }
    }

    protected virtual void Update()
    {
        foreach(VisualEffect vfx in m_vfxs)
        {
            if (vfx == null)
                UpdateVfxArray();
        }
    }

    void UpdateVfxArray()
    {
        if (orbs == null)
        {
            m_vfxs = GetComponentsInChildren<VisualEffect>();
        }
        else
        {
            m_vfxs = orbs.GetComponentsInChildren<VisualEffect>();
        }
    }
}
