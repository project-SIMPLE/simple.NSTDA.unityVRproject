using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FlameMonster2 : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider flameHitBox;
    // Start is called before the first frame update
    [SerializeField]
    private bool hitCoolDown;
    //[SerializeField]
    //private bool stopFlame;
    [SerializeField]
    private GameObject fireBallPrefab;

    [SerializeField]
    private GameObject shootingPoint;


    [SerializeField]
    private int hitPoint = 5;
    [SerializeField]
    private GameObject hpUI;
    [SerializeField]
    private Slider hpBar;

    // Cooldown for Hited rate
    private float hitTimer;

    // Cooldown for spawn small fire
    private float fireBallTimer;
    private float timerToSpawnFireBall = 5.0f;
    private float fireBallNum;


    private void OnEnable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += KillFlame;
        VU2ForestProtectionEventManager.Instance.OnGameStop += KillFlame;
        SetToInitialState();
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= KillFlame;
        VU2ForestProtectionEventManager.Instance.OnGameStop += KillFlame;
    }

    public void SetToInitialState()
    {
        hitPoint = 5;
        fireBallTimer = 0;
        hitTimer = 0;
        hitCoolDown = false;
        hpUI?.SetActive(false);
        fireBallNum = Random.Range(3,5);
        timerToSpawnFireBall = Random.Range(8f,15f);
    }

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

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.tag != "Water") return;

        if (!hitCoolDown)
        {
            //Debug.LogFormat("HP--");
            //stopFlame = true;
            hitCoolDown = true;
            ReduceHitPoint();
        }
        else
        {

        }


    }
    private void ReduceHitPoint()
    {
        hitPoint -= 1;
        if (hitPoint == 4) hpUI?.SetActive(true);
        if (hitPoint < 0)
        {
            Debug.Log(this.gameObject.name + " GONE");
            FlameGone();
        }
        UpdateHPBar();
    }
    public void KillFlame()
    {
        FlameGone();
    }
    public void KillFlame(bool t)
    {
        if (t) FlameGone();
    }
    private void FlameGone()
    {
        SetToInitialState();
        //VU2ForestProtectionEventManager.Instance?.ThreatUpdate(this.gameObject.name,"GONE");
        VU2ForestProtectionEventManager.Instance?.FireRemove(this.gameObject.transform.position);
        VU2ObjectPoolManager.Instance?.ReturnObjectToPool(this.gameObject);
        //this.gameObject.SetActive(false);
    }
    private void UpdateHPBar()
    {
        hpBar.value = hitPoint;
    }

    private void SpawnFireBall()
    {
        for(int i=1; i<= fireBallNum; i++) {
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
            rb.velocity = fireBall.transform.up * Random.Range(5f, 8f);
        }
        else
        {
            
        }
        //Debug.Log("Shoot Direction : " + rb.velocity);
    }
}
