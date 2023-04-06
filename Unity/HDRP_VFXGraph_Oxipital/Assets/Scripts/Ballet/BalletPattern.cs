using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletPattern : MonoBehaviour
{
    public enum BalletPatternType { Point, Line, Circle }

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
    private List<Vector3> cirlePositions; // Computed position of the circle
    private List<Vector3> linePositions; // Computed position of the line
    private float currentSpeed;

    public void Init(BalletManager balletMngr)
    {
        // Initialize list
        dancers = new List<BalletDancer>();
        cirlePositions = new List<Vector3>();
        linePositions = new List<Vector3>();

        UpdateDancerCount(dancerCount);

        currentSpeed = speed;
    }

    public void UpdateMovement()
	{
        transform.position = position;
        transform.eulerAngles = rotation;

        // Update Speed
        currentSpeed += speed / 1000;

        ComputeCircleMovement();
        ComputeLineMovement();
    }

    public void ApplyMovement()
	{
        if (dancers == null)
            return;

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
            case BalletPatternType.Point:
                List<Vector3> onePosition = new List<Vector3>();
                foreach (BalletDancer d in dancers)
                    onePosition.Add(position);

                target = onePosition;
                break;
        }

        // Apply movement
        for(int i = 0; i < dancers.Count; i++)
		{
            if (dancers[i] != null)
                dancers[i].transform.position = Vector3.Lerp(dancers[i].transform.position, target[i],0.1f);
		}
    }

    public void ShowDancer(bool isVisible)
	{
        if (dancers == null)
            return;

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

        Debug.Log("Dancer " + dancer.id + " created and added to pattern" + id);

        // Update dancerCount
        dancerCount = dancers.Count;

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
            dancerCount = dancers.Count;
        }

        return result;
	}

    void ComputeCircleMovement()
	{
        for (int i = 0; i < dancers.Count; i++)
        {
            if(size != 0)
			{
                cirlePositions[i] = new Vector3( Mathf.Sin(currentSpeed + i * Mathf.PI * 2f / dancers.Count) * size / 2,
                                                 Mathf.Cos(currentSpeed + i * Mathf.PI * 2f/ dancers.Count) * size / 2,
                                                0f);
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

        if(dancerCount == 1)
		{
            linePositions[0] = Vector3.zero;
            linePositions[0] = transform.TransformPoint(linePositions[0]);
            return;
        }

        // Set points positions
        for (int i = 0; i < dancers.Count; i++)
        {
            linePositions[i] = new Vector3(0,
                                           (i * size / (dancers.Count - 1)),
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

        BalletPatternData data = new BalletPatternData();
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

    public void LoadData(BalletPatternData data)
	{
        id = data.id;
        dancerCount = data.dancerCount;
        patternType = (BalletPatternType) data.patternType;
        position = data.position;
        rotation = data.rotation;
        size = data.size;
        sizeOffset = data.sizeOffset;
        speed = data.speed;
        lerpDuration = data.lerpDuration;
        verticalOffset = data.verticalOffset;
        LFOFrequency = data.LFOFrequency;
        noiseAmplitude = data.noiseAmplitude;
        noiseSpeed = data.noiseSpeed;
    }

    public Vector3 GetPosition(int idPos)
	{
        Vector3 result = Vector3.zero; 

        if(idPos >= 0 && idPos < dancerCount)
		{
            result = dancers[idPos].transform.position;
		}

        return result;
	}

    public void UpdateDancerCount(int count)
	{
        if (count > dancers.Count)
        {
            int instanceCount = count - dancers.Count;
            for (int i = 0; i < instanceCount; i++)
            {
                AddDancer();
            }
        }
        else if(count < dancers.Count)
        {
            int removeCount = dancers.Count - count;
            for (int i = 0; i < removeCount; i++)
            {
                RemoveDancer();
            }
        }
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