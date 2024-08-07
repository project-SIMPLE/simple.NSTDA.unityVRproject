using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestbedHUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject HUDUI;
    [SerializeField]
    private TextMeshProUGUI appleScore;
    [SerializeField]
    private TextMeshProUGUI orangeScore;
    [SerializeField]
    private TextMeshProUGUI mangoScore;

    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject finishMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private TextMeshProUGUI stageNumberText;

    [SerializeField]
    private TextMeshProUGUI[] pausePannelScoreTexts;
    private void Start()
    {
        TestbedManager.instance.OnSeedCollected += UpdateSeedUI;
        TestbedManager.instance.OnTimerFinish += ShowPauseMenu;
    }
    private void OnDestroy()
    {
        TestbedManager.instance.OnSeedCollected -= UpdateSeedUI;
        TestbedManager.instance.OnTimerFinish -= ShowPauseMenu;
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
                mangoScore.text = value.ToString();
                break;
        }
    }
    private void ShowPauseMenu(int index,bool isFinish)
    {
        HUDUI.SetActive(false);
        pauseUI.SetActive(true);
        if (isFinish)
        {
            pauseMenu.SetActive(false);
            finishMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
            stageNumberText.text = "Stage " + index.ToString();
        }
        
        if(pausePannelScoreTexts != null)
        {
            pausePannelScoreTexts[0].text = appleScore.text;
            pausePannelScoreTexts[1].text = orangeScore.text;
            pausePannelScoreTexts[2].text = mangoScore.text;
        }
    }

}
