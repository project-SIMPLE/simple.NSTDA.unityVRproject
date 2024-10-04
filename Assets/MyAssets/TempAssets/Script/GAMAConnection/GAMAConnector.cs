using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GAMAConnector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendSeedInfoToGAMA(int seedID)
    {
        Dictionary<string, string> args = new Dictionary<string,string>() { 
            { "SeedID",seedID.ToString()} 
        };

        try
        {
            //ConnectionManager.Instance.SendExecutableAsk("", args);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
