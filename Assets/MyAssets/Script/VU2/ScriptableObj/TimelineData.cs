using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AlienEvent
{
    public int spawnTime;
    public string alienType;
}

[System.Serializable]
public struct RainEvent
{
    public int startTime;
    public int stopTime;
}

[System.Serializable]
public struct GrassEvent
{
    public int spawnTime;
    public string grassType;
}

[System.Serializable]
public struct FireEvent
{
    public int spawnTime;
    public string fireType;
}

[CreateAssetMenu(fileName = "TimelineData")]
public class TimelineData : ScriptableObject
{
    public List<AlienEvent> alienEvents = new List<AlienEvent>
    {
        new AlienEvent { spawnTime =   0, alienType = "A2" },
        new AlienEvent { spawnTime =  15, alienType = "A1" },
        new AlienEvent { spawnTime =  30, alienType = "A2" },
        new AlienEvent { spawnTime =  90, alienType = "A2" },
        new AlienEvent { spawnTime = 105, alienType = "A1" },
        new AlienEvent { spawnTime = 120, alienType = "A2" },
        new AlienEvent { spawnTime = 165, alienType = "A1" },
        new AlienEvent { spawnTime = 180, alienType = "A2" },
        new AlienEvent { spawnTime = 195, alienType = "A1" },
    };

    public List<RainEvent> rainEvents = new List<RainEvent>
    {
        new RainEvent { startTime =  15, stopTime =  45 },
        new RainEvent { startTime =  90, stopTime = 135 },
        new RainEvent { startTime = 225, stopTime = 240 },
    };

    public List<GrassEvent> grassEvents = new List<GrassEvent>
    {
        new GrassEvent { spawnTime =  15, grassType = "G1" },
        new GrassEvent { spawnTime =  30, grassType = "G2" },
        new GrassEvent { spawnTime = 105, grassType = "G2" },
        new GrassEvent { spawnTime = 120, grassType = "G2" },
        new GrassEvent { spawnTime = 180, grassType = "G1" },
        new GrassEvent { spawnTime = 195, grassType = "G1" },
    };

    public List<FireEvent> fireEvents = new List<FireEvent>
    {
        new FireEvent { spawnTime =  47, fireType = "F1" },
        new FireEvent { spawnTime =  60, fireType = "F2" },
        new FireEvent { spawnTime =  75, fireType = "F1" },
        new FireEvent { spawnTime = 150, fireType = "F1" },
        new FireEvent { spawnTime = 165, fireType = "F2" },
        new FireEvent { spawnTime = 180, fireType = "F2" },
        new FireEvent { spawnTime = 195, fireType = "F2" },
        new FireEvent { spawnTime = 210, fireType = "F1" },
    };
}
