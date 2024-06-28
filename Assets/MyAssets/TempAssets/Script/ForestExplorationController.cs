using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForestExplorationController : MonoBehaviour
{
    [SerializeField]
    private GameObject Move;
    
    [SerializeField]
    private GameObject EnvironmentsObj;
    [SerializeField]
    private int EnviIndex = 0;


    [SerializeField]
    private bool GameStart = false;
    [SerializeField]
    private GameObject FinishUI;

    [SerializeField]
    private ForestTimer timer;

    public static ForestExplorationController FExplorInstance { get; private set; }

    
    void Start()
    {
        //StartGame();
    }
    private void Awake()
    {
        if (FExplorInstance != null && FExplorInstance != this)
        {
            Destroy(this);
        }
        else
        {
            FExplorInstance = this;
        }


        EnvironmentsObj.transform.GetChild(EnviIndex).gameObject.SetActive(true);
        if(timer == null)
        {
            this.gameObject.GetComponent<ForestTimer>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        timer.StartTimer();
    }
    
    public void ChangeEnvironment()
    {
        if(EnviIndex == EnvironmentsObj.transform.childCount-1)
        {
            ShowFinishUI();
        }
        else
        {
            EnvironmentsObj.transform.GetChild(EnviIndex).gameObject.SetActive(false);
            EnviIndex++;
            EnvironmentsObj.transform.GetChild(EnviIndex).gameObject.SetActive(true);
            timer.StartTimer();
        }
    }
    
    public void ShowFinishUI()
    {
        if(FinishUI!= null)
        {
            FinishUI.SetActive(true);
        }
    }
    public void RestarStage()
    {
        //EnviIndex = 0;
        //timer.ResetTimer();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
    
}
