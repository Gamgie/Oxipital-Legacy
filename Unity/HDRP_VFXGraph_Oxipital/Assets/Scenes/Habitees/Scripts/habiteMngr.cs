using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class habiteMngr : MonoBehaviour
{
    public GameObject airPrefab;
    public GameObject corpsePrefab;
    public GameObject talePrefab;
    public float sceneTransitionDuration;
    public int defaultScene;

    [Header("Air Parameters")]
    public float temperatureAmount;
    public float densityBuoyancy;
    public float densityWeight;
    public float emitterRadius;

    [Header("Corpse Parameters")]
    public float leftSwirl;
    public float rightSwirl;
    public float orbita;
    public float noiseIntensity;
    public float noiseFrequency;
    public float turbIntensity;
    public float turbFrequency;

    [Header("Tale Parameters")]
    public float taleTurbence;

    private GameObject actualPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LoadScene(defaultScene);
    }


    private void LoadScene(int sceneid)
    {
        if(actualPrefab)
        {
            // Check if we are not trying to load the already loaded prefab.
            if(actualPrefab.GetComponent<HabitePrefab>().id != sceneid)
            {
                actualPrefab.GetComponent<HabitePrefab>().transitionDuration = sceneTransitionDuration;
                actualPrefab.GetComponent<HabitePrefab>().ToBeDestroy();
            }
            else
            {
                return;
            }
        }

        if(sceneid == 0) // Air Scene
        {
            actualPrefab = Instantiate(airPrefab);
        }
        else if(sceneid == 1) // Corpse Scene
        {
            actualPrefab = Instantiate(corpsePrefab);
        }
        else if(sceneid == 2) // Tale Scene
        {
            actualPrefab = Instantiate(talePrefab);
        }

        // Set transition time and init the instancianted prefab.
        actualPrefab.GetComponent<HabitePrefab>().transitionDuration = sceneTransitionDuration;
        actualPrefab.GetComponent<HabitePrefab>().Init(this);
    }

    public void LoadAir()
    {
        LoadScene(0);
    }

    public void LoadCorpse()
    {
        LoadScene(1);
    }

    public void LoadTale()
    {
        LoadScene(2);
    }

    public void LoadWater()
    {
        UnloadAllScene();
    }

    public void UnloadAllScene()
    {
        if (actualPrefab != null)
        {
            actualPrefab.GetComponent<HabitePrefab>().transitionDuration = sceneTransitionDuration;
            actualPrefab.GetComponent<HabitePrefab>().ToBeDestroy();
            actualPrefab = null;
        }
    }

    private void Update()
    {
        if(actualPrefab != null )
        {
            actualPrefab.GetComponent<HabitePrefab>().UpdatePrefab();
        }
    }

}
