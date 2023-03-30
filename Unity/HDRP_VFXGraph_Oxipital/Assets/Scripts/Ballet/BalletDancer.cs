using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalletDancer : MonoBehaviour
{
    public int id;
    public bool isVisible;
    public bool isLinked;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        color = GenerateRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().enabled = isVisible;
        GetComponent<MeshRenderer>().material.color = color;
    }

    private Color GenerateRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    }
}
