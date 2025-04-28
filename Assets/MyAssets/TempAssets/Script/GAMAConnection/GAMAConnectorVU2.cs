using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMAConnectorVU2 : SimulationManager
{
    private bool isSubscribed = false;
    public override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("Register Action!!");

        if (!isSubscribed) 
        {
            VU2Zone1EventManager.Instance.OnPlayerHitFruitOnTree += SendFruitPosToGAMA;
            VU2Zone1EventManager.Instance.OnPutFruitIntoBucket += TellGAMAToRemoveFruit;
            VU2Zone1EventManager.Instance.OnLoadSeedToNextZone += TellGAMAToCountFruit;
            isSubscribed = true;
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (isSubscribed)
        {
            VU2Zone1EventManager.Instance.OnPlayerHitFruitOnTree -= SendFruitPosToGAMA;
            VU2Zone1EventManager.Instance.OnPutFruitIntoBucket -= TellGAMAToRemoveFruit;
            VU2Zone1EventManager.Instance.OnLoadSeedToNextZone -= TellGAMAToCountFruit;
            isSubscribed = false;
        }
    }

    GAMAMessage_edit message = null;
    protected override void ManageOtherMessages(string content)
    {
        //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!content " + content);
        message = GAMAMessage_edit.CreateFromJSON(content);
        
    }

    private void UpdateGameManager(GAMAMessage_edit m)
    {
        //Debug.Log("!!!!!!!!!!!!!!!!!!!GAMAMessage Format Head = __"+ m.Head.ToString()+"___");
        string jsonHead = m.Head;
        string jsonBody = m.Body;
        

        switch (jsonHead)
        {
            /*case "Start":
                Debug.Log("JSON HEAD START");
                OnlineModeGameManager.Instance?.GameStart();
                break;
            case "Stop":
                OnlineModeGameManager.Instance?.GameStop();
                break;
            case "Tutorial":
                Debug.Log("Tutorial START");
                OnlineModeGameManager.Instance?.StartTutorial();
                break;*/
        }
       
    }

    protected override void OtherUpdate()
    {
        if (message != null)
        {  
            UpdateGameManager(message);
            message = null;
        }
    }

    public void SendTutorialFinishInfo()
    {
        Debug.Log("Team ID: "+ GetTeamID() +"Finish Tutorial");
        Dictionary<string, string> args = new Dictionary<string, string>
        {
            { "player_ID",GetTeamID()},
            { "tutorial_status","true"}
        };

        try
        {
            ConnectionManager.Instance.SendExecutableAsk("tutorial_finish", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
    public void SendSeedInfoToGAMA(int seedID)
    {
        Debug.Log("Collect!!! :" + seedID);
        Dictionary<string, string> args = new Dictionary<string, string> {
         {"player_ID", GetTeamID() },
         {"tree_ID",  seedID.ToString()}
        };
        try
        {
            ConnectionManager.Instance.SendExecutableAsk("collect_seeds", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private string GetTeamID()
    {
        return ConnectionManager.Instance.getUseMiddleware() ? ConnectionManager.Instance.GetConnectionId() : ("\"" + ConnectionManager.Instance.GetConnectionId() + "\""); ;
    }


    public void SendFruitPosToGAMA(string treeName, int fruitIndex, int fruitID, Vector3 Pos)
    {
        Debug.Log("Team ID: " + GetTeamID() + "Hit Fruit on Tree " + treeName + " At Pos index: " + fruitIndex + " Then create Fruit: " + fruitID + " At Pos: " + Pos.ToString());
        Dictionary<string, string> args = new Dictionary<string, string>
        {
            { "player_ID",GetTeamID()},
            { "tree_Name",treeName},
            { "fruit_Index", fruitIndex.ToString()},
            { "fruit_ID", fruitID.ToString()},
            { "fruit_Pos", Pos.ToString()}
        };

        try
        {
            ConnectionManager.Instance.SendExecutableAsk("PlayerHitFruitOnTree", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    public void TellGAMAToRemoveFruit(string fruitName)
    {
        Dictionary<string, string> args = new Dictionary<string, string>
        {
            { "fruit_Name", fruitName }
        };

        try
        {
            ConnectionManager.Instance.SendExecutableAsk("PlayerCollectFruit", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void TellGAMAToCountFruit()
    {
        Dictionary<string, string> args = new Dictionary<string, string>
        {
            { "status", "true"}
        };


        try
        {
            ConnectionManager.Instance.SendExecutableAsk("CountScore", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

}
/*
[System.Serializable]
public class GAMAMessage_edit
{
    public string Head;
    public string Body;

    public static GAMAMessage_edit CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GAMAMessage_edit>(jsonString);
    }

}*/