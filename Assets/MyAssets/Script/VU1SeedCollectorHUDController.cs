using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
        {2,"<i>Quercus</i> sp."},
        {3,"<i>Sapindus</i> sp."},
        {4,"<i>Magnolia</i> sp."},
        {5,"<i>Phoebe</i> sp."},
        {6,"<i>Debregeasia</i> sp."},
        {7,"<i>Diospyros</i> sp."},
        {8,"<i>Ostodes</i> sp."},
        {9,"<i>Phyllanthus</i> sp."},
        {11,"<i>Castanopsis</i> sp."},
        {12,"<i>Gmelina</i> sp."}
    };
    private Dictionary<int, TextMeshProUGUI> IDtoScoreUI = new Dictionary<int, TextMeshProUGUI>();


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
    };

    /**
     * 
     * Season1: 4,9,11
     * Season2: 3,4,6,-3
     * Season3: 2,3,12
     * Season4: 2,5,6,-2
     * Season5: 5,7,8
     * Season6: 4,8,9,-9
     *
     * Alien : Quercus, Sapindus, Phyllan
     *
     *
     * */
    private void Start()
    {
        //SeedCollectionOfflineEventManager.instance.OnMoveToNextStage += SetupHUDUIid;
        SeedCollectionOfflineEventManager.instance.OnSeedCollected += UpdateSeedUI;
        SeedCollectionOfflineEventManager.instance.OnStageFinish += ShowResultMenu;
        SeedCollectionOfflineEventManager.instance.OnAllCompletedAllStage += ShowFinishResultText;
        SeedCollectionOfflineEventManager.instance.OnTutorialFinish += ShowAndUpdateStageIntroMenu;
    }
    private void OnDestroy()
    {
        //SeedCollectionOfflineEventManager.instance.OnMoveToNextStage -= SetupHUDUIid;
        SeedCollectionOfflineEventManager.instance.OnSeedCollected -= UpdateSeedUI;
        SeedCollectionOfflineEventManager.instance.OnStageFinish -= ShowResultMenu;
        SeedCollectionOfflineEventManager.instance.OnAllCompletedAllStage -= ShowFinishResultText;
        SeedCollectionOfflineEventManager.instance.OnTutorialFinish -= ShowAndUpdateStageIntroMenu;
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

            IDtoScoreUI.Add(cFruitID[i], HUDUIFruitScore[i]);
        }

    }
   


    private void UpdateSeedUI(int id, int value)
    {
        IDtoScoreUI[id].text = value.ToString();
    }

    private void ShowResultMenu()
    {
        ShowAndUpdateResultMenuUI();

        IDtoScoreUI.Clear();
        HUDUI.SetActive(false);
        


        /*if (isFinish)
        {
            //resultMenuUI.SetActive(false);
            finishMenu.SetActive(true);
        }
        else
        {
            resultMenuUI.SetActive(true);
            stageNumberText.text = "Season " + index.ToString();
        }*/
        /*
        if(pausePannelScoreTexts != null)
        {
            pausePannelScoreTexts[0].text = appleScore.text;
            pausePannelScoreTexts[1].text = orangeScore.text;
            pausePannelScoreTexts[2].text = mangoScore.text;
        }*/
    }
    private void ShowAndUpdateStageIntroMenu(int stageIndex)
    {
        SetupHUDUIid(stageIndex);
        WorldUI.SetActive(true);
        stageIntroUI.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            stageIntroFruitNameText[i].text = IDtoFruitName[cFruitID[i]];
        }
        stageNumberText.text = "Season " + stageIndex.ToString();
    }
    public void CloseStageIntroMenu()
    {
        WorldUI.SetActive(false);
        stageIntroUI.SetActive(false);
        HUDUI.SetActive(true);
    }

    private void ShowAndUpdateResultMenuUI()
    {
        WorldUI.SetActive(true);
        resultMenuUI.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            resultMenuFruitNameText[i].text = HUDUIFruitTexts[i].text;
            resultMenuFruitScoreText[i].text = HUDUIFruitScore[i].text;
        }
    }
    public void CloseStageResultMenu()
    {
        WorldUI.SetActive(false);
        resultMenuUI.SetActive(false);
    }


    private void ShowFinishResultText(int[] totalScore)
    {
        WorldUI.SetActive(true);
        finishMenu.SetActive(true);
        UpdateNumberofAlienFruit();
        for (int i = 0; i < totalScore.Length; i++)
        {
            if(!IDtoFruitName.ContainsKey(i+1)) continue;

            if (i == 1 || i == 2 || i == 8)
            {
                finalScoreNameUI.text += IDtoFruitName[i + 1] + Environment.NewLine;
                int aIndex = GetAlienIndex(i);

                if (alienList[aIndex] > 0)
                {
                    finalScoreNumberUI.text += ": " + totalScore[i].ToString() + "<color=red>(" + alienList[aIndex] + " Alien sp. fruits)</color>"

                        + Environment.NewLine;
                }
                else
                {
                    finalScoreNumberUI.text += ": " + totalScore[i].ToString() + Environment.NewLine;
                }
            }
            else
            {
                finalScoreNameUI.text += IDtoFruitName[i + 1] + Environment.NewLine;
                finalScoreNumberUI.text += ": " + totalScore[i].ToString() + Environment.NewLine;
            }
            
        }
    }
    private int GetAlienIndex(int id)
    {
        
        if (id == 1)
        {
            return 0;
        }
        else if (id == 2)
        {
            return 1;      }
        else if (id == 8)
        {
            return 2;
        }
        else return -1;

    }

    private int[] alienList;
    private void UpdateNumberofAlienFruit()
    {
        alienList = SeedCollectionOfflineEventManager.instance.GetAlienLists();
    }

}
