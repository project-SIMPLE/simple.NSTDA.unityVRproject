using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VU2ServiceInstaller : MonoBehaviour
{
    [SerializeField]
    private IVU2EventManager _eventManager;
    [SerializeField]
    private IVU2GAMACommandLogic _commandLogic;
    [SerializeField]
    private IVU2QuestionnaireManager _questionnaireManager;

    private VU2ServiceLocator sl;

    private void Awake()
    {
        sl = VU2ServiceLocator.Instance;

    }

    private void InstallService()
    {
        if (_eventManager != null)
        {
            sl.Register(IVU2EventManager, _eventManager);
        }
}
