using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestbedManager : MonoBehaviour
{
    [SerializeField]
    private bool offlineMode = true;

    public static TestbedManager instance { get; private set;}
    [SerializeField]
    private int[] fruitListScore = { 0, 0, 0 };

    [SerializeField]
    private GameObject LocomotionModule;
    [SerializeField]
    private GameObject BeltTool;
    [SerializeField]
    private GameObject ToolsMenu;

    [SerializeField]
    private int stageIndex = 1;

    [SerializeField]
    private GameObject[] stages;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateStage(stageIndex-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateStage(int index)
    {
        if(index > 0)
        {
            stages[index-1].SetActive(false);
        }
        stages[index].SetActive(true);
    }
    public void StartGame()
    {
        if(offlineMode)
        {
            GameStart();
        }
    }

    public void EnablePlayMode()
    {
        LocomotionModule?.SetActive(true);
        BeltTool?.SetActive(true);
        if (ToolsMenu != null)
        {
            ToolsMenu?.SetActive(true);
        }
        
    }
    public void DisablePlayMode()
    {
        LocomotionModule?.SetActive(false);
        BeltTool?.SetActive(false);
        if (ToolsMenu != null)
        {
            ToolsMenu.SetActive(false);
        }
    }

    public event Action<int,int> OnSeedCollected;
    public void SeedCollected(int id)
    {
        /*if(OnSeedCollected != null)
        {
            OnSeedCollected(id);
        }*/
        fruitListScore[id-1]++;
        OnSeedCollected(id, fruitListScore[id - 1]);
    }
    public event Action OnResetSeedPosition;
    public void ResetSeedPosition()
    {
        if(OnResetSeedPosition != null)
        {
            OnResetSeedPosition();
        }
    }

    public event Action<int,bool> OnTimerFinish;
    public void TimerFinish()
    {
        
        DisablePlayMode();
        stageIndex++;
        if (IsGameFinish())
        {
            if (OnTimerFinish != null)
            {
                OnTimerFinish(stageIndex,true);
            }
        }
        else
        {
            UpdateStage(stageIndex-1);
            if (OnTimerFinish != null)
            {
                OnTimerFinish(stageIndex,false);
            }
        }

        
    }

    private bool IsGameFinish()
    {
        if(stageIndex-1 == stages.Length) {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public event Action OnGameStart;
    public void GameStart()
    {
        if(OnGameStart != null)
        {
            OnGameStart();
        }
        EnablePlayMode();
    }
}
