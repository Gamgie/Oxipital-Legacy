using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletManager : MonoBehaviour
{
    public BalletDancer dancerPrefab;

    public List<BalletDancer> dancers; // a list of objects to choreograph
    public List<BalletForm> forms; // a list of form 
    public int dancerCount = 0;
    public float speed = 1f; // speed of the choreography
    public float radius = 5f; // radius of the circle choreography
    public float frequency = 1f; // frequency of the oscillation

    public bool showDancers; // Show debug object to visualize easier


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(BalletDancer dancer in dancers)
		{
            dancer.isVisible = showDancers;
		}
    }

    public void AddDancer()
	{
        BalletDancer dancer = Instantiate(dancerPrefab) as BalletDancer;
        
        // Look for the next available ID
        int newID = 0;
        foreach(BalletDancer d in dancers)
		{
            if(newID == d.id)
			{
                newID++;
			}
            else
			{
                break;
			}
		}

        dancer.id = newID;
        dancer.name = "Dancer " + dancer.id;
        dancer.transform.parent = gameObject.transform;
        dancers.Add(dancer);
    }

    public bool RemoveDancer(int id=-1)
	{
        bool result = false;
        BalletDancer dancerToRemove = null;

        if(id == -1)
		{
            dancerToRemove = dancers[dancers.Count - 1];
            dancers.RemoveAt(dancers.Count - 1);
            result = true;
		}
        else
		{
            foreach(BalletDancer dancer in dancers)
			{
                if (dancer.id == id)
				{
                    result = dancers.Remove(dancer);
                }
			}
        }

        if(dancerToRemove != null)
		{
            Debug.Log("Dancer " + dancerToRemove.id + " removed from the list and destroyed.");
            Destroy(dancerToRemove);
        }
        
        return result;
	}
}
