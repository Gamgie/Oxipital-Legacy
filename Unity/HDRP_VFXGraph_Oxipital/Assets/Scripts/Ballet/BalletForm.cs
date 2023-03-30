using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletForm 
{
    public Vector3 position; // Position of this form
    public Vector3 axis; // Orientation of this form
    public float size; // Size of this form
    public float sizeOffset; // offset between each dancer of this form

    List<GameObject> positions;

    public void Start()
	{
        List<GameObject> positions = new List<GameObject>();
    }

    protected virtual void ApplyMovement()
	{

	}

    public void AddDancer(GameObject go)
	{
        positions.Add(go);
	}

    public bool RemoveDancer(string id)
	{
        bool result = false;

        foreach(GameObject go in positions)
		{
            if(go.name == id)
			{
                result = positions.Remove(go);
			}
		}

        return result;
	}
}
