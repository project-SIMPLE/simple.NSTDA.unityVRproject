using System;
using System.Collections;
using System.Collections.Generic;
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
            isSubscribed = true;
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (isSubscribed)
        {
            
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
            { "tree_Name", treeName },
            {"status",status }
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
}
