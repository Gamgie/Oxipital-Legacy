using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletPattern : MonoBehaviour
{
    public enum BalletPatternType { Line, Circle }

    public int id;
    public int dancerCount;
    public BalletPatternType patternType = BalletPatternType.Circle;
    public Vector3 position; // Position of this pattern
    public Vector3 rotation = Vector3.zero; // Rotation in euler angle of this pattern
    public float size = 1; // Size of this pattern
    public float sizeOffset; // offset between each dancer of this pattern
    public float speed = 1f; // speed of the choreography
    public float lerpDuration = 3f; // Time for moving from a pattern to another
    //public float frequency = 1f; // frequency of the oscillation

    [Header("Circle Parameter")]
    public float verticalOffset;

    [Header("Position Alteration")]
    public float LFOFrequency = 0;
    public float noiseAmplitude = 0;
    public float noiseSpeed = 0;

    List<BalletDancer> dancers;
    BalletManager mngr;

    List<Vector3> cirlePositions; // Computed position of the circle
    List<Vector3> linePositions; // Computed position of the line

    void Start()
	{
        cirlePositions = new List<Vector3>();
        linePositions = new List<Vector3>();
    }

    public void Init(BalletManager balletMngr)
    {
        mngr = balletMngr;
        dancers = new List<BalletDancer>();
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

    public void AddDancer()
	{
        if(mngr == null)
		{
            Debug.LogError("No BalletManager attached to " + gameObject.name);
            return;
		}

        BalletDancer dancer = mngr.AddDancer();
        dancers.Add(dancer);
        cirlePositions.Add(new Vector3());
        linePositions.Add(new Vector3());
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

            // We found it so remove it from the list.
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

    void ComputeCircleMovement()
	{
        for (int i = 0; i < dancers.Count; i++)
        {
            if(size != 0)
			{
                cirlePositions[i] = new Vector3(Mathf.Sin(Time.time * speed / size + i * Mathf.PI * 2f / dancers.Count) * size * 2,
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
}
