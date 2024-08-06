using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBedTimer : MonoBehaviour
{
    [SerializeField]
    protected float SetTimer;
    [SerializeField]
    protected bool TimerOn = false;
    [SerializeField]
    protected float timer;
    // Start is called before the first frame update
    void Start()
    {
        TestbedManager.instance.OnGameStart += StartTimer;
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
    void OnDestroy()
    {
        TestbedManager.instance.OnGameStart -= StartTimer;
    }

    public void EditSetTimer(int i)
    {
        SetTimer = i;
    }
    public void StartTimer()
    {
        TimerOn = true;
        timer = SetTimer;
    }
    public void StopTimer()
    {
        TimerOn = false;
        TestbedManager.instance.TimerFinish();

    }
    public void ResetTimer()
    {
        TimerOn = false;
        timer = SetTimer;
    }
}
