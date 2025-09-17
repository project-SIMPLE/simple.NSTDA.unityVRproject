using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VU2TutorialStage : MonoBehaviour
{
    public UnityEvent OnStageFinish;

    [SerializeField]
    protected int totalTask;
    [SerializeField]
    protected int cTask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void SetToInitialState()
    {

    }

    public virtual void BeginStage()
    {

    }

    public virtual void FinishStage() 
    {
        SetToInitialState();
        OnStageFinish?.Invoke();
        this.gameObject.SetActive(false);
    }
}
