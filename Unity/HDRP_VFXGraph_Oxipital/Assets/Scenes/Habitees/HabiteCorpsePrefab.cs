using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HabiteCorpsePrefab : HabitePrefab
{
    public GameObject lightRoot;

    public void Start()
    {
        Light[] lights = lightRoot.GetComponentsInChildren<Light>();

        foreach (Light l in lights)
        {
            l.DOIntensity(0, transitionDuration).From();
        }

        DOTween.To(() => transform.GetComponentInChildren<OrbController>().rate,
                    x => transform.GetComponentInChildren<OrbController>().rate = x,
                    0,
                    transitionDuration).From();
    }

    public override void ToBeDestroy()
    {
        Light[] lights = lightRoot.GetComponentsInChildren<Light>();

        foreach(Light l in lights)
        {
            l.DOIntensity(0, transitionDuration);
        }

        transform.GetComponentInChildren<OrbController>().rate = 0;
        DOTween.To(() => transform.GetComponentInChildren<OrbController>().size,
                    x => transform.GetComponentInChildren<OrbController>().size = x,
                    0,
                    transitionDuration).SetDelay(transitionDuration / 2);

        Destroy(this.gameObject, 25);
    }
}
