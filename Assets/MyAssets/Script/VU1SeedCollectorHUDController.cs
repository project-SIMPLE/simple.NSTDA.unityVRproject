using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VU1SeedCollectorHUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject HUDUI;
    [SerializeField]
    private TextMeshProUGUI[] HUDUIFruitTexts;
    [SerializeField]
    private TextMeshProUGUI[] HUDUIFruitScore;

    [SerializeField]
    private GameObject WorldUI;
    [SerializeField]
    private GameObject finishMenu;
    [SerializeField]
    private GameObject stageIntroUI;
    [SerializeField]
    private GameObject resultMenuUI;
    [SerializeField]
    private TextMeshProUGUI stageNumberText;

    [SerializeField]
    private TextMeshProUGUI[] stageIntroFruitNameText;

    [SerializeField]
    private TextMeshProUGUI[] resultMenuFruitNameText;
    [SerializeField]
    private TextMeshProUGUI[] resultMenuFruitScoreText;

    [SerializeField]
    private TextMeshProUGUI finalScoreNameUI;
    [SerializeField]
    private TextMeshProUGUI finalScoreNumberUI;

    private Dictionary<int,string> IDtoFruitName = new Dictionary<int, string>()
    {
        {2,"Quercus"},
        {3,"Sapindus"},
        {4,"Magnolia"},
        {5,"Phoebe"},
        {6,"Debregeasia"},
        {7,"Diospyros"},
        {8,"Ostodes"},
        {9,"Phyllan"},
        {11,"Castano"},
        {12,"Gmelina"}
    };
    private Dictionary<int, TextMeshProUGUI> IDtofScoreUI = new Dictionary<int, TextMeshProUGUI>();
    /*
    private List<string> fruitNames = new List<string> { 
        "Quercus",
        "Sapindus",
        "Magnolia",
        "Phoebe",
        "Debregeasia",
        "Diospyros",
        "Ostodes",
        "Phyllan",
        "Castano",
        "Gmelina"
    };*/
    /**
     * 
     * Season1: 4,9,11
     * Season2: 3,4,6,-3
     * Season3: 2,3,12
     * Season4: 2,5,6,-2
     * Season5: 5,7,8
     * Season6: 4,8,9,-9
     *
     * */
    private void Start()
    {
        SeedCollectionOfflineEventManager.instance.OnGameStart += SetupHUDUIid;
        SeedCollectionOfflineEventManager.instance.OnSeedCollected += UpdateSeedUI;
        SeedCollectionOfflineEventManager.instance.OnTimerFinish += ShowPauseMenu;
        SeedCollectionOfflineEventManager.instance.OnAllCompletedAllStage += ShowFinishResultText;
    }
    private void OnDestroy()
    {
        SeedCollectionOfflineEventManager.instance.OnGameStart -= SetupHUDUIid;
        SeedCollectionOfflineEventManager.instance.OnSeedCollected -= UpdateSeedUI;
        SeedCollectionOfflineEventManager.instance.OnTimerFinish -= ShowPauseMenu;
        SeedCollectionOfflineEventManager.instance.OnAllCompletedAllStage -= ShowFinishResultText;
    }

    private int[] cFruitID;
    private void SetupHUDUIid(int season)
    {
        switch(season) 
        {
            case 1:
                cFruitID = new int[]{ 4, 9, 11 };
                break;
            case 2:
                cFruitID = new int[] { 3, 4, 6 };
                break;
            case 3:
                cFruitID = new int[] { 2, 3, 12 };
                break;
            case 4:
                cFruitID = new int[] { 2, 5, 6 };
                break;
            case 5:
                cFruitID = new int[] { 5, 7, 8 };
                break;
            case 6:
                cFruitID = new int[] { 4, 8, 9 };
                break;
            default:
                
                break;
        }

        for (int i = 0; i < cFruitID.Length; i++)
        {
            HUDUIFruitTexts[i].text = "" + IDtoFruitName[cFruitID[i]];
            HUDUIFruitScore[i].text = "0";

            IDtofScoreUI.Add(cFruitID[i], HUDUIFruitScore[i]);
        }

    }
   


    private void UpdateSeedUI(int id, int value)
    {
        IDtofScoreUI[id].text = value.ToString();
    }

    private void ShowPauseMenu(int index,bool isFinish)
    {
        IDtofScoreUI.Clear();
        HUDUI.SetActive(false);
        WorldUI.SetActive(true);
        if (isFinish)
        {
            resultMenuUI.SetActive(false);
            finishMenu.SetActive(true);
        }
        else
        {
            resultMenuUI.SetActive(true);
            stageNumberText.text = "Season " + index.ToString();
        }
        /*
        if(pausePannelScoreTexts != null)
        {
            pausePannelScoreTexts[0].text = appleScore.text;
            pausePannelScoreTexts[1].text = orangeScore.text;
            pausePannelScoreTexts[2].text = mangoScore.text;
        }*/
    }
    private void ShowAndUpdateStageIntroMenu()
    {
        for (int i = 0; i < 3; i++)
        {
            stageIntroFruitNameText[i].text = IDtoFruitName[cFruitID[i]];
        }

    }
    private void ShowAndUpdateResultMenuUI()
    {
        for (int i = 0; i < 3; i++)
        {
            resultMenuFruitNameText[i].text = HUDUIFruitTexts[i].text;
            resultMenuFruitScoreText[i].text = HUDUIFruitScore[i].text;
        }
    }

    private void ShowFinishResultText(int[] totalScore)
    {
        for (int i = 0; i < totalScore.Length; i++)
        {
            if(!IDtoFruitName.ContainsKey(i+1)) continue;

            finalScoreNameUI.text += IDtoFruitName[i+1] + Environment.NewLine;
            finalScoreNumberUI.text += ": " + totalScore[i].ToString() + Environment.NewLine;
        }
    }
}
