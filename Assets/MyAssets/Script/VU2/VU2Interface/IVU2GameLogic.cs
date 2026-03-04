using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IVU2GameLogic
{
    public void LogicStartStopGame(bool isRunning);
    public void LogicShowPlayerScore(List<GAMATreesMessage> players);
    public string LogicGetplayerScore();
    public void LogicSetPlayerID(string playerID);
    public void LogicRemoveOtherPlayerTree(List<GAMATreesMessage> tree);
    public void LogicUpdateTreeFromGAMA(List<GAMATreesMessage> tree);
    public void LogicUpdateGrassOnTreeFromGAMA(List<GAMATreesMessage> tree);
    public void LogicUpdateThreatsMessageFromGAMA(List<GAMAThreatMessage> threats);
    public void LogicGetPlayerRainEffect(string effect);
    public void LogicUpdateRainEffect(bool t);
    public void LogicUpdatePlayerBackground(List<GAMATreesMessage> message);

    public int LogicGetBGStage();

    public void LogicUpdateFireEffect(bool t);
    public void LogicFireRemove();
    public void LogicResetAllFire();

    public void LogicCreateThreat(string name, Vector3 pos);
    public GameObject LogicCreateWeed(Vector3 pos, Quaternion rot, int weedType);

    public void LogicHandleRemoveThreatsMessageFromGAMA(List<GAMAThreatMessage> threats);

    public void LogicCollectQuestionnaireData(string qType, string data);
    public void LogicGAMAReceieveQuestionnaireData();
    public void LogicResendQuestionnaireData(string type);
    
}
public enum GlobalThreat
{
    Fire,
    Grasses,
    AlienPlant,
    Non
}
