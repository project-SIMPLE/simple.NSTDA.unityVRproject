using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimationInput : MonoBehaviour
{
    
    private Animator anim;
    int Idle = Animator.StringToHash("Idle");
    int GrabLarge = Animator.StringToHash("GrabLarge");
    int GrabStickUp = Animator.StringToHash("GrabStickUp");
    int GrabGun = Animator.StringToHash("GrabStickFront");

    public InputActionProperty GrabInputAction;
    public InputActionProperty TriggerInputAction;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    bool isGrab;
    bool isTrigger;
    void Update()
    {
        
        isGrab = GrabInputAction.action.IsPressed();
        isTrigger = TriggerInputAction.action.IsPressed();

        string cItem = GetItemOnHand();


        if (isGrab)
        {
            if (cItem == "No" || cItem == "Other")
            {
                SetHandAnimation(1);
            }
            else if (cItem == "Tools")
            {
                SetHandAnimation(2);
            }
            if(cItem == "Gun")
            {
                SetHandAnimation(3);
            }
        }
        else if (isTrigger)
        {
            if (isGrab && cItem == "Gun")
            {
                SetHandAnimation(2);
            }
        }
        else
        {
            SetHandAnimation(0);
        }


    }
    private string itemOnHand = "No";

    private string GetItemOnHand()
    {
        return itemOnHand;
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        string tags = args.interactableObject.transform.tag;
        //Debug.Log(tags +" Name: "+ args.interactableObject.transform.name);
        if(tags == "Tools")
        {
            if(args.interactableObject.transform.name == "CrossbowVR")
            {
                //SetHandAnimation(3);
                itemOnHand = "Gun";
            }
            else
            {
                itemOnHand = "Tools";
                //SetHandAnimation(2);
            }
        }
        else
        {
            itemOnHand = "Other";
            //SetHandAnimation(1);
        }
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        //SetHandAnimation(0);
        itemOnHand = "No";
    }
    public void FireGun()
    {

    }
    public void SetHandAnimation(int id)
    {
        switch (id)
        {
            case 0:
                anim.SetTrigger(Idle);
                break;
            case 1:
                anim.SetTrigger(GrabLarge);
                break;
            case 2:
                anim.SetTrigger(GrabStickUp);
                break;
            case 3:
                    anim.SetTrigger(GrabGun);
                break;
            default:
                anim.SetTrigger(Idle);
                break;
        }
    }
}
