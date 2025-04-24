using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject stage1;
    [SerializeField] 
    private GameObject stage2;
    [SerializeField]
    private GameObject[] interactableItem;
    [SerializeField]
    private int SeedCount = 0;
    private bool IsEventRegis = false;
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private GameObject TutorialFinishText;

    [SerializeField]
    private Animator DoorAnimationControl;

    [SerializeField]
    private Animator Dooe2AnimationControl;

    private void OnEnable()
    {
        if (!IsEventRegis)
        {
            Debug.Log("Regis!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            OnlineModeGameManager.Instance.OnTutorialStart += TutorialStart;
            OnlineModeGameManager.Instance.OnTutorialSeedCollect += TutorialSeedCollect;
            IsEventRegis=true;
            OnlineModeGameManager.Instance.OnTutorialFinish += TutorialFinish;
            OnlineModeGameManager.Instance.OnGameStart += GameStart;
        }
        
    }
    private void OnDisable()
    {
        if (IsEventRegis)
        {

            OnlineModeGameManager.Instance.OnTutorialStart -= TutorialStart;
            OnlineModeGameManager.Instance.OnTutorialSeedCollect -= TutorialSeedCollect;
            IsEventRegis = false;

            OnlineModeGameManager.Instance.OnTutorialFinish -= TutorialFinish;
            OnlineModeGameManager.Instance.OnGameStart -= GameStart;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TutorialStart()
    {
        stage1.SetActive(true);
        wall.SetActive(true);
        TutorialFinishText.SetActive(false);
    }
    public void TutorialFinish()
    {
        TutorialFinishText.SetActive(true);
    }
    public void GameStart()
    {
        TutorialFinishText.SetActive(false);
        wall.SetActive(false);
    }
    public void ResetTutorial()
    {
        stage1.SetActive(true);
        stage1.SetActive(false);
        SeedCount = 0;
    }
    public void Stage1Completed()
    {
        stage1.SetActive(false);
        
    }
    public void StartStage2()
    {
        stage2.SetActive(true);
        SeedCount = 0;

        foreach (GameObject child in interactableItem)
        {
            child.SetActive(true);
        }
    }
    public void Stage2Complete()
    {
        stage2.SetActive(false);
        OnlineModeGameManager.Instance.TutorialFinish();
        
    }
    
    private void TutorialSeedCollect()
    {
        Debug.Log("Mango");
        SeedCount++;
        if(SeedCount == 2)
        {
            SetDoor2Animation(true);
        }
        if(SeedCount == 4)
        {
            SetDoor2Animation(false);
            Stage2Complete();
        }
    }
    public void SetDoorAnimation(bool t)
    {
        if (DoorAnimationControl == null) return;
        DoorAnimationControl.SetBool("Open", t);
    }
    public void SetDoor2Animation(bool t)
    {
        if (Dooe2AnimationControl == null) return;
        Dooe2AnimationControl.SetBool("Open", t);
    }
}
