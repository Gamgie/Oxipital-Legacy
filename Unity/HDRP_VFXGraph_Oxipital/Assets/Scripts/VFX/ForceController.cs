using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class ForceController : MonoBehaviour
{

    public int forceID = 0;

    protected VisualEffect[] m_vfxs;
    protected string suffix = "";

    private void OnEnable()
    {
        m_vfxs = GetComponentsInChildren<VisualEffect>();

        // Set suffix to handle same multiple force on the same object.
        if (forceID != 0)
        {
            suffix = " " + forceID.ToString();
        }
    }

}