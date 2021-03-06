using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HabitePrefab : MonoBehaviour
{
    public float transitionDuration;

    public abstract void ToBeDestroy();
}
