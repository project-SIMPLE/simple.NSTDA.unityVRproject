using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseFire : MonoBehaviour
{
    [SerializeField]
    protected CapsuleCollider flameHitBox;
    [SerializeField]
    protected bool hitByWater;
    [SerializeField]
    protected float coolDownValue = 0.1f;

    [SerializeField]
    protected int settingHP = 20;
    [SerializeField]
    protected int cHP;


    [SerializeField]
    protected GameObject hpUI;
    [SerializeField]
    protected Slider hpBar;

    protected void Start()
    {
        SetToInitialState();
    }

    protected virtual void FixedUpdate()
    {
        if (!hitByWater)
        {
            FlameGrowingByTime();
        }
        else
        {
            FlameRecoverFromWater();
        }
    }

    protected virtual void FlameGrowingByTime()
    {

    }
    private float revocerTimer = 0;
    protected virtual void FlameRecoverFromWater()
    {
        if (revocerTimer >= coolDownValue)
        {
            revocerTimer = 0;
            hitByWater = false;
        }
        revocerTimer += Time.deltaTime;
    }

    protected virtual void SetToInitialState()
    {
        flameHitBox.radius = 2f;
        cHP = settingHP;
        hitByWater = false;
        hpUI?.SetActive(false);
        revocerTimer = 0;
        hpBar.value = cHP;
    }

    protected virtual void ReduceHitPoint()
    {
        cHP -= 1;
        if (cHP >0) hpUI?.SetActive(true);
        if (cHP <= 0)
        {
            Debug.Log(this.gameObject.name + " GONE");
            KillFire();
        }
        UpdateHPBar();
    }

    protected virtual void UpdateHPBar()
    {
        hpBar.value = cHP;
    }

    public virtual void KillFire()
    {
        SetToInitialState();
        VU2ForestProtectionEventManager.Instance?.FireRemove(this.gameObject.transform.position);
        VU2ObjectPoolManager.Instance?.ReturnObjectToPool(this.gameObject);
        
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
        if (other.transform.tag != "Water") return;

        if (!hitByWater)
        {
            hitByWater = true;
            ReduceHitPoint();
        }
        else
        {

        }
    }

}
