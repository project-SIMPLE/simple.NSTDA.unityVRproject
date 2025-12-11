using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForestTimer : MonoBehaviour
{
    [SerializeField]
    private float SetTimer;
    [SerializeField]
    private TextMeshProUGUI TimerText;
    [SerializeField]
    private bool TimerOn = false;

    [SerializeField]
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        if(SetTimer == 0f)
        {
            SetTimer = 20f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn)
        {
            if(timer > 0)
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
        ForestExplorationController.FExplorInstance.ChangeEnvironment();
    }
    public void ResetTimer()
    {
        TimerOn = false;
        timer = SetTimer;
    }
}
