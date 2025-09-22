using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ThreatTutorialFire : MonoBehaviour
{
    public UnityEvent OnTaskFinish;

    [SerializeField]
    private bool hitCoolDown;
    [SerializeField]
    private int hitPoint = 5;
    [SerializeField]
    private GameObject hpUI;
    [SerializeField]
    private Slider hpBar;

    // Cooldown for Hited rate
    private float timer;

    public void SetToInitialState()
    {
        
        hitPoint = 5;
        timer = 0;
        hitCoolDown = false;
        hpUI?.SetActive(false);

    }
    void Start()
    {
        //SetToInitialState();
    }
    private void OnEnable()
    {
        SetToInitialState();
    }
    private void FixedUpdate()
    {
        if (!hitCoolDown)
        {
            
        }
        else
        {
            if (timer >= 0.5f)
            {
                timer = 0;
                hitCoolDown = false;
            }
            timer += Time.deltaTime;
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.tag != "Water") return;

        if (!hitCoolDown)
        {
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
            FlameGone();
        }
        UpdateHPBar();
    }

    private void FlameGone()
    {
        OnTaskFinish?.Invoke();
        this.gameObject.SetActive(false);
    }
    private void UpdateHPBar()
    {
        hpBar.value = hitPoint;
    }
}
