using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class SeedCollectionOfflineEventManager : MonoBehaviour
{
    [SerializeField]
    private bool offlineMode = true;

    public static SeedCollectionOfflineEventManager instance { get; private set;}
    [SerializeField]
    private int[] fruitListScore = { -10, 0, 0,0,0,0,0,0,0,-10,0,0 };
    [SerializeField]
    private int[] alienListScore = { 0, 0, 0 };

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
    private void OnDisable()
    {
        if (PlayerPrefs.HasKey("SeedCollectionStage"))
        {
            PlayerPrefs.DeleteKey("SeedCollectionStage");
        }
        //PlayerPrefs.DeleteKey
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
        //stage.SetupTreeTimelineInfo(stageIndex);
        EnablePlayMode();
        SetTutorialStatus(true);
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
        UpdateStage(stageIndex);
        
        GameStart();
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
    [SerializeField]
    private Transform[] teleportPoints;
    private void MovePlayer(int i)
    {
        /*if (player != null)
        {
            player.transform.position = new Vector3(0,0,0);
        }*/
        Vector3 newPos;
        switch (i)
        {
            case 1:
                newPos = teleportPoints[0].position;
                break;
            case 2:
                newPos = teleportPoints[1].position;
                break;
            default:
                newPos = new Vector3(0, 0, 0);
                break;
        }
        player.transform.position = newPos;

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
        if(id == 0)
        {
            TutorialSeedCollect();
        }
        else
        {
            if (id < 0)
            {
                int tmp = math.abs(id);
                fruitListScore[tmp - 1]++;
                OnSeedCollected(tmp, fruitListScore[tmp - 1]);
                CollectAlienFruit(tmp);
            }
            else
            {
                fruitListScore[id - 1]++;
                OnSeedCollected(id, fruitListScore[id - 1]);
            }
            
        }

        /*if (id < 0 || fruitListScore[id - 1] <-1) return;
        fruitListScore[id-1]++;
        OnSeedCollected(id, fruitListScore[id - 1]);*/
    }
    /**
     * 
     * -2
     * -3
     * -9
     * 
     */
    private void CollectAlienFruit(int id)
    {
        switch (id)
        {
            case 2:
                alienListScore[0]++;
                break;
            case 3:
                alienListScore[1]++;
                break;
            case 9:
                alienListScore[2]++;
                break;
        }
    }
    public int[] GetAlienLists()
    {
        return alienListScore;
    }

    public event Action OnTutorialSeedCollect;
    public void TutorialSeedCollect()
    {
        OnTutorialSeedCollect?.Invoke();
    }

    public event Action OnTutorialStart;
    public event Action<int, int[]> OnTutorialFinish;
    public void SetTutorialStatus(bool s)
    {
        
        if (s)
        {
            
            MovePlayer(2);
            OnTutorialStart?.Invoke();
        }
        else{
            MovePlayer(1);
            OnTutorialFinish?.Invoke(stageIndex, fruitListScore);
        }
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
    public event Action OnStageFinish;
    public void TimerFinish()
    {
        DisablePlayMode();
        MovePlayer(1);
        OnStageFinish?.Invoke();

    }
    public event Action<int> OnMoveToNextStage;
    public void ContinueToNextStage()
    {
        stage.ClearAllTreeFromStage();
        stageIndex++;
        if (IsGameFinish())
        {
            Debug.Log("Finish all stage");
            OnAllCompletedAllStage?.Invoke(fruitListScore);
            
        }
        else
        {
            //UpdateStage(stageIndex);
            EnablePlayMode();
            OnMoveToNextStage?.Invoke(stageIndex);
            
            SetTutorialStatus(true);
        }
    }
    public void EarlyFinishGame()
    {
        Debug.Log("Finish before stage 6");
        OnAllCompletedAllStage?.Invoke(fruitListScore);
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
    public void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }
}
