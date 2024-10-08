using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GAMAConnector : SimulationManager
{

    GAMAMessage_edit message = null;
    protected override void ManageOtherMessages(string content)
    {
        // Debug.Log("content " + content);
        message = GAMAMessage_edit.CreateFromJSON(content);
         
    }

    protected override void OtherUpdate()
    {

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
    public string message;

    public static GAMAMessage_edit CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GAMAMessage_edit>(jsonString);
    }

}