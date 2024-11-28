using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




public class AreaTrigger : MonoBehaviour
{
    [SerializeField]
    public UnityEvent OnTriggerCustom;

    [SerializeField]
    public UnityEvent OnExitCustom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("Player Walk in");
            OnTriggerCustom.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("Player Walk in");
            OnExitCustom.Invoke();

        }
    }


}
