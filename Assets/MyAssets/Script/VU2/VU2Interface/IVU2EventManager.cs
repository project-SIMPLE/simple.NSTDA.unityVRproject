using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IVU2EventManager
{

    public event Action<string, int> OnUpdateTreeState;
    public event Action<string> OnUpdateGrassOnTree;
    public event Action<string, string> OnTreeChangeState;

    public event Action OnTutorialStart;
    public event Action<bool> OnUpdateRainEffect;
    public event Action<bool> OnUpdateFireEffect;
    public event Action OnFireRemove;
    public event Action<int> OnUpdateStateUI;
    public event Action OnGameStop;
    public event Action<GlobalThreat> OnRemoveGlobalThreat;


    public void StartStopGame(bool isRunning);

    public string GetPlayerScore();
    public void StatusUIControl(int i);
    public void GetPlayerRainEffect(string effect);
    public void TutorialStart();

    
    public void UpdateRainEffect(bool t);
    public void UpdateFireEffect(bool t);
    public int GetBGStage();


    public void FireRemove();

    public void ResetAllFire();

    public void UpdateStatusUI(int index);

    public void RemoveAllActiveObjectOnMap();

    public void CreateThreat(string name, Vector3 pos);
    public GameObject CreateWeed(Vector3 pos, Quaternion rot, int weedType);

    
    public void RemoveGlobalThreat(GlobalThreat type);

    /*public enum GlobalThreat
    {
        Fire,
        Grasses,
        AlienPlant,
        Non
    }*/
}
