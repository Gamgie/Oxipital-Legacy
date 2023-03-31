using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePattern : BalletPattern
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        position = Vector3.zero;
        axis = Vector3.up;
        size = 1;
        sizeOffset = 0;
        speed = 1;
    }

    public override void ApplyMovement()
    {
        base.Update();

        for(int i = 0; i > dancers.Count; i++)
		{
            Vector3 pos = new Vector3(Mathf.Sin(Time.time * speed + i * Mathf.PI * 2f / dancers.Count) * size, 0f, Mathf.Cos(Time.time * speed + i * Mathf.PI * 2f / dancers.Count) * size);
            dancers[i].transform.position = pos;
            Debug.Log("Apply movement");
        }
    }
}
