using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2OfflineGameManager : MonoBehaviour
{
    [SerializeField]
    VU2SeedlingsManager seedlingsManager;

    private int score;
    [SerializeField]
    private bool isGameRunning = false;

    [SerializeField]
    private float countEverySec;
    [SerializeField]
    private float playTimeInSec;

    private void FixedUpdate()
    {
        if (isGameRunning)
        {
            playTimeInSec -= Time.deltaTime;
            countEverySec += Time.deltaTime;

            if(playTimeInSec <= 0)
            {
                GameStop();
            }
            if(countEverySec >= 1)
            {
                UpdateTreesGrownValue();
                CheckThreatTable();
                countEverySec = 0;
            }


        }
    }
    
    public void GameStart()
    {
        countEverySec = 0;
        playTimeInSec = 240;
        isGameRunning = true;
    }
    public void GameStop()
    {
        isGameRunning = false;
    }

    private void UpdateTreesGrownValue()
    {
        seedlingsManager.AddGrowValueToSeedling();
    }
    private void CheckThreatTable()
    {

    }
}
