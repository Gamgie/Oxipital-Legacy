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
    public float orbRotationSpeedX;
    public float orbRotationSpeedY;
    public float orbRotationSpeedZ;
    public int orbCount;
    public Orb OrbPrefab;
    public GameObject emitterOrb;

    // Start is called before the first frame update
    void OnEnable()
    {
        if(m_orbsVisualEffect != null)
        {
            foreach(Orb o in m_orbsVisualEffect)
            {
                Destroy(o.gameObject);
            }
        }

        m_orbsVisualEffect = new List<Orb>();
        orbCount = PlayerPrefs.GetInt("OrbCount", orbCount);

        for (int i=0; i<orbCount;i++)
        {
            if (orbCount > m_orbsVisualEffect.Count)
            {
                AddOrb();
            }
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
            DestroyOrb(m_orbsVisualEffect.Count - 1);
        }

        // Rotate orbs
        UpdateOrbPosition();
        emitterOrb.transform.Rotate(orbRotationSpeedX * Time.deltaTime, orbRotationSpeedY * Time.deltaTime, orbRotationSpeedZ * Time.deltaTime);
    }

    private void OnDestroy()
    {
        // Save how many orbs where there
        PlayerPrefs.SetInt("OrbCount", orbCount);
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
        o.transform.parent = emitterOrb.transform;
        m_orbsVisualEffect.Add(o);
    }

    public void DestroyOrb(int index)
    {
        if (m_orbsVisualEffect == null)
        {
            Debug.LogError("Try to destroy an orb in a null list");
            return;
        }

        Orb orbToBeDestroyed = m_orbsVisualEffect[index];
        Destroy(orbToBeDestroyed.gameObject);
        m_orbsVisualEffect.RemoveAt(index);
    }

    private void UpdateOrbPosition()
    {
        if (m_orbsVisualEffect == null)
        {
            Debug.LogError("No orb list found");
            return;
        }

        for (int i = 0; i < m_orbsVisualEffect.Count; i++)
        {
            float xPos = orbPositionRadius * Mathf.Cos(i * 360 / orbCount * Mathf.Deg2Rad);
            float yPos = orbPositionRadius * Mathf.Sin(i * 360 / orbCount * Mathf.Deg2Rad);
            m_orbsVisualEffect[i].emitterPosition = new Vector3(xPos,yPos,0);
        }
    }

    public void ResetOrbPosition()
    {
        emitterOrb.transform.SetPositionAndRotation(Vector3.zero, Quaternion.EulerAngles(0,0,0));
    }
}
