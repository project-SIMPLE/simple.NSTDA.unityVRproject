using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IVU2GAMACommandLogic
{
    public void ShowPlayerScore(List<GAMATreesMessage> players);
    public void SetPlayerID(string playerID);
    public void RemoveOtherPlayerTree(List<GAMATreesMessage> tree);
    public void UpdateTreeFromGAMA(List<GAMATreesMessage> tree);
    public void UpdateGrassOnTreeFromGAMA(List<GAMATreesMessage> tree);
    public void UpdateThreatsMessageFromGAMA(List<GAMAThreatMessage> threats);
    public void UpdatePlayerBackground(List<GAMATreesMessage> message);
    public void TreeChangeState(string treeName, string state);
    public void HandleRemoveThreatsMessageFromGAMA(List<GAMAThreatMessage> threats);
}
