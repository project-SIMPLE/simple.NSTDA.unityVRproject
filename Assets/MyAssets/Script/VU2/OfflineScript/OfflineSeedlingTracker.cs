using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineSeedlingTracker 
{
    [SerializeField]
    private SeedlingInfoScriptableObj seedlingInfo;
    [SerializeField]
    private float cGrowValue;
    [SerializeField]
    private bool isGrowing;
    [SerializeField]
    private bool isAlive;
    [SerializeField]
    private int state;

    public OfflineSeedlingTracker(SeedlingInfoScriptableObj info)
    {
        seedlingInfo = info;
        state = 1;
        cGrowValue = 0;

        isGrowing = true;
        isAlive = true;
    }
    
    public bool IsGrowing()
    {
        return isGrowing;
    }
    public void SetGrowingState(bool t)
    {
        isGrowing = t;
    }
    public bool IsAlive()
    {
        return isAlive;
    }
    public void SetAliveState(bool t)
    {
        isAlive = t;
    }
    public void SeedlingGrow(float time)
    {
        if (isGrowing)
        {
            cGrowValue += time;
        }

    }
    public int GetState()
    {
        return state;
    }
    public bool isChangingState()
    {
        if (state > 2) return false;
        if(cGrowValue >= seedlingInfo.growValue[state - 1])
        {
            state++;
            return true;
        }
        else
        {
            return false;
        }
    }
    public SeedlingInfoScriptableObj GetSeedlingInfoScript()
    {
        return seedlingInfo;
    }
}
