using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineModeGameManager : MonoBehaviour
{
    public static OnlineModeGameManager instance { get; private set; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public event Action OnGameStart;
    public void GameStart()
    {

    }

    public event Action OnGameStop;
    public void GameStop()
    {

    }

    public event Action<int> OnSeedCollected;
    public void SeedCollected(int seedID)
    {
        if (OnSeedCollected != null)
        {
            OnSeedCollected(seedID);
        }
        
    }

}
