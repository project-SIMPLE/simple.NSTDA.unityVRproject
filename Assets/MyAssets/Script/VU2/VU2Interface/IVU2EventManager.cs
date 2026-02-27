using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IVU2EventManager
{
    public void StartStopGame(bool isRunning);
    public void ShowPlayerScore(List<GAMATreesMessage> players);
    public string GetPlayerScore();
    public void StatusUIControl(int i);
    public event Action<string, int> OnUpdateTreeState;
    public event Action<string> OnUpdateGrassOnTree;

    public event Action OnTutorialStart;
    public void TutorialStart();

    public event Action<bool> OnUpdateRainEffect;
    public void UpdateRainEffect(bool t);

    public event Action<bool> OnUpdateFireEffect;
    public void UpdateFireEffect(bool t);

    public event Action OnFireRemove;
    public void FireRemove();

    public void ResetAllFire();

    public event Action<int> OnUpdateStateUI;
    public void UpdateStatusUI(int index);

    public event Action OnGameStop;
    public void RemoveAllActiveObjectOnMap();

    public void CreateThreat(string name, Vector3 pos);
    public GameObject CreateWeed(Vector3 pos, Quaternion rot, int weedType);


    public event Action<GlobalThreat> OnRemoveGlobalThreat;
    public void RemoveGlobalThreat(GlobalThreat type);

    public enum GlobalThreat
    {
        Fire,
        Grasses,
        AlienPlant,
        Non
    }
}
