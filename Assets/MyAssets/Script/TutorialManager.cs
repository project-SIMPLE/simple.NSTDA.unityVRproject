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

    private void OnEnable()
    {
        if (!IsEventRegis)
        {
            Debug.Log("Regis!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            OnlineModeGameManager.Instance.OnTutorialSeedCollect += TutorialSeedCollect;
            IsEventRegis=true;
        }
        
    }
    private void OnDisable()
    {
        if (IsEventRegis)
        {
            OnlineModeGameManager.Instance.OnTutorialSeedCollect -= TutorialSeedCollect;
            IsEventRegis = false;
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
        if(SeedCount == 4)
        {
            Stage2Complete();
        }
    }
}