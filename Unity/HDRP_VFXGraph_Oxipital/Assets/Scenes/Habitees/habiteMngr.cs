using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class habiteMngr : MonoBehaviour
{
    public GameObject airPrefab;
    public GameObject corpsePrefab;
    public GameObject waterPrefab;
    public GameObject talePrefab;
    public float sceneTransitionDuration;

    private GameObject actualPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LoadScene(0);
    }


    private void LoadScene(int sceneid)
    {
        if(actualPrefab)
        {
            actualPrefab.GetComponent<HabitePrefab>().transitionDuration = sceneTransitionDuration;
            actualPrefab.GetComponent<HabitePrefab>().ToBeDestroy();
        }
            

        if(sceneid == 0) // Air Scene
        {
            actualPrefab = Instantiate(airPrefab);
        }
        else if(sceneid == 1) // Corpse Scene
        {
            actualPrefab = Instantiate(corpsePrefab);
        }
        else if(sceneid == 2) // Water Scene
        {
            actualPrefab = Instantiate(waterPrefab);
        }
        else if(sceneid == 3) // Tale Scene
        {
            actualPrefab = Instantiate(talePrefab);
        }

        actualPrefab.GetComponent<HabitePrefab>().transitionDuration = sceneTransitionDuration;
    }

    public void LoadAir()
    {
        LoadScene(0);
    }

    public void LoadCorpse()
    {
        LoadScene(1);
    }

    public void LoadWater()
    {
        LoadScene(2);
    }

    public void LoadTale()
    {
        LoadScene(3);
    }

}
