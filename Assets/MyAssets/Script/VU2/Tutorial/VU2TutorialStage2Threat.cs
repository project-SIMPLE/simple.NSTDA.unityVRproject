using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2TutorialStage2Threat : VU2TutorialStage
{
    [SerializeField]
    private GameObject[] threatPrefab;


    public override void SetToInitialState()
    {
        cTask = totalTask;
        base.SetToInitialState();
    }
    public override void BeginStage()
    {
        SetToInitialState();
        SetUpTutorialObject();
    }

    private void SetUpTutorialObject()
    {
        foreach (GameObject o in threatPrefab)
        {
            o.SetActive(true);
        }
    }

    public void TaskComplete()
    {
        cTask--;
        if (cTask <= 0)
        {
            FinishStage();
        }
    }

    public override void FinishStage()
    {
        
        base.FinishStage();
    }
}


