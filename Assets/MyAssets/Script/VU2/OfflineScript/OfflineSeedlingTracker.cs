using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineSeedlingTracker : MonoBehaviour
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
    public bool isChangingState()
    {
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
