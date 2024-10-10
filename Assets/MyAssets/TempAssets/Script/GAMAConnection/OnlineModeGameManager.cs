using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineModeGameManager : MonoBehaviour
{
    public static OnlineModeGameManager Instance { get; private set; }

    [SerializeField]
    private GameObject[] interactableItemAndTools;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        ActiveInteractableItemAndTools(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ActiveInteractableItemAndTools(bool t)
    {
        if (interactableItemAndTools == null) return;
        foreach(GameObject item in interactableItemAndTools)
        {
            item.SetActive(t);
        }
    }


    public event Action OnGameStart;
    public void GameStart()
    {
        ActiveInteractableItemAndTools(true);
    }

    public event Action OnGameStop;
    public void GameStop()
    {
        ActiveInteractableItemAndTools(false);
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
