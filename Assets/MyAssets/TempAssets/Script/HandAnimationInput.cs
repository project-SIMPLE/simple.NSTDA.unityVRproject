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
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        string tags = args.interactableObject.transform.tag;
        if(tags == "Tools")
        {
            if(args.interactableObject.transform.name == "CrossbowVR")
            {
                SetHandAnimation(3);
            }
            else
            {
                SetHandAnimation(2);
            }
        }
        else
        {
            SetHandAnimation(1);
        }
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        SetHandAnimation(0);
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
