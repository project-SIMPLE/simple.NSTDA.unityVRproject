using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2OfflineVersionLogic : MonoBehaviour,IVU2GameLogic
{
    [SerializeField]
    private GameObject FlamePrefab;
    [SerializeField]
    private GameObject FlamePrefab2;
    [SerializeField]
    private GameObject FlamePrefab2sub2;
    [SerializeField]
    private GameObject AlienPrefab;
    [SerializeField]
    private GameObject AlienPrefab2;

    private VU2EnvironmentController envController;
    private VU2PlayerInteractionControl pInteractControler;
    private VU2OfflineGameManager gameManager;

    /*[SerializeField]
    private QuestionnaireControl Q1Script;
    [SerializeField]
    private QuestionnaireControl Q2Script;*/
    [SerializeField]
    private int playerScore = 100;
    private string thisPlayerID;
    private List<GameObject> cPlayerTrees = new List<GameObject>();
    private int cBGStage;
    private int totalFire = 0;

    

    private void Start()
    {
        //VU2ForestProtectionEventManager.Instance.OnUpdateTreeState += LogicHandelOnSeedlingChangeState;
        //VU2ForestProtectionEventManager.Instance.OnTreeChangeState += LogicHandelOnSeedlingDeath;
        VU2ForestProtectionEventManager.Instance.OnScoreChange += UpdatePlayerScore;
    }
    private void OnDisable()
    {
        //VU2ForestProtectionEventManager.Instance.OnUpdateTreeState -= LogicHandelOnSeedlingChangeState;
        //VU2ForestProtectionEventManager.Instance.OnTreeChangeState -= LogicHandelOnSeedlingDeath;
        VU2ForestProtectionEventManager.Instance.OnScoreChange -= UpdatePlayerScore;
    }

    private void Awake()
    {
        envController = this.gameObject.GetComponent<VU2EnvironmentController>();
        pInteractControler = this.gameObject.GetComponent<VU2PlayerInteractionControl>();
        gameManager = this.gameObject.GetComponent<VU2OfflineGameManager>();
    }

    public void LogicStartStopGame(bool isRunning)
    {
        if (isRunning)
        {
            VU2ForestProtectionEventManager.Instance.StatusUIControl(-1);
            pInteractControler.EnableTools(true);
            pInteractControler.EnableLocomotion(true);
            cBGStage = 2;
            IsBGChange();
            playerScore = 100;
            gameManager.GameStart();
        }
        else
        {
            //gameManager.GameStop();
            VU2ForestProtectionEventManager.Instance.StatusUIControl(3);
            LogicUpdateRainEffect(false);

            LogicResetAllFire();
            VU2ForestProtectionEventManager.Instance.RemoveAllActiveObjectOnMap();
            VU2BGSoundManager.Instance.StopAllSFX();
        }
    }

    private void UpdatePlayerScore(int i)
    {
        playerScore += i;
        IsBGChange();
    }
   
    private void IsBGChange()
    {
        int newBGStage;
        if (playerScore <150)
        {
            newBGStage = 1;
        }
        else if (playerScore>=150 && playerScore<240)
        {
            newBGStage = 2;
        }
        else
        {
            newBGStage = 3;
        }

        if (newBGStage != cBGStage)
        {
            cBGStage = newBGStage;
            envController.ShowEnvironment(cBGStage);
        }
    }


    public void LogicCreateThreat(string name, Vector3 pos)
    {
        if(name == null) return;
        switch (name)
        {
            case "Flame1":
                totalFire++;
                LogicUpdateFireEffect(true);
                //Instantiate(FlamePrefab, pos, this.transform.rotation);
                VU2ObjectPoolManager.Instance?.SpawnObject(FlamePrefab, pos, this.transform.rotation);
                break;
            case "Flame2":
                totalFire++;
                LogicUpdateFireEffect(true);
                //Instantiate(FlamePrefab, pos, this.transform.rotation);
                VU2ObjectPoolManager.Instance?.SpawnObject(FlamePrefab2, pos, this.transform.rotation);
                break;
            case "Alien":
                //Instantiate(AlienPrefab, pos, this.transform.rotation);
                GameObject tmp = VU2ObjectPoolManager.Instance?.SpawnObject(AlienPrefab, pos, this.transform.rotation);
                if (tmp.TryGetComponent<Weed>(out Weed script))
                {
                    script.SetGrowFrom(Weed.GrowDir.Center, 3);
                }
                break;
            case "Alien2":
                GameObject tmp2 = VU2ObjectPoolManager.Instance?.SpawnObject(AlienPrefab2, pos, this.transform.rotation);
                if (tmp2.TryGetComponent<Weed>(out Weed script2))
                {
                    script2.SetGrowFrom(Weed.GrowDir.Center, 3);
                }
                break;
            case "Flame22":
                if (totalFire >= 7) return;
                totalFire++;
                LogicUpdateFireEffect(true);
                //Instantiate(FlamePrefab, pos, this.transform.rotation);
                VU2ObjectPoolManager.Instance?.SpawnObject(FlamePrefab2sub2, pos, this.transform.rotation);
                break;

        }
    }

    public GameObject LogicCreateWeed(Vector3 pos, Quaternion rot, int weedType)
    {
        if (weedType == 1)
        {
            return VU2ObjectPoolManager.Instance?.SpawnObject(AlienPrefab, pos, rot);
        }
        else
        {
            return VU2ObjectPoolManager.Instance?.SpawnObject(AlienPrefab2, pos, rot);
        }
    }

    public void LogicFireRemove()
    {
        totalFire--;
        Debug.Log(totalFire);
        if (totalFire <= 0)
        {
            totalFire = 0;
            LogicUpdateFireEffect(false);
        }
    }
    public void LogicResetAllFire()
    {
        totalFire = 0;
        VU2ForestProtectionEventManager.Instance.UpdateFireEffect(false);
    }


    public int LogicGetBGStage()
    {
        return cBGStage;
    }

    public void LogicGetPlayerRainEffect(string effect)
    {
        if (effect == "Start")
        {
            LogicUpdateRainEffect(true);
        }
        else if (effect == "Stop")
        {
            LogicUpdateRainEffect(false);
        }
    }

    public string LogicGetplayerScore()
    {
        return playerScore.ToString(); ;
    }

    public void LogicUpdateRainEffect(bool t)
    {
        VU2ForestProtectionEventManager.Instance.UpdateRainEffect(t);
    }
    public void LogicUpdateFireEffect(bool t)
    {
        VU2ForestProtectionEventManager.Instance.UpdateFireEffect(t);
    }


    /////////////////////////////////////////////////////////////////////// GAMA Message unuseable in offline/////////////////////////////////////////////////
    
    public void LogicSetPlayerID(string playerID)
    {
        throw new System.NotImplementedException();
    }
    public void LogicShowPlayerScore(List<GAMATreesMessage> players)
    {
        throw new System.NotImplementedException();
    }

    
    public void LogicUpdatePlayerBackground(List<GAMATreesMessage> message)
    {
        throw new System.NotImplementedException();
    }

   

    public void LogicUpdateThreatsMessageFromGAMA(List<GAMAThreatMessage> threats)
    {
        throw new System.NotImplementedException();
    }

    public void LogicUpdateTreeFromGAMA(List<GAMATreesMessage> tree)
    {
        throw new System.NotImplementedException();
    }
    public void LogicCollectQuestionnaireData(string qType, string data)
    {
        throw new System.NotImplementedException();
    }
    public void LogicUpdateGrassOnTreeFromGAMA(List<GAMATreesMessage> tree)
    {
        throw new System.NotImplementedException();
    }
    
    public void LogicHandleRemoveThreatsMessageFromGAMA(List<GAMAThreatMessage> threats)
    {
        throw new System.NotImplementedException();
    }

    public void LogicRemoveOtherPlayerTree(List<GAMATreesMessage> tree)
    {
        throw new System.NotImplementedException();
    }
    public void LogicResendQuestionnaireData(string type)
    {
        throw new System.NotImplementedException();
    }
    public void LogicGAMAReceieveQuestionnaireData()
    {
        throw new System.NotImplementedException();
    }
}
