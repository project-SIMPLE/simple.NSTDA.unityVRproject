using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private bool hasHit = false;
    private float delayTimer;
    // Start is called before the first frame update
    private void OnEnable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += RainingWhileActive;
        SetToInitialState();
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= RainingWhileActive;
    }

    void Start()
    {
        SetToInitialState();
    }
    private void SetToInitialState()
    {
        hitPrioritize = 0;
        hasHit = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private int hitPrioritize;
    private void OnCollisionEnter(Collision collision)
    {
        
        //Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("PlayArea"))
        {
            hitPrioritize += 2;
            //CreateNewFlame();
            hasHit = true;
        }
        else if (collision.transform.CompareTag("GroundArea"))
        {
            hitPrioritize += 1;
            //RemoveFireBall();
            hasHit = true;
        }
    }
    private void CreateNewFlame()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,0);

        VU2ForestProtectionEventManager.Instance?.CreateThreat("Flame2", this.transform.position);
        RemoveFireBall();
    }
    private void RainingWhileActive(bool t)
    {
        if (t)
        {
            RemoveFireBall();
        }
    }
    private void RemoveFireBall()
    {
        /*if (!hasHit)
        {
            hasHit = true;
            delayTimer = 0;
            VU2ObjectPoolManager.Instance?.ReturnObjectToPool(this.gameObject);
        }*/
        
        SetToInitialState();
        VU2ObjectPoolManager.Instance?.ReturnObjectToPool(this.gameObject);
    }
    
    private void LateUpdate()
    {
        /*if (hasHit)
        {
            if(delayTimer >= 0.1f)
            {
                delayTimer = 0;
                hasHit = false;
            }
            delayTimer += Time.deltaTime;
        }*/

        if(hasHit)
        {
            if (delayTimer >= 0.1f)
            {
                delayTimer = 0;
                hasHit = false;

                if(hitPrioritize >= 2)
                {
                    CreateNewFlame();
                }
                else if(hitPrioritize <= 1)
                {
                    RemoveFireBall();
                }

            }
            delayTimer += Time.deltaTime;
        }
    }
}
