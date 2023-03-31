using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletManager : MonoBehaviour
{
    public bool showDancers; // Show debug object to visualize easier
    public BalletPattern patternPrefab;
    public List<BalletPattern> patterns; // a list of patterns 

    // Start is called before the first frame update
    void Start()
    {
        // Initialize list
        patterns = new List<BalletPattern>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(BalletPattern pattern in patterns)
		{
            pattern.ApplyMovement();
            pattern.ShowDancer(showDancers);
		}
    }

    public void AddPattern()
	{
        BalletPattern pattern = Instantiate(patternPrefab) as BalletPattern;

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

        string patternName = "Pattern " + newID;
        
        if(pattern != null)
		{
            // Set everything correctly before send it to the universe.
            pattern.id = newID;
            pattern.name = patternName;
            pattern.transform.parent = this.transform;
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

[System.Serializable]
public class BalletManagerData
{
    public List<BalletPatternData> patternData = new List<BalletPatternData>();
}