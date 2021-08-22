using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    public int sceneIndex;

    public void LoadScene()
    {
        Application.LoadLevel(sceneIndex);
    }
}
