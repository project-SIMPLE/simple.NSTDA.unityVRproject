using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallFunctionAfterTimePass : MonoBehaviour
{
    [SerializeField]
    private float timePass;
    public UnityEvent OnCallFunctionAfterTimePass;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        Invoke("CallFunction", timePass);
    }

    public void CallFunction()
    {
        OnCallFunctionAfterTimePass?.Invoke();
    }
}
