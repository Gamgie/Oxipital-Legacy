using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HabitePrefab : MonoBehaviour
{
    public float transitionDuration;

    // 0 : air
    // 1 : corpse
    // 2 : tale
    public int id;

    protected habiteMngr m_mngr;

    public abstract void ToBeDestroy();
    public abstract void Init(habiteMngr m);
    public abstract void UpdatePrefab();
}
