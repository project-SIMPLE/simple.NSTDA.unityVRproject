using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2OfflineGameManager : MonoBehaviour
{
    [SerializeField]
    VU2SeedlingsManager seedlingsManager;

    //private int score;
    [SerializeField]
    private bool isGameRunning = false;

    [SerializeField]
    private float countEverySec;
    [SerializeField]
    private float currentPlayTime;
    [SerializeField]
    private float totalPlayTime = 240;

    private void FixedUpdate()
    {
        if (isGameRunning)
        {
            currentPlayTime += Time.deltaTime;
            countEverySec += Time.deltaTime;

            if(currentPlayTime > totalPlayTime)
            {
                GameStop();
            }
            if(countEverySec >= 1)
            {
                UpdateTreesGrownValue();
                CheckThreatTable();
                countEverySec = 0;
            }
            if(currentPlayTime >= 175f && currentPlayTime < 176f)
            {
                VU2BGSoundManager.Instance.AnnounceOneMinRemained();
            }
            //CheckThreatTable();

        }
    }
    
    public void GameStart()
    {
        countEverySec = 0;
        currentPlayTime = 0;
        isGameRunning = true;
        seedlingsManager.PrepareSeedlingArea();
        ResetSpwanIndex();
}
    public void GameStop()
    {
        isGameRunning = false;
        seedlingsManager.ClearSeedlingArea();
        VU2ForestProtectionEventManager.Instance?.StartStopGame(false);
    }

    private void UpdateTreesGrownValue()
    {
        seedlingsManager.AddGrowValueToSeedling();
    }
    

    ////////////////////////////////// Spwan Manager
    [Header("Data")]
    public TimelineData timeData;
    private int alineIdx;
    private int rainStartIdx;
    private int rainStopIdx;
    private int grassIdx;
    private int fireIdx;

    private void ResetSpwanIndex()
    {
        alineIdx = 0;
        rainStartIdx = 0;
        rainStopIdx = 0;
        grassIdx = 0;
        fireIdx = 0;
    }

    private void CheckThreatTable()
    {
        //Alien
        if (alineIdx < timeData.alienEvents.Count && currentPlayTime >= timeData.alienEvents[alineIdx].spawnTime)
        {
            SpawnThreat(timeData.alienEvents[alineIdx].alienType);
            alineIdx++;
        }
        if (grassIdx < timeData.grassEvents.Count && currentPlayTime >= timeData.grassEvents[grassIdx].spawnTime)
        {
            SpawnGrassesOnSeedling(timeData.grassEvents[grassIdx].grassType);
            grassIdx++;
        }
        if (fireIdx < timeData.fireEvents.Count && currentPlayTime >= timeData.fireEvents[fireIdx].spawnTime)
        {
            SpawnThreat(timeData.fireEvents[fireIdx].fireType);
            fireIdx++;
        }

        //Rain Start/Stop
        if (rainStartIdx < timeData.rainEvents.Count && currentPlayTime >= timeData.rainEvents[rainStartIdx].startTime)
        {
            VU2ForestProtectionEventManager.Instance.GetPlayerRainEffect("Start");
            rainStartIdx++;
        }
        if (rainStopIdx < timeData.rainEvents.Count && currentPlayTime >= timeData.rainEvents[rainStopIdx].stopTime)
        {
            VU2ForestProtectionEventManager.Instance.GetPlayerRainEffect("Stop");
            rainStopIdx++;
        }

    }

    private void SpawnGrassesOnSeedling(string type)
    {
        int num = 0;
        switch (type)
        {
            case "G1":
                num = 2;
                break;
            case "G2":
                num = 8;
                break;
        }
        seedlingsManager.AddGrassesOnSeedling(num);
    }

    private void SpawnThreat(string type)
    {
        int num = 1;
        string prefabName = "";
        switch (type)
        {
            case "A1":
                num = 1;
                prefabName = "Alien";
                break;
            case "A2":
                num = 4;
                prefabName = "Alien2";
                break;
            case "F1":
                prefabName = "Flame1";
                break;
            case "F2":
                prefabName = "Flame2";
                break;

        }
        List<Vector3> posinMap = seedlingsManager.GetRandomPointInSeedlingZone(10, num);
        foreach (Vector3 pos in posinMap)
        {
            Vector3 newPos = new Vector3(pos.x,0.05f,pos.z);
            VU2ForestProtectionEventManager.Instance.CreateThreat(prefabName, newPos);

        }

    }

    private void SpawnAlien(string type)
    {
        int num = 0;
        
        switch (type)
        {
            case "A1":
                num = 1;
                break;
            case "A2":
                num = 4;
                break;
        }
        List<Vector3> posinMap = seedlingsManager.GetRandomPointInSeedlingZone(10,num);


    }
    private void SpawnFire(string type)
    {
        int num = 1;
        
        switch (type)
        {
            case "F1":
                break;
            case "F2":
                break;
        }
        List<Vector3> posOnMap = seedlingsManager.GetRandomPointInSeedlingZone(10, num); ;
    }

}
