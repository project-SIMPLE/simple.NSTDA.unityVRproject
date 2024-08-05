using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestbedHUDController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI appleScore;
    [SerializeField]
    private TextMeshProUGUI orangeScore;
    [SerializeField]
    private TextMeshProUGUI MangoScore;

    private void Start()
    {
        TestbedManager.instance.OnSeedCollected += UpdateSeedUI;
    }
    private void OnDestroy()
    {
        TestbedManager.instance.OnSeedCollected -= UpdateSeedUI;
    }
    private void UpdateSeedUI(int id, int value)
    {
        switch (id)
        {
            case 1:
                appleScore.text = value.ToString();
                break;
            case 2:
                orangeScore.text = value.ToString();
                break; 
            case 3:
                MangoScore.text = value.ToString();
                break;
        }
    }
}
