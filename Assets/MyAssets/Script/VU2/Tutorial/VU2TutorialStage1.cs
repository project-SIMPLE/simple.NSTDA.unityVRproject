using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2TutorialStage1 : VU2TutorialStage
{
    [SerializeField]
    private GameObject plantableSeeding;
    [SerializeField]
    private Transform[] seedingSpawnPoint;

    private List<GameObject> seedlingLists;


    public override void SetToInitialState()
    {
        cTask = totalTask;
        if (seedlingLists == null)
        {
            seedlingLists = new List<GameObject>();
        }
        else
        {
            seedlingLists.Clear();
        }
    }

    public override void BeginStage()
    {
        SetToInitialState();
        SetUpTutorialObject();
    }

    private void SetUpTutorialObject()
    {
        seedlingLists = new List<GameObject>();
        foreach (Transform t in seedingSpawnPoint)
        {
            GameObject tmp = Instantiate(plantableSeeding, t.position, t.rotation);
            seedlingLists.Add(tmp);
        }
    }

    public void TaskComplete()
    {
        cTask--;
        if(cTask <= 0)
        {
            FinishStage();
        }
    }

    public override void FinishStage()
    {
        RemoveSeedling();
        base.FinishStage();
    }

    private void RemoveSeedling()
    {
        foreach(GameObject s in seedlingLists)
        {
            Destroy(s);
        }
    }
}
