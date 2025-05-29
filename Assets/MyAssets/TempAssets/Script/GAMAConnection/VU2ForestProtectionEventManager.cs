using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2ForestProtectionEventManager : MonoBehaviour
{
    public static VU2ForestProtectionEventManager Instance { get; private set; }
    // Start is called before the first frame update
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
        
    }


    void Start()
    {
        
    }

    public event Action<string, string> OnTreeChangeState;
    public void TreeChangeState(string treeName, string state)
    {
        OnTreeChangeState?.Invoke(treeName, state);
    }



    /*public event Action<string, int> OnRemoveLocalFruitOnTree;
    public void RemoveFruitOnTree(string treeName, int treeIndex)
    {
        OnRemoveLocalFruitOnTree?.Invoke(treeName,treeIndex);
    }

    public event Action<string,int,int,Vector3> OnPlayerHitFruitOnTree;
    public void PlayerHitFruitOnTree(string treeName, int fruitIndex, int fruitID, Vector3 Pos)
    {
        OnPlayerHitFruitOnTree?.Invoke(treeName, fruitIndex, fruitID, Pos);
    }

    public event Action<string> OnPutFruitIntoBucket;
    public void PutFruitIntoBucket(string seedName)
    {
        OnPutFruitIntoBucket?.Invoke(seedName);
    }

    public event Action OnLoadSeedToNextZone;
    public void LoadSeedToNextZone()
    {
        OnLoadSeedToNextZone?.Invoke();
    }*/
    // Update is called once per frame
    void Update()
    {
        
    }

}
