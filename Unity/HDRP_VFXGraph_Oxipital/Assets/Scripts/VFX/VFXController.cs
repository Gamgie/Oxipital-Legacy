using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

public class VFXController : MonoBehaviour
{
    private List<OrbGroup> orbsVisualEffect;

    [Header("Orbs Positions")]
    //public float orbPositionRadius;
    //public float orbRotationSpeedX;
    //public float orbRotationSpeedY;
    //public float orbRotationSpeedZ;
    public int orbGroupCount;
    public OrbGroup orbGroupPrefab;
    public GameObject emitterOrb;

    // Start is called before the first frame update
    void OnEnable()
    {
        if(orbsVisualEffect != null)
        {
            foreach(OrbGroup o in orbsVisualEffect)
            {
                Destroy(o.gameObject);
            }
        }

        orbsVisualEffect = new List<OrbGroup>();
        orbGroupCount = PlayerPrefs.GetInt("OrbCount", orbGroupCount);

        for (int i=0; i<orbGroupCount;i++)
        {
            if (orbGroupCount > orbsVisualEffect.Count)
            {
                AddOrbGroup();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(orbGroupCount > orbsVisualEffect.Count)
        {
            AddOrbGroup();
        }
        else if(orbGroupCount < orbsVisualEffect.Count)
        {
            DestroyOrbGroup(orbsVisualEffect.Count - 1);
        }
    }

    private void OnDestroy()
    {
        // Save how many orbs where there
        PlayerPrefs.SetInt("OrbCount", orbGroupCount);
    }

    public void KillAllParticles()
    {
        if (orbsVisualEffect == null)
            return;

        foreach (OrbGroup o in orbsVisualEffect)
        {
            o.Reinit();
        }
    }

    public void AddOrbGroup()
    {
        if (orbsVisualEffect == null)
        {
            Debug.LogError("Try to add an orb in a null list");
            return;
        }

        OrbGroup o = Instantiate(orbGroupPrefab);
        o.gameObject.name = "OrbGroup" + orbsVisualEffect.Count;
        o.transform.parent = emitterOrb.transform;
        o.orbGroupId = orbsVisualEffect.Count;
        o.Initialize();
        orbsVisualEffect.Add(o);
    }

    public void DestroyOrbGroup(int index)
    {
        if (orbsVisualEffect == null)
        {
            Debug.LogError("Try to destroy an orb in a null list");
            return;
        }

        OrbGroup orbToBeDestroyed = orbsVisualEffect[index];
        Destroy(orbToBeDestroyed.gameObject);
        orbsVisualEffect.RemoveAt(index);
    }

    //private void UpdateOrbPosition()
    //{
    //    if (orbsVisualEffect == null)
    //    {
    //        Debug.LogError("No orb list found");
    //        return;
    //    }

    //    for (int i = 0; i < orbsVisualEffect.Count; i++)
    //    {
    //        float xPos = orbPositionRadius * Mathf.Cos(i * 360 / orbGroupCount * Mathf.Deg2Rad);
    //        float yPos = orbPositionRadius * Mathf.Sin(i * 360 / orbGroupCount * Mathf.Deg2Rad);
    //        orbsVisualEffect[i].emitterPosition = new Vector3(xPos,yPos,0);
    //    }
    //}

    //public void ResetOrbPosition()
    //{
    //    emitterOrb.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0,0,0));
    //}
}
