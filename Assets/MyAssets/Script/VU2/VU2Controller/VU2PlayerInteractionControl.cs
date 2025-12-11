using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI.BodyUI;

public class VU2PlayerInteractionControl : MonoBehaviour
{
    [SerializeField]
    private GameObject tools;
    [SerializeField]
    private GameObject handMenu;
    [SerializeField]
    private GameObject locomotion;

    private void Start()
    {
        EnableTools(false);
        EnableLocomotion(false);

        VU2ForestProtectionEventManager.Instance.OnGameStop += GameStop;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnGameStop -= GameStop;
    }

    private void GameStop()
    {
        EnableTools(false);
        EnableLocomotion(false);
    }

    public void EnableTools(bool t)
    {
        handMenu.SetActive(t);
        tools.SetActive(t);
    }
    public void EnableLocomotion(bool t)
    {
        locomotion.SetActive(t);
    }
}
