using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestbedManager : MonoBehaviour
{

    public static TestbedManager instance { get; private set;}
    [SerializeField]
    private int[] fruitListScore = { 0, 0, 0 };


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

    public event Action<int,int> OnSeedCollected;
    public void SeedCollected(int id)
    {
        /*if(OnSeedCollected != null)
        {
            OnSeedCollected(id);
        }*/
        fruitListScore[id-1]++;
        OnSeedCollected(id, fruitListScore[id - 1]);
    }
    public event Action OnResetSeedPosition;
    public void ResetSeedPosition()
    {
        if(OnResetSeedPosition != null)
        {
            OnResetSeedPosition();
        }
    }
}
