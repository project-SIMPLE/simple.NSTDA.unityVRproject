using JetBrains.Annotations;
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
            //teamID = GetTeamIDAsInt();
            VU2ForestProtectionEventManager.Instance.OnTreeChangeState += SendTreeStatusToGAMA;

            VU2ForestProtectionEventManager.Instance.OnFinishQuestionnaire += SendCustomDataToGAMA;
            //VU2ForestProtectionEventManager.Instance.OnFireRemove += SendOtherMessageToGAMA;
            isSubscribed = true;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (isSubscribed)
        {
            VU2ForestProtectionEventManager.Instance.OnTreeChangeState -= SendTreeStatusToGAMA;

            VU2ForestProtectionEventManager.Instance.OnFinishQuestionnaire -= SendCustomDataToGAMA;
            //VU2ForestProtectionEventManager.Instance.OnFireRemove -= SendOtherMessageToGAMA;
            isSubscribed = false;
        }
    }
    ListOfGAMAMessage ListsOfMessage = null;
    GAMAMessage_edit2 message = null;
    protected override void ManageOtherMessages(string content)
    {
        //Debug.Log(content);
        //message = GAMAMessage_edit2.CreateFromJSON(content);
        ListsOfMessage = ListOfGAMAMessage.CreateFromJSON(content);

    }

    private void UpdateGameManager(GAMAMessage_edit2 m)
    {
        //Debug.Log("!!!!!!!!!!!!!!!!!!!GAMAMessage Format Head = __"+ m.Head.ToString()+"___");
        string jsonHead = m.Head;
        string jsonBody = m.Body;
        List<GAMATreesMessage> jsonTrees = m.Trees;
        List<GAMAThreatMessage> jsonThreats = m.Threats;
        Debug.Log("++++++++++++++++++++++++++++++   " + m.Head + " - " + m.Body + " - " + jsonTrees.Count + " - " + jsonThreats.Count);
        //Debug.Log(jsonContent.ToString());
        switch (jsonHead)
        {
            case "ReadyCheck":
                VU2ForestProtectionEventManager.Instance?.StatusUIControl(0);
                break;
            case "TutorialStart":
                VU2ForestProtectionEventManager.Instance?.TutorialStart();
                break;
            case "StartGame":
                VU2ForestProtectionEventManager.Instance?.StartStopGame(true);
                break;
            case "StopGame":
                VU2ForestProtectionEventManager.Instance?.StartStopGame(false);
                break;
            case "ReadID":
                VU2ForestProtectionEventManager.Instance?.GetPlayerID(GetTeamID());
                if (jsonTrees != null)
                {
                    VU2ForestProtectionEventManager.Instance?.RemoveOtherPlayerTree(m.Trees);
                }
                else
                {
                    Debug.Log("ERROR, jsonContent not found");
                }
                break;
            case "Update":
                //Debug.Log("Name: " + jsonContent[0].Name + "  | Grow State: "+ jsonContent[0].State);
                if (jsonBody == "GROW")
                {
                    VU2ForestProtectionEventManager.Instance?.UpdateTreeFromGAMA(jsonTrees);
                }
                else if (jsonBody == "GRASS")
                {
                    VU2ForestProtectionEventManager.Instance?.UpdateGrassOnTreeFromGAMA(jsonTrees);
                }

                if(jsonThreats.Count > 0 && jsonThreats != null)
                {
                    Debug.Log("Got Threats");
                    VU2ForestProtectionEventManager.Instance?.UpdateThreatsMessageFromGAMA(jsonThreats);
                }
                break;
            case "Rain":
                VU2ForestProtectionEventManager.Instance?.GetPlayerRainEffect(jsonBody);
                break;
            case "Background":
                VU2ForestProtectionEventManager.Instance?.UpdatePlayerBackground(jsonTrees);
                break;
            case "Announce":
                Debug.Log("Annouce STH");
                break;
        }

    }

    private void ReadListOfMessage(ListOfGAMAMessage lists)
    {
        List<GAMAMessage_edit2> mes = lists.ListOfMessage;
        Debug.Log("############################ Lists Size " + mes.Count);
        foreach (GAMAMessage_edit2 m in mes)
        {
            UpdateGameManager(m);

        }
    }

    protected override void OtherUpdate()
    {
        /*if (message != null)
        {
            UpdateGameManager(message);
            message = null;
        }*/
        if (ListsOfMessage != null)
        {
            ReadListOfMessage(ListsOfMessage);
            ListsOfMessage = null;
        }

    }

    private int GetTeamIDAsInt()
    {
        int id;

        if (Int32.TryParse(GetTeamID(), out id))
        {
            return id;
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

    //
    // -1 = stop growing
    //  0 = die
    //  1 = growing
    //
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

    public void SendPlayerReady()
    {
        Debug.Log("Team ID: " + GetTeamID() + "Ready");
        Dictionary<string, string> args = new Dictionary<string, string>
        {
            { "player_ID",GetTeamID()},
            { "Ready","true"}
        };

        try
        {
            ConnectionManager.Instance.SendExecutableAsk("PlayerID_Ready", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
    /*
     * Header = "Before" and "After"
     * message = answer in string
     * 
     * */
    public void SendCustomDataToGAMA(string header, string message)
    {
        Dictionary<string, string> args = new Dictionary<string, string>
        {
            {"PlayerID", GetTeamID() },
            {"Header", header },
            {"Message",message }
        };
        try
        {
            ConnectionManager.Instance.SendExecutableAsk("QuestionnaireData", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}




[System.Serializable]
public class ListOfGAMAMessage
{
    public List<GAMAMessage_edit2> ListOfMessage;

    public static ListOfGAMAMessage CreateFromJSON(string jsonString)
    {

        return JsonUtility.FromJson<ListOfGAMAMessage>(jsonString);
    }
}

[System.Serializable]
public class GAMAMessage_edit2
{
    public string Head;
    public string Body;
    public List<GAMATreesMessage> Trees;
    public List<GAMAThreatMessage> Threats;

    public static GAMAMessage_edit2 CreateFromJSON(string jsonString)
    {

        return JsonUtility.FromJson<GAMAMessage_edit2>(jsonString);
    }

}

[System.Serializable]
public class GAMATreesMessage
{
    public string PlayerID;
    public string Name;
    public string State;

    public static GAMATreesMessage CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GAMATreesMessage>(jsonString);
    }
}

[System.Serializable]
public class GAMAThreatMessage
{
    public string Name;
    public string x;
    public string y;
    public string z;
    public string PlayerID;

    public static GAMAThreatMessage CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GAMAThreatMessage>(jsonString);
    }
}
