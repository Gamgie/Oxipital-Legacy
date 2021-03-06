using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HabiteAirPrefab : HabitePrefab
{
    public void Start()
    {
        DOTween.To(() => transform.GetComponentInChildren<OrbController>().rate,
                    x => transform.GetComponentInChildren<OrbController>().rate = x,
                    0,
                    transitionDuration).From();
    }

    public override void ToBeDestroy()
    {
        transform.GetComponentInChildren<OrbController>().rate = 0;


        Destroy(this.gameObject, 25);
    }
}
