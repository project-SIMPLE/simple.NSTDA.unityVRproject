using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class VU2PlayerEffectAndUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject rainEffect;
    [SerializeField]
    private GameObject fireEffect;

    /**
     * 
     * 0 ReadyUI
     * 1 Result UI
     * 2 Finish UI
     * */
    [SerializeField]
    private GameObject stateUI;

    /*
     * 0 main pannel
     * 1 Before Play Q
     * 2 After Play Q
     * 
     * */
    [SerializeField]
    private GameObject[] QuestionnaireUI;
    // Start is cal
    // led before the first frame update

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
