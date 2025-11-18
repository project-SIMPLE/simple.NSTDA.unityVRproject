using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VU2TutorialControl : MonoBehaviour
{
    public UnityEvent OnFinishTutorial;
    public UnityEvent OnFinishResultAnimation;

    // Forest Degrading
    // 
    public UnityEvent OnFinishIntroAnimation;

    // Charactor planting seedling
    //
    public UnityEvent OnFinishTutorialIntroAnimation;

    [SerializeField]
    private GameObject tutorialObj;
    /*[SerializeField]
    private GameObject plantableSeeding;
    [SerializeField]
    private Transform[] seedingSpawnPoint;
    */
    [SerializeField]
    private GameObject[] ForestIntroAnimationObj;
    [SerializeField]
    private GameObject CharactorPlantingAnimationObj;
    [SerializeField]
    private GameObject SeedlingObj;

    [SerializeField]
    private GameObject AfterGameAnimation;

    

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnTutorialStart -= BeginTutorial;
    }
    // Start is called before the first frame update
    void Start()
    {
        VU2ForestProtectionEventManager.Instance.OnTutorialStart += BeginTutorial;
        SetToInitialState();
    }

    private void SetToInitialState()
    {
        cTutorialStep = 0;
        tutorialStage = 0;
        SeedlingObj.SetActive(false);
        //ForestIntroAnimationObj[0].SetActive(true);
    }

    [SerializeField]
    private int cTutorialStep;
    [SerializeField]
    private int maxTutorialStep;

    [SerializeField]
    private GameObject[] Stage;
    private int tutorialStage = 0;

    private void BeginTutorial()
    {
        SetToInitialState();
        ChangeTutorialStep(cTutorialStep);
    }
    /*
     * Tutorial State
     * 0 Animation
     * 1 plant trees
     * 2 add fertilizer
     * 3 threat
     * 
     */
    public void ChangeTutorialStep(int state)
    {
        
        switch (state)
        {
            case 0:
                PlayForestDegradeAnimation();
                break;
            case 1:
                StartTutorialIntroAnimation();
                break;
            case 2:
                StartTutorial();
                break;
            case 3:
                break;
        }
    }
    public void ChangeTutorialToNextStep()
    {
        cTutorialStep++;
        if (cTutorialStep > maxTutorialStep)
        {
            FinishTutorial();
            return;
        }
        ChangeTutorialStep(cTutorialStep);
    }


    private float timePass = 10.0f;
    private void PlayForestDegradeAnimation()
    {
        ForestIntroAnimationObj[0].SetActive(false);
        ForestIntroAnimationObj[1].SetActive(true);
        //Invoke("StopBGAnimation", timePass);
        Invoke("FinishIntroAnimation", timePass);
    }
    private void FinishIntroAnimation()
    {
        //ForestIntroAnimationObj.SetActive(false);
        OnFinishIntroAnimation?.Invoke();
    }

    public void StartTutorialIntroAnimation() 
    {
        ForestIntroAnimationObj[1].SetActive(false);
        CharactorPlantingAnimationObj.SetActive(true);
        Invoke("FinishTutorialIntroAnimation",17f);
    }

    private void FinishTutorialIntroAnimation()
    {
        SeedlingObj.SetActive(true);
        CharactorPlantingAnimationObj.SetActive(false);
        OnFinishTutorialIntroAnimation?.Invoke();
        ChangeTutorialToNextStep();
    }

    private void StopBGAnimation()
    {
        //ForestIntroAnimationObj.SetActive (false);
        //OnFinishIntroAnimation?.Invoke();
        ChangeTutorialToNextStep();
    }
    /*private void CallFunctionAfterTimePass(string functionName, float timePass)
    {
        
        MethodInfo method = this.GetComponent<VU2TutorialControl>().GetType().GetMethod(functionName, BindingFlags.Public | BindingFlags.NonPublic);
        if(method != null)
        {
            Invoke(functionName, timePass);
        }
    }*/

    private void StartTutorial()
    {
        tutorialObj.SetActive (true);
        ActiveTutorialStage(tutorialStage);

        //plantableSeeding.transform.position = seedingSpawnPoint.position;
        /*
                seedlingLists = new List<GameObject> ();
                foreach (Transform t in seedingSpawnPoint)
                {
                    GameObject tmp = Instantiate(plantableSeeding, t.position, t.rotation);
                    seedlingLists.Add(tmp);
                }*/
    }
    public void FinishCurrentStage()
    {
        tutorialStage++;
        if (tutorialStage >= Stage.Count())
        {
            ChangeTutorialToNextStep();
        }
        else
        {
            ActiveTutorialStage(tutorialStage);
        }
    }
    private void ActiveTutorialStage(int i)
    {
        if (Stage[i] != null)
        {
            Stage[i].SetActive(true);
            Stage[i].GetComponent<VU2TutorialStage>()?.BeginStage();
        }
    }

    private void FinishTutorial()
    {
        tutorialObj.SetActive(false);
        OnFinishTutorial?.Invoke();
    }
    /*
     * 40 -> -50
     * 41 -> 110
     * 111 -> 150
     * */


    private GameObject resultAni;
    public void ShowResult()
    {
        /*int index = VU2ForestProtectionEventManager.Instance.GetBGStage();
        if(index <0 || index> AfterGameAnimation.transform.childCount)
        {
            index = 1;
        }*/
        int index;
        int score;
        if (int.TryParse(VU2ForestProtectionEventManager.Instance.GetPlayerScore(), out score))
        {
            if(score <= 49)
            {
                index = 0;
            }else if(score >49 && score <= 74){
                index = 1;
            }
            else
            {
                index = 2;
            }
        }
        else
        {
            index = 0;
        }

        resultAni = AfterGameAnimation.transform.GetChild(index).gameObject;
        resultAni?.SetActive (true);
        VU2BGSoundManager.Instance?.PlayEndingBGSFX(index);
        Invoke("FinishResultAni", 20f);
    }
    private void FinishResultAni()
    {
        resultAni?.SetActive (false);
        OnFinishResultAnimation?.Invoke();
    }

}
