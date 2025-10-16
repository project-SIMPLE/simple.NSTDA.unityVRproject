using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSmallArea : BaseFire
{
    [SerializeField]
    private float auraSize = 7;

    private void OnEnable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += OnRainShow;
        VU2ForestProtectionEventManager.Instance.OnGameStop += KillFire;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= OnRainShow;
        VU2ForestProtectionEventManager.Instance.OnGameStop -= KillFire;
    }

    protected override void SetToInitialState()
    {
        base.SetToInitialState();
        auraSize = 1;
    }

    protected override void FlameGrowingByTime()
    {
        base.FlameGrowingByTime();
        IncreaseFireSize();
    }

    private void IncreaseFireSize()
    {
        if (flameHitBox.radius < auraSize)
        {
            flameHitBox.radius += 0.1f * Time.deltaTime;
        }
    }

    protected override void FlameRecoverFromWater()
    {
        base.FlameRecoverFromWater();
    }

    protected override void KillFire()
    {
        base.KillFire();
    }
    private void OnRainShow(bool isRain)
    {
        if(isRain) KillFire();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }


}
