using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CountdownDoSomething : MonoBehaviour
{
    [SerializeField]
    private float countdownTime;
    [SerializeField]
    private bool isShowTimeInSec = false;
    [SerializeField]
    private TextMeshProUGUI timerText;
    private float timeRemaining;

    public UnityEvent OnCountDownFinish;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        timeRemaining = countdownTime;
        if(isShowTimeInSec )
        {
            StartCoroutine("UpdateEverySecUntilFinish");
        }
        else
        {
            Invoke("FinishCounting", timeRemaining);
        }
    }

    IEnumerator UpdateEverySecUntilFinish()
    {
        while (timeRemaining > 0)
        {
            timerText.text = timeRemaining.ToString();
            yield return new WaitForSeconds(1);
            timeRemaining -= 1;
        }
        FinishCounting();
    }

    private void FinishCounting()
    {
        OnCountDownFinish?.Invoke();
    }

}
