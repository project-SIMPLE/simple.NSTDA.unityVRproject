using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnlineModeGameManager : MonoBehaviour
{
    public static OnlineModeGameManager Instance { get; private set; }
    [SerializeField]
    private XRInteractionManager interactionManager;
    

    [SerializeField]
    private GameObject[] interactableItemAndTools;
    [SerializeField]
    //private GameObject Tutorial_Area;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
       // ActiveInteractableItemAndTools(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ActiveLocomotion(bool t)
    {
        interactableItemAndTools[3].SetActive(t);
    }
    
    private void ActiveInteractableItemAndTools(bool t)
    {
        //if (interactableItemAndTools == null) return;
        try{
            foreach(GameObject item in interactableItemAndTools)
            {
                //Debug.Log(item +""+ t);
                item.SetActive(t);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void StartTutorial()
    {
        //Tutorial_Area.GetComponent<TutorialManager>().TutorialStart();
        ActiveInteractableItemAndTools(true);
        TutorialStart();
    }

    public event Action OnTutorialStart;
    public void TutorialStart()
    {
        OnTutorialStart?.Invoke();
    }

    public event Action OnTutorialFinish;
    public void TutorialFinish()
    {
        if( OnTutorialFinish != null)
        {
            OnTutorialFinish();
        }
    }

    public event Action OnGameStart;
    public void GameStart()
    {
        Debug.Log("GAMA START");
        ActiveInteractableItemAndTools(true);
        
        if( OnGameStart != null )
        {
            OnGameStart();
        }
    }

    public event Action OnGameStop;
    public void GameStop()
    {
        Debug.Log("GAMA STOP");
        ActiveInteractableItemAndTools(false);
        if(OnGameStop != null )
        {
            OnGameStop();
        }
    }

    public event Action OnTutorialSeedCollect;
    public event Action<int> OnSeedCollected;
    public void SeedCollected(int seedID)
    {
        if(seedID == 0)
        {
            Debug.Log("collect ");
            if(OnTutorialSeedCollect != null)
            {
                Debug.Log("call Listioner");
                OnTutorialSeedCollect();
            }
        }
        else if (OnSeedCollected != null)
        {
            OnSeedCollected(seedID);
        }
    }

}
