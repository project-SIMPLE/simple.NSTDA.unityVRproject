using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSmallArea : BaseFire
{
    [SerializeField]
    private float targetAuraSize = 7;
    [SerializeField]
    private float fireSpreadRate = 0.3f;

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
        flameHitBox.radius = 2;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void FlameGrowingByTime()
    {
       
        base.FlameGrowingByTime();
        IncreaseFireSize();
    }

    private void IncreaseFireSize()
    {
        if (flameHitBox.radius < targetAuraSize)
        {
            Debug.Log("Test");
            flameHitBox.radius += fireSpreadRate * Time.deltaTime;
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
