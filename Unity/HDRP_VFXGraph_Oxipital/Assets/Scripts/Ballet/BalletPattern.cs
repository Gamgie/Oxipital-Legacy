using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletPattern : MonoBehaviour
{
    public int id;
    public int dancerCount; 
    public Vector3 position; // Position of this pattern
    public Vector3 axis = Vector3.up; // Orientation of this pattern
    public float size = 1; // Size of this pattern
    public float sizeOffset; // offset between each dancer of this pattern
    public float speed = 1f; // speed of the choreography
    //public float frequency = 1f; // frequency of the oscillation

    protected List<BalletDancer> dancers;
    protected BalletManager mngr;

    protected virtual void Start()
	{
    }

    public void Init(BalletManager balletMngr)
    {
        mngr = balletMngr;
        dancers = new List<BalletDancer>();
    }

    protected virtual void Update()
	{
        transform.position = position;
        transform.eulerAngles = axis;

        dancerCount = dancers.Count;
	}

    public virtual void ApplyMovement()
	{
        Debug.Log("Apply movement parent");
    }

    public void AddDancer()
	{
        if(mngr == null)
		{
            Debug.LogError("No BalletManager attached to " + gameObject.name);
            return;
		}

        BalletDancer dancer = mngr.AddDancer();
        dancers.Add(dancer);
	}

    public bool RemoveDancer(BalletDancer dancer = null)
	{
        bool result = false;

        // Remove dancer from the list of this pattern.
        if(dancer == null) // No target so remove last one in the list
		{
            dancer = dancers[dancers.Count - 1];
            dancers.RemoveAt(dancers.Count - 1);
        }
        else
		{
            // Look for the target in the list.
            BalletDancer dancerToRemove = null;
            foreach (BalletDancer d in dancers)
            {
                if (d.name == dancer.name)
                {
                    dancerToRemove = d;
                }
            }

            // We found it so remove from the list.
            if(dancerToRemove != null)
			{
                result = dancers.Remove(dancerToRemove);
            }
            else
			{
                Debug.LogError("Couldn't find " + dancer.name + " in pattern " + gameObject.name);
                return false;
			}
        }

        // Call manager to finish the job : remove it from main list and destroy it
        if (mngr == null)
        {
            Debug.LogError("No BalletManager attached to " + gameObject.name);
            result = false;
        }
        else
        {
            mngr.RemoveDancer(dancer.id);
        }

        return result;
	}
}
