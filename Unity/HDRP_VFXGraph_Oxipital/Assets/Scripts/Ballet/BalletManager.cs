using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletManager : MonoBehaviour
{
    public bool showDancers; // Show debug object to visualize easier
    public DataManager dataMngr; // Load stored data
    public BalletPattern patternPrefab;
    public List<BalletPattern> patterns; // a list of patterns 

    private OxipitalData data;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize list
        if(patterns == null)
            patterns = new List<BalletPattern>();

        data = dataMngr.LoadData();

        showDancers = (PlayerPrefs.GetInt("ShowDancer") != 0);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(BalletPattern pattern in patterns)
		{
            pattern.UpdateMovement();
            pattern.ApplyMovement();
            pattern.ShowDancer(showDancers);
		}
    }

	private void OnDestroy()
	{
        PlayerPrefs.SetInt("ShowDancer", showDancers ? 1 : 0);
	}

	public BalletPattern AddPattern()
	{
        BalletPattern pattern = Instantiate(patternPrefab) as BalletPattern;
        int newID = 0;
        string patternName = "";

        // Look for the next available ID
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

        patternName = "Pattern " + newID;

        if (pattern != null)
		{
            // Set everything correctly before send it to the universe.
            pattern.id = newID;
            pattern.name = patternName;
            pattern.transform.parent = this.transform;

            // Check if we have available data for this guy.
            if (data == null)
                data = dataMngr.LoadData();
            foreach(BalletPatternData patternData in data.balletMngrData.patternData)
			{
                if(patternData.id == newID)
				{
                    pattern.LoadData(patternData);
				}
			}

            pattern.Init(this);

            // Add to main list
            patterns.Add(pattern);
        }

        return pattern;
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
                    patternToRemove = p;
                    result = patterns.Remove(p);
                    break;
                }
            }
        }

        if (patternToRemove != null)
        {
            Debug.Log("Pattern " + patternToRemove.id + " removed from the list and destroyed.");
            Destroy(patternToRemove.gameObject);
        }

        return result;
    }

    public BalletPattern GetPattern(int id)
	{
        BalletPattern p = null;

        if(id < 0 || id >= patterns.Count)
		{
            Debug.LogError("Trying to get pattern out of bound.");
            return p;
		}

        foreach(BalletPattern pa in patterns)
		{
            if (pa.id == id)
                p = pa;
		}

        if(p == null)
		{
            Debug.LogError("Could find Pattern " + id);
		}

        return p;
	}

    public void SynchronizePattern()
	{
        foreach(BalletPattern bp in patterns)
		{
            bp.ResetSpeed();
		}
	}
}

[System.Serializable]
public class BalletManagerData
{
    public List<BalletPatternData> patternData = new List<BalletPatternData>();
}