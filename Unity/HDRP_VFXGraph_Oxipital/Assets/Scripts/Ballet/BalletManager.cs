using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletManager : MonoBehaviour
{
    public enum BalletPatternType { Line, Circle}

    public BalletDancer dancerPrefab;
    public CirclePattern circlePatternPrefab;
    public CirclePattern linePatternPrefab;

    public List<BalletDancer> dancers; // a list of objects to choreograph
    public List<BalletPattern> patterns; // a list of patterns 
    
    public bool showDancers; // Show debug object to visualize easier

    private Transform patternParent;
    private Transform dancerParent;

    // Start is called before the first frame update
    void Start()
    {
        // Create pattern parent object
        patternParent = new GameObject().transform;
        patternParent.name = "Patterns Parent";
        patternParent.parent = gameObject.transform;

        // Create dancer parent object
        dancerParent = new GameObject().transform;
        dancerParent.name = "Dancers Parent";
        dancerParent.parent = gameObject.transform;

        // Initialize list
        dancers = new List<BalletDancer>();
        patterns = new List<BalletPattern>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach(BalletDancer dancer in dancers)
		{
            dancer.isVisible = showDancers;
		}

        foreach(BalletPattern pattern in patterns)
		{
            pattern.ApplyMovement();
		}
    }

    public BalletDancer AddDancer()
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

        // Set everything correctly before send it to the universe.
        dancer.id = newID;
        dancer.name = "Dancer " + dancer.id;
        dancer.transform.parent = dancerParent;
        dancers.Add(dancer);

        return dancer;
    }

    public bool RemoveDancer(int id=-1)
	{
        bool result = false;
        BalletDancer dancerToRemove = null;

        // If no dancer here, let's get out directly.
        if (dancers.Count == 0)
            return false;

        // If id is -1, it means we want to remove last object
        if (id == -1)
		{
            dancerToRemove = dancers[dancers.Count - 1];
            dancers.RemoveAt(dancers.Count - 1);
            result = true;
		}
        else // Let's find the right guy to kill
        {
            foreach(BalletDancer dancer in dancers)
			{
                if (dancer.id == id)
				{
                    dancerToRemove = dancer;
                }
			}
            result = dancers.Remove(dancerToRemove);
        }

        if(dancerToRemove != null)
		{
            Debug.Log("Dancer " + dancerToRemove.id + " removed from the list and destroyed.");
            Destroy(dancerToRemove.gameObject);
        }
        else
		{
            Debug.LogError("Couldn't find the dancer to remove from the list");
		}
        
        return result;
	}

    public void AddPattern(BalletPatternType type)
	{
        BalletPattern pattern = null;

        // Look for the next available ID
        int newID = 0;
        foreach (BalletPattern p in patterns)
        {
            if (newID == p.id)
            {
                newID++;
            }
            else
            {
                break;
            }
        }

        // Choose a beautiful name
        string patternName = "";
        switch (type)
		{
            case BalletPatternType.Circle:
                patternName = "Circle";
                pattern = Instantiate(circlePatternPrefab) as BalletPattern;
                break;
            case BalletPatternType.Line:
                patternName = "Line";
                pattern = Instantiate(linePatternPrefab) as BalletPattern;
                break;
		}

        
        if(pattern != null)
		{
            // Set everything correctly before send it to the universe.
            pattern.id = newID;
            pattern.name = patternName + " " + pattern.id;
            pattern.transform.parent = patternParent;
            pattern.Init(this);
            patterns.Add(pattern);
        }
    }

    public bool RemovePattern(int id=-1)
	{
        bool result = false;
        BalletPattern patternToRemove = null;

        // If no patterns here, let's get out directly.
        if (patterns.Count == 0)
            return false;

        // If id is -1, it means we want to remove last object
        if (id == -1)
        {
            patternToRemove = patterns[patterns.Count - 1];
            patterns.RemoveAt(patterns.Count - 1);
            result = true;
        }
        else // Let's find the right guy to kill
        {
            foreach (BalletPattern p in patterns)
            {
                if (p.id == id)
                {
                    result = patterns.Remove(p);
                }
            }
        }

        if (patternToRemove != null)
        {
            Debug.Log("Dancer " + patternToRemove.id + " removed from the list and destroyed.");
            Destroy(patternToRemove.gameObject);
        }

        return result;
    }
}
