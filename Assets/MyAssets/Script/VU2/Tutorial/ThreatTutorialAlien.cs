using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThreatTutorialAlien : MonoBehaviour
{
    public UnityEvent OnTaskFinish;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetToInitialState()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Tools")
        {
            KillWeed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tools")
        {
            KillWeed();
        }
    }
    public void KillWeed()
    {
        OnTaskFinish?.Invoke();
        this.gameObject.SetActive(false);
    }
}
