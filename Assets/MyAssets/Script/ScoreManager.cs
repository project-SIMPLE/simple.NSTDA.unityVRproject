using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance { get; private set;}
    private int treeScore;
    private int fruitScore;
    [SerializeField]
    private int targetTreeScore;
    [SerializeField]
    private int targetFruitScore;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        //targetScore = 5;
        ResetScore();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool treeCheck;
    bool fruitCheck;
    private void CheckResult()
    {
        if(treeScore == targetTreeScore)
        {
            treeCheck = true;
            //GameManager.GameManagerInstance.PauseScene(true);
            //HUDController.HUDInstance.SwitchHUD(true);
        }
        if(fruitScore == targetFruitScore)
        {
            fruitCheck = true;
        }

        if (treeCheck && fruitCheck)
        {
            HUDController.HUDInstance.SwitchHUD(true);
        }
       
    }
    public void SetTargetFruitScore(int i)
    {
        targetFruitScore = i;
    }
    public int GetTargetFruitScore()
    {
        return targetFruitScore;
    }
    public int GetFruitScore()
    {
        return fruitScore;
    }
    public void IncreaseFruitScore(int s)
    {
        fruitScore += s;
        CheckResult();
        HUDController.HUDInstance.UpdateText1();
    }



    public void SetTargetTreeScore(int i)
    {
        targetTreeScore = i;
    }
    public int GetTargetTreeScore()
    {
        return targetTreeScore;
    }
    public int GetTreeScore()
    {
        return treeScore;
    }
    public void ResetScore()
    {
        treeScore = 0;
        fruitScore = 0;
        treeCheck = false;
        fruitCheck = false;
    }
    public void IncreaseTreeScore(int s)
    {
        treeScore += s;
        CheckResult();
        HUDController.HUDInstance.UpdateText1();
        HUDController.HUDInstance.HideInfoPanel();
    }

    public void DecreaseTreeScore(int s)
    {
        treeScore -= s;
    }
}
