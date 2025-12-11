using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimer : MonoBehaviour
{
    [SerializeField]
    protected float SetTimer;
    [SerializeField]
    protected bool TimerOn = false;
    [SerializeField]
    protected float timer;

    
    void Start()
    {
       /* if (SetTimer == 0f)
        {
            SetTimer = 20f;
        }*/
      
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
                StopTimer();
            }
        }
    }
    public virtual void EditSetTimer(int i)
    {
        SetTimer = i;
    }
    public virtual void StartTimer()
    {
        TimerOn = true;
        timer = SetTimer;
    }
    public virtual void StopTimer()
    {
        TimerOn = false;
        
    }
    public virtual void ResetTimer()
    {
        TimerOn = false;
        timer = SetTimer;
    }
    
}
