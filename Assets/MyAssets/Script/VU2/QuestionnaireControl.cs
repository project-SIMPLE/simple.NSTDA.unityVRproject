using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class QuestionnaireControl : MonoBehaviour
{
    public UnityEvent OnFinishQuestionnaire;
    public UnityEvent<string,string> OnSendQuestionnaireData;

    [SerializeField]
    private string qType;
    [SerializeField]
    private int totalQuestionNum;
    [SerializeField]
    private int cQuestion;
    [SerializeField]
    private string answerString ="";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        ResetQuestions();
    }

    public void ResetQuestions()
    {
        totalQuestionNum = this.transform.childCount;
        answerString = "";
        cQuestion = 1;
        this.transform.GetChild(cQuestion - 1).gameObject.SetActive(true);
    } 

    public void NextQuestion()
    {
        this.transform.GetChild(cQuestion-1).gameObject.SetActive(false);
        if (cQuestion < totalQuestionNum)
        {

            cQuestion++;
            this.transform.GetChild(cQuestion-1).gameObject.SetActive(true);
        }
        else
        {
            FinishQuestionnaire();
        }
    }

    public void RecordAnswer(string answer)
    {
        if (answer == null || answer.Length == 0) return;
        answerString += answer;
        NextQuestion();
    }
    
    public void FinishQuestionnaire()
    {
        OnSendQuestionnaireData?.Invoke(qType,GetQuestionnaireAnswer());
        OnFinishQuestionnaire?.Invoke();
        
    }

    public void ResendQuestionnaireData()
    {
        OnSendQuestionnaireData?.Invoke(qType, GetQuestionnaireAnswer());
    }

    public string GetQuestionnaireAnswer()
    {
        return answerString;
    }
}
