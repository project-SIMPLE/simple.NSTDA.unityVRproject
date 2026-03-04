using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2OnlineVersionLogic : MonoBehaviour, IVU2GameLogic
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

    //[SerializeField]
    //private GameObject PauseUI;
    private VU2EnvironmentController envController;
    private VU2PlayerInteractionControl pInteractControler;

    [SerializeField]
    private QuestionnaireControl Q1Script;
    [SerializeField]
    private QuestionnaireControl Q2Script;

    private string playerScore = "";
    private string thisPlayerID;
    private List<GameObject> cPlayerTrees = new List<GameObject>();
    private int cBGStage;
    private int totalFire = 0;
    [SerializeField]
    private bool isGAMAReceieveData;


    private void Awake()
    {
        envController = this.gameObject.GetComponent<VU2EnvironmentController>();
        pInteractControler = this.gameObject.GetComponent<VU2PlayerInteractionControl>();
    }

    public void LogicStartStopGame(bool isRunning)
    {
        if (isRunning)
        {
            VU2ForestProtectionEventManager.Instance.StatusUIControl(-1);
            pInteractControler.EnableTools(true);
            pInteractControler.EnableLocomotion(true);
            //Time.timeScale = 1f;
            //PauseUI.SetActive(false);
        }
        else
        {
            VU2ForestProtectionEventManager.Instance.StatusUIControl(3);
            LogicUpdateRainEffect(false);

            LogicResetAllFire();
            VU2ForestProtectionEventManager.Instance.RemoveAllActiveObjectOnMap();
            VU2BGSoundManager.Instance.StopAllSFX();
            //PauseUI.SetActive(true);
            //Time.timeScale = 0f;
        }
    }
    public void LogicShowPlayerScore(List<GAMATreesMessage> players)
    {
        foreach (GAMATreesMessage p in players)
        {
            string cID = p.PlayerID;
            if (cID == thisPlayerID)
            {
                Debug.Log($"Player ID: {p.PlayerID} got score = {p.Name}");
                playerScore = p.Name;



            }
        }
    }
    public string LogicGetplayerScore()
    {
        return playerScore;
    }
    public void LogicSetPlayerID(string playerID)
    {
        thisPlayerID = playerID;
    }
    public void LogicRemoveOtherPlayerTree(List<GAMATreesMessage> tree)
    {
        foreach (GAMATreesMessage t in tree)
        {
            string cID = t.PlayerID;
            //Debug.Log("#################  cID "+ cID);
            //Debug.Log("#################  this ID " + thisPlayerID);
            if (cID == null)
            {
                Debug.Log("PlayerID error");
                return;
            }
            if (cID != thisPlayerID)
            {
                GameObject.Find(t.Name)?.gameObject.SetActive(false);
                //Destroy(GameObject.Find(t.Name)?.gameObject);
            }
            else
            {
                cPlayerTrees.Add(GameObject.Find(t.Name)?.gameObject);
            }
        }
    }
    public void LogicUpdateTreeFromGAMA(List<GAMATreesMessage> tree)
    {
        foreach (GAMATreesMessage t in tree)
        {
            string cID = t.PlayerID;
            if (cID == null)
            {
                Debug.Log("PlayerID error");
                return;
            }
            if (cID == thisPlayerID)
            {
                //Debug.Log("Player : "+ cID);
                //GameObject.Find(t.Name)?.GetComponent<Seeding>()?.ChangeGrowState(Int32.Parse(t.State));
                VU2ForestProtectionEventManager.Instance.UpdatePlayerTreeFromGAMA(t.Name, Int32.Parse(t.State));
                //GameObject.Find(t.Name).gameObject.SetActive(false);
            }
        }
    }
    public void LogicUpdateGrassOnTreeFromGAMA(List<GAMATreesMessage> tree)
    {
        foreach (GAMATreesMessage t in tree)
        {
            string cID = t.PlayerID;
            if (cID == null)
            {
                Debug.Log("PlayerID error");
                return;
            }
            if (cID == thisPlayerID)
            {
                VU2ForestProtectionEventManager.Instance.UpdatePlayerGrassOnTreeFromGAMA(t.Name);
            }
        }
    }
    public void LogicUpdateThreatsMessageFromGAMA(List<GAMAThreatMessage> threats)
    {
        foreach (GAMAThreatMessage t in threats)
        {
            if (t.PlayerID != thisPlayerID) continue;
            //Debug.Log(t.Name);
            float GamaX;
            float GamaY;
            float GamaZ;
            if (float.TryParse(t.x, out GamaX) && float.TryParse(t.y, out GamaY) &&
                float.TryParse(t.z, out GamaZ))
            {

                Vector3 tmp = new Vector3(GamaX, GamaY, GamaZ);
                LogicCreateThreat(t.Name, tmp);
            }
            else
            {
                Debug.Log("Error : Cannot convert to Float Number");
            }

        }
    }
    public void LogicGetPlayerRainEffect(string effect)
    {
        Debug.Log("+++++++++++++ Rain : " + effect);

        if (effect == "Start")
        {
            LogicUpdateRainEffect(true);
        }
        else if (effect == "Stop")
        {
            LogicUpdateRainEffect(false);
        }
    }
    public void LogicUpdateRainEffect(bool t)
    {
        VU2ForestProtectionEventManager.Instance.UpdateRainEffect(t);
    }
    public void LogicUpdatePlayerBackground(List<GAMATreesMessage> message)
    {
        foreach (GAMATreesMessage t in message)
        {
            string cID = t.PlayerID;
            if (cID == null)
            {
                Debug.Log("PlayerID error");
                return;
            }
            if (cID == thisPlayerID)
            {

                int stage;
                if (int.TryParse(t.Name, out stage))
                {
                    Debug.Log("Change background to stage: " + t.Name);
                    cBGStage = stage;
                    envController.ShowEnvironment(stage);
                }
                else
                {
                    Debug.Log("Stage Name error");
                }
            }
        }
    }
    public int LogicGetBGStage()
    {
        return cBGStage;
    }
    public void LogicUpdateFireEffect(bool t)
    {
        VU2ForestProtectionEventManager.Instance.UpdateFireEffect(t);
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

    public void LogicCreateThreat(string name, Vector3 pos)
    {
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

    public void LogicHandleRemoveThreatsMessageFromGAMA(List<GAMAThreatMessage> threats)
    {
        foreach (GAMAThreatMessage t in threats)
        {
            if (t.PlayerID != thisPlayerID) continue;
            GlobalThreat tmp = GlobalThreat.Non;
            switch (t.Name)
            {
                case "Fire":
                    tmp = GlobalThreat.Fire;
                    break;
                case "Aliens":
                    tmp = GlobalThreat.AlienPlant;
                    break;
                case "Grasses":

                    tmp = GlobalThreat.Grasses;
                    break;
            }

            VU2ForestProtectionEventManager.Instance.RemoveGlobalThreat(tmp);

        }
    }

    public void LogicCollectQuestionnaireData(string qType, string data)
    {
        Debug.Log($"Questionnaire from :{qType} with Data :{data}");
        VU2ForestProtectionEventManager.Instance.FinishQuestionnaire(qType, data);
        isGAMAReceieveData = false;
        StartCoroutine(RepeatSendingQuestionnaireData(qType));
    }
    public void LogicGAMAReceieveQuestionnaireData()
    {
        isGAMAReceieveData = true;

    }
    public void LogicResendQuestionnaireData(string type)
    {
        if (type == null) return;
        switch (type)
        {
            case "before":
                Q1Script.ResendQuestionnaireData();
                break;
            case "after":
                Q2Script.ResendQuestionnaireData();
                break;
        }
    }
    IEnumerator RepeatSendingQuestionnaireData(string type)
    {
        while (!isGAMAReceieveData)
        {
            Debug.Log("ReSending Questionnaire Data");
            LogicResendQuestionnaireData(type);
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("GAMA has replies message");
    }

}
