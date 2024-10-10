using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMAConnector : SimulationManager
{

    public override void OnEnable()
    {
        base.OnEnable();
        OnlineModeGameManager.Instance.OnSeedCollected += SendSeedInfoToGAMA;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        OnlineModeGameManager.Instance.OnSeedCollected -= SendSeedInfoToGAMA;

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
            case "Start":
                Debug.Log("JSON HEAD START");
                OnlineModeGameManager.Instance?.GameStart();
                break;
            case "Stop":
                OnlineModeGameManager.Instance?.GameStop();
                break;
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


    public void SendSeedInfoToGAMA(int seedID)
    {

        Dictionary<string, string> args = new Dictionary<string, string> {
         {"player_ID", GetTeamID() },
         {"tree_ID",  seedID.ToString()}
        };

        /*Dictionary<string, string> args = new Dictionary<string,string>() { 
            { "SeedID",seedID.ToString()} 
        };*/

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
}

[System.Serializable]
public class GAMAMessage_edit
{
    public string Head;
    public string Body;

    public static GAMAMessage_edit CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GAMAMessage_edit>(jsonString);
    }

}