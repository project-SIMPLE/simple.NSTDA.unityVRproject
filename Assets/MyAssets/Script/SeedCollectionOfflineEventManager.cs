using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SeedCollectionOfflineEventManager : MonoBehaviour
{
    [SerializeField]
    private bool offlineMode = true;

    public static SeedCollectionOfflineEventManager instance { get; private set;}
    [SerializeField]
    private int[] fruitListScore = { -10, 0, 0,0,0,0,0,0,0,-10,0,0 };

    [SerializeField]
    private GameObject LocomotionModule;
    [SerializeField]
    private GameObject BeltTool;
    [SerializeField]
    private GameObject ToolsMenu;

    [SerializeField]
    private int stageIndex = 1;

    [SerializeField]
    //private GameObject[] stages;
    private StageSetup stage;

    [SerializeField]
    private GameObject player;

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
        
        //UpdateStage(stageIndex-1);
        if (PlayerPrefs.HasKey("SeedCollectionStage"))
        {
            stageIndex = PlayerPrefs.GetInt("SeedCollectionStage");
        }
        else
        {
            stageIndex = 1;
        }
        stage.SetupTreeTimelineInfo(stageIndex);
    }

    private void UpdateStage(int index)
    {
        /*if(index > 0)
        {
            stages[index-1].SetActive(false);
        }
        stages[index].SetActive(true);*/
        stage.SetupTreeTimelineInfo(index);
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
    private void MovePlayer()
    {
        if (player != null)
        {
            player.transform.position = new Vector3(0,0,0);
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

        if (id < 0 || fruitListScore[id - 1] <-1) return;
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
    public event Action<int[]> OnAllCompletedAllStage;
    public event Action<int,bool> OnTimerFinish;
    public void TimerFinish()
    {
        stage.ClearAllTreeFromStage();
        DisablePlayMode();
        MovePlayer();
        stageIndex++;
        if (IsGameFinish())
        {
            OnAllCompletedAllStage?.Invoke(fruitListScore);
            OnTimerFinish?.Invoke(stageIndex, true);
        }
        else
        {
            UpdateStage(stageIndex);
            OnTimerFinish?.Invoke(stageIndex, false);
        }

        
    }

    private bool IsGameFinish()
    {
        if(stageIndex > 6) {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public event Action<int> OnGameStart;
    public void GameStart()
    {
        OnGameStart?.Invoke(stageIndex);
        EnablePlayMode();
    }
}
