using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class VU2PlayerEffectAndUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject rainEffect;
    [SerializeField]
    private GameObject fireEffect;

    /**
     * 0 Ready pannel
     * 1 Brfore Tutorial
     * 2 After tutorial Q1
     * 3 Finish Game score UI
     * 4 After Q2 & Change player
     * 
     * */
    [SerializeField]
    private GameObject stateUI;

    /*
     * 0 pannel
     * 1 Before play Q (Q1)
     * 2 After play Q (Q2)
     * 
     * */
    [SerializeField]
    private GameObject[] QuestionnaireUI;
    // Start is cal
    // led before the first frame update
    [SerializeField]
    private TextMeshProUGUI scoreText;


    private void Start()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += UpdateRainEffect;
        VU2ForestProtectionEventManager.Instance.OnUpdateFireEffect += UpdateFireEffect;
        VU2ForestProtectionEventManager.Instance.OnUpdateStateUI += StatusUI;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= UpdateRainEffect;
        VU2ForestProtectionEventManager.Instance.OnUpdateFireEffect -= UpdateFireEffect;
        VU2ForestProtectionEventManager.Instance.OnUpdateStateUI -= StatusUI;
    }
    private void UpdateRainEffect(bool t)
    {
        rainEffect.SetActive(t);
    }
    private void UpdateFireEffect(bool t)
    {
        fireEffect.SetActive(t);
    }
    private void HideEffect()
    {

    }

    public void SetScoreText(string score)
    {
        //Debug.Log(score);
        scoreText.text = score;
    }

    public void StatusUI(int index)
    {
        for(int i=0; i< stateUI.transform.childCount; i++)
        {
            stateUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        if(index != -1)
        {
            stateUI.transform.GetComponent<UIFollowCam>()?.MoveInFrontOfCamera();
            stateUI.transform.GetChild(index).gameObject.SetActive(true);

        }

        if (index == 3)
        {
            SetScoreText(VU2ForestProtectionEventManager.Instance?.GetPlayerScore());
            //Debug.Log(VU2ForestProtectionEventManager.Instance?.GetPlayerScore());
        }
    }




    /*
     * -1 Close
     * 1 before play Q
     * 2 After play Q
     * 
     * */
    public void OpenQuestionnaire(int num)
    {
        //Debug.Log($"Open Questionnaire: {num}");
        QuestionnaireUI[0].SetActive(true);
        QuestionnaireUI[1].SetActive(false);
        QuestionnaireUI[2].SetActive(false);
        QuestionnaireUI[0].transform.GetComponent<UIFollowCam>()?.MoveInFrontOfCamera();

        switch (num)
        {
            case -1:
                QuestionnaireUI[0].SetActive(false);
                break;
            case 1:
                QuestionnaireUI[1].SetActive(true);
                break;
            case 2:
                QuestionnaireUI[2].SetActive(true);
                break;
        }
    }


    /*public enum UIPannel
    {
        None = -1,
        ReadyUI = 0,
        BeforeTutorial = 1,
        AfterQ1 = 2,
        ScoreUI = 3,
        FinishUI = 4
    }*/

}
