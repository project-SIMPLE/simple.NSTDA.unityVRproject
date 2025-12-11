using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Hitable : MonoBehaviour
{
    [SerializeField]
    string hitObjectTag = "AXE";

    [SerializeField]
    GameObject parentObject;

    //[SerializeField]
    //private ParticleSystem pSystem;
    //[SerializeField]
    //private AudioSource pSource;

    

    //[SerializeField]
    //private int _Hardness = 5;

    //[SerializeField]
    //private int currentHardness;

    private bool coolDownOn = false;

    //private void Start()
    //{
    //    currentHardness = _Hardness;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        
        if (coolDownOn) return;
        if (collision.gameObject.tag.Equals(hitObjectTag,System.StringComparison.InvariantCultureIgnoreCase))
        {
            ActiveHit();
        }
        
    }
    public void ActiveHit()
    {
        StartCoroutine(HitDelay());
    }

    IEnumerator HitDelay()
    {
        coolDownOn = true;
        if(parentObject!= null)
        {
            parentObject.GetComponent<CutableObject>().ReduceHardness();
        }
        yield return new WaitForSeconds(0.7f);
        coolDownOn = false;
    }
    /*
    public void ReduceHardness()
    {
        
        currentHardness--;
        if (currentHardness <= 0) Broken();
    }

    private void Broken()
    {
        this.gameObject.SetActive(false);
    }
    public void PlayFeedbackEffecct()
    {
        
        pSource.Play();
        pSystem.Play();
    }
    */
}
