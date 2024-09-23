using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit; 
using UnityEngine.InputSystem;



public class SimulationManagerInteraction : SimulationManager
{
    protected override void HoverEnterInteraction(HoverEnterEventArgs ev)
    {

    }

    protected override void HoverExitInteraction(HoverExitEventArgs ev)
    {
        
    }

    protected override void SelectInteraction(SelectEnterEventArgs ev)
    {

        if (remainingTime <= 0.0)
        {
            GameObject grabbedObject = ev.interactableObject.transform.gameObject;

            if (("fire").Equals(grabbedObject.tag))
            {
                Dictionary<string, string> args = new Dictionary<string, string> {
                         {"plot_", grabbedObject.name }
                    };
                ConnectionManager.Instance.SendExecutableAsk("extinguish", args);
               
                remainingTime = timeWithoutInteraction;
            }

        }

    }


//Defines what happens when the main button (of the right controller) is trigger 
protected override void TriggerMainButton()
    {
       
    }

    //Defines what happens when a non-standard message is received from GAMA. 
    protected override void ManageOtherMessages(string content)
    {

    }

    //Processes additional information contained in WorldJSONInfo - sent by GAMA at each simulation step.  
    protected override void ManageOtherInformation()
    {

    }


    //Adds extra actions to be performed for each new frame.
    protected override void OtherUpdate()
    {

    }

   
}