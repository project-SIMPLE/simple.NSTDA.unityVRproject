using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IVU2QuestionnaireManager 
{
    public event Action<string, string> OnFinishQuestionnaire;
    public void FinishQuestionnaire(string qType, string data);
    public void CollectQuestionnaireData(string qType, string data);
    public void GAMAReceieveQuestionnaireData();
    public void ResendQuestionnaireData(string type);
}
