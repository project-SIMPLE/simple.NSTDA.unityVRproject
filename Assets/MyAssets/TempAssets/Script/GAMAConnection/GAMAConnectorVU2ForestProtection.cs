using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GAMAConnectorVU2ForestProtection : SimulationManager
{
    private bool isSubscribed = false;
    [SerializeField]
    private int teamID; 

    

    public override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("Register Action!!");

        if (!isSubscribed)
        {
            teamID = GetTeamIDAsInt();
            VU2ForestProtectionEventManager.Instance.OnTreeChangeState += SendTreeStatusToGAMA;
            isSubscribed = true;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (isSubscribed)
        {
            VU2ForestProtectionEventManager.Instance.OnTreeChangeState -= SendTreeStatusToGAMA;
            isSubscribed = false;
        }
    }

    GAMAMessage_edit2 message = null;
    protected override void ManageOtherMessages(string content)
    {
        //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!content " + content);
        message = GAMAMessage_edit2.CreateFromJSON(content);

    }

    private void UpdateGameManager(GAMAMessage_edit2 m)
    {
        //Debug.Log("!!!!!!!!!!!!!!!!!!!GAMAMessage Format Head = __"+ m.Head.ToString()+"___");
        string jsonHead = m.Head;
        string jsonBody = m.Body;
        string jsonContent = m.Content;

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

    private int GetTeamIDAsInt()
    {
        int id;

        if(Int32.TryParse(GetTeamID(), out id))
        {

        }
        else
        {
            id = 0;
        }

        return id;
    }

    private string GetTeamID()
    {
        return ConnectionManager.Instance.getUseMiddleware() ? ConnectionManager.Instance.GetConnectionId() : ("\"" + ConnectionManager.Instance.GetConnectionId() + "\""); ;
    }

    public void SendTreeStatusToGAMA(string treeName, string status)
    {
        Dictionary<string, string> args = new Dictionary<string, string>
        {
            {"tree_Name", treeName },
            {"status",status }
        };

        try
        {
            ConnectionManager.Instance.SendExecutableAsk("ChangeTreeState", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}

[System.Serializable]
public class GAMAMessage_edit2
{
    public string Head;
    public string Body;
    public string Content;

    public static GAMAMessage_edit2 CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GAMAMessage_edit2>(jsonString);
    }

}
