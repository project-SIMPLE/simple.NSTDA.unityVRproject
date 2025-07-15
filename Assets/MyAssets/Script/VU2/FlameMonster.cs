using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FlameMonster : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider flameHitBox;
    [SerializeField]
    private ParticleSystem[] fireRingParticles;

    [SerializeField]
    private float auraSize = 1;
    [SerializeField]
    private bool hitCoolDown;
    //[SerializeField]
    //private bool stopFlame;

    [SerializeField]
    private int hitPoint = 5;
    [SerializeField]
    private GameObject hpUI;
    [SerializeField]
    private Slider hpBar;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        hitCoolDown = false;
        //stopFlame = false;
    }
    private void FixedUpdate()
    {
        if (!hitCoolDown)
        {
            auraSize += 0.1f * Time.deltaTime;
            IncreaseFlameSize();
        }
        else
        {
            if(timer >= 0.5f)
            {
                timer = 0;
                hitCoolDown=false;
            }
            timer += Time.deltaTime;
        }
       
    }

    private void IncreaseFlameSize()
    {
        flameHitBox.radius = auraSize * 2;
        foreach (ParticleSystem p in fireRingParticles)
        {
            var shape = p.shape;
            shape.radius = auraSize;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.tag != "Water") return;
        
        if(!hitCoolDown)
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
            this.gameObject.SetActive(false);
            //Debug.Log("MonsterDie");
        }
        UpdateHPBar();
    }
    private void UpdateHPBar()
    {
        hpBar.value = hitPoint;
    }
    
}
