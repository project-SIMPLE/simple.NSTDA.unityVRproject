using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SeedInSeason
{
    public GameObject treePrefab;
    public int number;
}


[CreateAssetMenu(fileName = "SeedPhenologyTimeline")]
public class SeedcollectionTimeline : ScriptableObject
{
    public List<SeedInSeason> Season1 = new List<SeedInSeason>();
    public List<SeedInSeason> Season2 = new List<SeedInSeason>();
    public List<SeedInSeason> Season3 = new List<SeedInSeason>();
    public List<SeedInSeason> Season4 = new List<SeedInSeason>();
    public List<SeedInSeason> Season5 = new List<SeedInSeason>();
    public List<SeedInSeason> Season6 = new List<SeedInSeason>();
}
