using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEnable : MonoBehaviour
{
    [SerializeField]
    private GameObject delayActiveObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Invoke("StartDelayActiveGameObjet",20f);
    }
    private void OnDisable()
    {
        if(IsInvoking("StartDelayActiveGameObjet")) CancelInvoke("StartDelayActiveGameObjet");
        if(delayActiveObj.activeSelf) delayActiveObj.SetActive(false);
    }
    private void StartDelayActiveGameObjet()
    {
        delayActiveObj.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
