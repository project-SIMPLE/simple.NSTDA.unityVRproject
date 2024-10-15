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
        ActiveInteractableItemAndTools(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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


    public event Action OnGameStart;
    public void GameStart()
    {
        Debug.Log("GAMA START");
        ActiveInteractableItemAndTools(true);
        
    }

    public event Action OnGameStop;
    public void GameStop()
    {
        Debug.Log("GAMA STOP");
        ActiveInteractableItemAndTools(false);
    }

    public event Action<int> OnSeedCollected;
    public void SeedCollected(int seedID)
    {
        
        if (OnSeedCollected != null)
        {
            OnSeedCollected(seedID);
        }
    }

}
