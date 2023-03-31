using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletPattern : MonoBehaviour
{
    public enum BalletPatternType { Line, Circle }

    public BalletDancer dancerPrefab;

    public int id;
    public int dancerCount;
    public BalletPatternType patternType = BalletPatternType.Circle;
    public Vector3 position; // Position of this pattern
    public Vector3 rotation = Vector3.zero; // Rotation in euler angle of this pattern
    public float size = 1; // Size of this pattern
    public float sizeOffset; // offset between each dancer of this pattern
    public float speed = 1f; // speed of the choreography
    public float lerpDuration = 3f; // Time for moving from a pattern to another
    public float phase; // Rotation phase
    //public float frequency = 1f; // frequency of the oscillation

    [Header("Circle Parameter")]
    public float verticalOffset;

    [Header("Position Alteration")]
    public float LFOFrequency = 0;
    public Vector3 LFODirection = Vector3.zero;
    public float noiseAmplitude = 0;
    public float noiseSpeed = 0;

    private List<BalletDancer> dancers; // a list of objects to choreograph
    private BalletPatternData data = new BalletPatternData();

    List<Vector3> cirlePositions; // Computed position of the circle
    List<Vector3> linePositions; // Computed position of the line

    public void Init(BalletManager balletMngr)
    {
        // Initialize list
        dancers = new List<BalletDancer>();
        cirlePositions = new List<Vector3>();
        linePositions = new List<Vector3>();
    }

    void Update()
	{
        transform.position = position;
        transform.eulerAngles = rotation;

        dancerCount = dancers.Count;

        ComputeCircleMovement();
        ComputeLineMovement();
    }

    public void ApplyMovement()
	{
        List<Vector3> target = null;

        // Look for the good target
        switch (patternType)
		{
            case BalletPatternType.Circle:
                target = cirlePositions;
                break;
            case BalletPatternType.Line:
                target = linePositions;
                break;
        }

        // Apply movement
        for(int i = 0; i < dancers.Count; i++)
		{
            dancers[i].transform.position = Vector3.Lerp(dancers[i].transform.position, target[i],0.1f);
		}
    }

    public void ShowDancer(bool isVisible)
	{
        foreach(BalletDancer d in dancers)
		{
            d.isVisible = isVisible;
		}
	}

    public BalletDancer AddDancer()
	{
        BalletDancer dancer = Instantiate(dancerPrefab) as BalletDancer;

        // Look for the next available ID
        int newID = 0;
        foreach (BalletDancer d in dancers)
        {
            if (newID == d.id)
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
        dancer.transform.parent = this.transform;

        dancers.Add(dancer);
        cirlePositions.Add(new Vector3());
        linePositions.Add(new Vector3());

        return dancer;
    }

    public bool RemoveDancer(int id = -1)
	{
        bool result = false;
        BalletDancer dancerToRemove = null;

        // If no dancer here, let's get out directly.
        if (dancers.Count < 1)
            return false;

        // Remove dancer from the list of this pattern.
        if (id == -1) // If id is -1, it means we want to remove last object
        {
            dancerToRemove = dancers[dancers.Count - 1];
            dancers.RemoveAt(dancers.Count - 1);
            result = true;
        }
        else // Look for the target in the list.
        {
            foreach (BalletDancer d in dancers)
            {
                if (d.id == id)
                {
                    dancerToRemove = d;
                }
            }

            // We found it so remove it from the list.
            if(dancerToRemove != null)
			{
                result = dancers.Remove(dancerToRemove);
            }
            else
			{
                Debug.LogError("Couldn't find Dancer " + id + " in pattern " + gameObject.name);
                return false;
			}
        }

        if (dancerToRemove != null)
        {
            Debug.Log("Dancer " + dancerToRemove.id + " removed from the list and destroyed.");
            Destroy(dancerToRemove.gameObject);
        }

        return result;
	}

    void ComputeCircleMovement()
	{
        for (int i = 0; i < dancers.Count; i++)
        {
            if(size != 0)
			{
                cirlePositions[i] = new Vector3( Mathf.Sin(Time.time * speed / size + i * Mathf.PI * 2f / dancers.Count) * size * 2,
                                                 0f,
                                                Mathf.Cos(Time.time * speed / size + i * Mathf.PI * 2f / dancers.Count) * size * 2);
                cirlePositions[i] = transform.TransformPoint(cirlePositions[i]);
            }
            else
			{
                cirlePositions[i] = Vector3.zero;

            }
            
        }
    }

    void ComputeLineMovement()
	{
        if (dancerCount == 0)
            return;

        // Set points positions
        for (int i = 0; i < dancers.Count; i++)
        {
            linePositions[i] = new Vector3(0,
                                           (i * size * 4 / dancers.Count),
                                           0);

            linePositions[i] = transform.TransformPoint(linePositions[i]);
        }

        // And then offset to center the line on the origin
        Vector3 firstPoint = linePositions[0];
        Vector3 lastPoint = linePositions[linePositions.Count-1];
        Vector3 midPoint = (firstPoint + lastPoint) / 2f;

        for (int i = 0; i < dancers.Count; i++)
        {
            linePositions[i] -= midPoint;
        }
    }

    public BalletPatternData StoreData()
	{
        data.id = id;
        data.dancerCount = dancerCount;
        data.patternType = (int)patternType;
        data.position = position;
        data.rotation = rotation; 
        data.size = size; 
        data.sizeOffset = sizeOffset;
        data.speed = speed;
        data.lerpDuration = lerpDuration;
        data.verticalOffset = verticalOffset;
        data.LFOFrequency = LFOFrequency;
        data.noiseAmplitude = noiseAmplitude;
        data.noiseSpeed = noiseSpeed;

        return data;
	}
}


[System.Serializable]
public class BalletPatternData
{
    public int id;
    public int dancerCount;
    public int patternType;
    public Vector3 position;
    public Vector3 rotation;
    public float size; 
    public float sizeOffset; 
    public float speed; 
    public float lerpDuration;
    public float verticalOffset;
    public float LFOFrequency;
    public float noiseAmplitude;
    public float noiseSpeed;
}