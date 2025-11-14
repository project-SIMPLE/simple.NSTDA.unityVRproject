using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FlameMonster2 : FireSmallArea, ICreateFireOnTree, IGlobalThreat
{
    public enum FireState
    {
        Growing,
        Shooting
    }

    //[SerializeField]
    //private bool stopFlame;
    [SerializeField]
    private GameObject fireBallPrefab;

    [SerializeField]
    private GameObject shootingPoint;

    [SerializeField]
    private FireState fs = FireState.Growing;
    [SerializeField]
    private bool spawnOnMax = false;
    // Cooldown for spawn small fire
    //private float timeToChangeState = 10f;
    [SerializeField]
    private float fireBallTimer;
    private float timerToSpawnFireBall = 20.0f;
    private float fireBallNum;



    private void OnEnable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += OnRainShow;
        VU2ForestProtectionEventManager.Instance.OnGameStop += KillFire;
        VU2ForestProtectionEventManager.Instance.OnRemoveGlobalThreat += RemoveGlobalThreat;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= OnRainShow;
        VU2ForestProtectionEventManager.Instance.OnGameStop -= KillFire;
        VU2ForestProtectionEventManager.Instance.OnRemoveGlobalThreat -= RemoveGlobalThreat;
    }

    protected override void SetToInitialState()
    {
        fs = FireState.Growing;
        fireBallTimer = 0;
        base.SetToInitialState();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /*
        private void FixedUpdate()
        {
            if (!hitCoolDown)
            {
                if(fireBallTimer >= timerToSpawnFireBall)
                {
                    fireBallTimer = 0;
                    SpawnFireBall();

                }
                fireBallTimer += Time.deltaTime;
            }
            else
            {
                if (hitTimer >= 0.25f)
                {
                    hitTimer = 0;
                    hitCoolDown = false;
                }
                hitTimer += Time.deltaTime;
            }
        }
    */
    protected override void FlameGrowingByTime()
    {
        

        if (fs == FireState.Growing)
        {
            base.FlameGrowingByTime();
        }else if(fs == FireState.Shooting)
        {
            CountDownToShootFireBall();
        }

        if (flameHitBox.radius >= targetAuraSize && fs != FireState.Shooting)
        {

            fs = FireState.Shooting;
            if (spawnOnMax)
            {
                SpawnFireBall();
            }
            //SpawnFireBall();
        }

    }
    protected void CountDownToShootFireBall()
    {
        if (fireBallTimer >= timerToSpawnFireBall)
        {
            

            SpawnFireBall();
            fireBallTimer = 0;

        }
        fireBallTimer += Time.deltaTime;
    }

    protected override void FlameRecoverFromWater()
    {
        base.FlameRecoverFromWater();
    }

    public override void KillFire()
    {
        base.KillFire();
    }
    private void OnRainShow(bool isRain)
    {
        if (isRain) KillFire();
    }
    

    private void SpawnFireBall()
    {
        fireBallNum = Random.Range(1, 4);

        for (int i=1; i<= fireBallNum; i++) {
            ShootFireParticle();
        }
    }
    public void ShootFireParticle()
    {
        Vector3 fireBallDirection = new Vector3(0, Random.Range(0f, 359f), 45f);

        //GameObject fireBall = Instantiate(fireBallPrefab, shootingPoint.transform.position, Quaternion.Euler( fireBallDirection));
        GameObject fireBall =  VU2ObjectPoolManager.Instance?.SpawnObject(fireBallPrefab, shootingPoint.transform.position, Quaternion.Euler(fireBallDirection));

        Rigidbody rb = fireBall.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = fireBall.transform.up * Random.Range(6f, 10f);
        }
        else
        {
            
        }
        
    }

    protected override void CreateFireOnTree(GameObject target)
    {
        base.CreateFireOnTree(target);

    }
    protected override void RemoveAllFireOnTree()
    {
        base.RemoveAllFireOnTree();

    }

    public override void OnFireHitTree(GameObject tree)
    {
        base.OnFireHitTree(tree);
       
    }

    public void RemoveGlobalThreat(GlobalThreat type)
    {
        if(type == GlobalThreat.Fire)
        {
            KillFire();
        }
    }
}
