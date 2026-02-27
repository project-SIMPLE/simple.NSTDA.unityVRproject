using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2OnlineVersionEventManager : MonoBehaviour, IVU2EventManager, IVU2QuestionnaireManager
{
    public event Action<string, int> OnUpdateTreeState;
    public event Action<string> OnUpdateGrassOnTree;
    public event Action OnTutorialStart;
    public event Action<bool> OnUpdateRainEffect;
    public event Action<bool> OnUpdateFireEffect;
    public event Action OnFireRemove;
    public event Action<int> OnUpdateStateUI;
    public event Action OnGameStop;
    public event Action<string, string> OnFinishQuestionnaire;

    event Action<IVU2EventManager.GlobalThreat> IVU2EventManager.OnRemoveGlobalThreat
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    public void CollectQuestionnaireData(string qType, string data)
    {
        throw new NotImplementedException();
    }

    public void CreateThreat(string name, Vector3 pos)
    {
        throw new NotImplementedException();
    }

    public GameObject CreateWeed(Vector3 pos, Quaternion rot, int weedType)
    {
        throw new NotImplementedException();
    }

    public void FinishQuestionnaire(string qType, string data)
    {
        throw new NotImplementedException();
    }

    public void FireRemove()
    {
        throw new NotImplementedException();
    }

    public void GAMAReceieveQuestionnaireData()
    {
        throw new NotImplementedException();
    }

    public string GetPlayerScore()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveAllActiveObjectOnMap()
    {
        throw new NotImplementedException();
    }

    public void ResendQuestionnaireData(string type)
    {
        throw new NotImplementedException();
    }

    public void ResetAllFire()
    {
        throw new NotImplementedException();
    }

    public void ShowPlayerScore(List<GAMATreesMessage> players)
    {
        throw new System.NotImplementedException();
    }

    public void StartStopGame(bool isRunning)
    {
        throw new System.NotImplementedException();
    }

    public void StatusUIControl(int i)
    {
        throw new System.NotImplementedException();
    }

    public void TutorialStart()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateFireEffect(bool t)
    {
        throw new NotImplementedException();
    }

    public void UpdateRainEffect(bool t)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateStatusUI(int index)
    {
        throw new NotImplementedException();
    }

    void IVU2EventManager.RemoveGlobalThreat(IVU2EventManager.GlobalThreat type)
    {
        throw new NotImplementedException();
    }
}
