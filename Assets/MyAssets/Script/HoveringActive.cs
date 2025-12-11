using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

public class HoveringActive : MonoBehaviour
{
    HoverEnterEvent HEEnter = new HoverEnterEvent();
    HoverExitEvent HEExit = new HoverExitEvent();

    // Start is called before the first frame update
    void Start()
    {
        HEEnter.AddListener(OnHoverEntered);
        HEExit.AddListener(OnHoverExited);
    }

    private void OnDestroy()
    {
        HEEnter?.RemoveListener(OnHoverEntered);
        HEExit?.RemoveListener(OnHoverExited);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"{args.interactorObject} hovered over {args.interactableObject}", this);
    }
    public void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"{args.interactorObject} stopped hovering over {args.interactableObject}", this);
    }
}
