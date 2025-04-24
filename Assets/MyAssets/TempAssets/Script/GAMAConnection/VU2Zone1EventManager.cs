using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2Zone1EventManager : MonoBehaviour
{
    public static VU2Zone1EventManager Instance { get; private set; }
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

    public event Action<string,int,int,Vector3> OnPlayerHitFruitOnTree;
    public void PlayerHitFruitOnTree(string treeName, int fruitIndex, int fruitID, Vector3 Pos)
    {
        OnPlayerHitFruitOnTree?.Invoke(treeName, fruitIndex, fruitID, Pos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
