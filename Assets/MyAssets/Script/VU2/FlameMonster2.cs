using System.Collections;
using System.Collections.Generic;
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
    private float normalTimer;
    private float timerCount = 5.0f;


    private void OnEnable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += KillFlame;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= KillFlame;
    }

    private void FixedUpdate()
    {
        if (!hitCoolDown)
        {
            if(normalTimer >= timerCount)
            {
                normalTimer = 0;


            }
            normalTimer += Time.deltaTime;
        }
        else
        {
            if (hitTimer >= 0.5f)
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
        //VU2ForestProtectionEventManager.Instance?.ThreatUpdate(this.gameObject.name,"GONE");
        VU2ForestProtectionEventManager.Instance?.FireRemove(this.gameObject.transform.position);
        VU2ObjectPoolManager.Instance?.ReturnObjectToPool(this.gameObject);
        //this.gameObject.SetActive(false);
    }
    private void UpdateHPBar()
    {
        hpBar.value = hitPoint;
    }

    public void ShootFireParticle()
    {
        GameObject fireBall = Instantiate(fireBallPrefab, shootingPoint.transform.position, Quaternion.identity);
        Rigidbody rb = fireBall.GetComponent<Rigidbody>();

        Vector3 fireBallDirection = new Vector3(0, Random.Range(0f, 359f), 30f);
        rb.velocity = fireBallDirection.normalized * 10f;
        Debug.Log("Shoot Direction : " + rb.velocity);
    }
}
