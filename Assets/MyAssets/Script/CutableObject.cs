using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultObj;
    [SerializeField]
    private GameObject brokenObj;


    [SerializeField]
    private ParticleSystem pSystem;
    [SerializeField]
    private AudioSource aSound;
    [SerializeField]
    private AudioClip finishAudio;

    [SerializeField]
    private int _Hardness = 5;

    [SerializeField]
    private int currentHardness;

    // Start is called before the first frame update
    void Start()
    {
        currentHardness = _Hardness;
    }



    public void ReduceHardness()
    {
        
        PlayFeedbackEffecct();
        currentHardness--;
        if (currentHardness <= 0)
        {
            StartCoroutine(Broken());
        }
        CallHUDInfo();
    }
    public void CallHUDInfo()
    {
        HUDController.HUDInstance.ShowInfoPannel(this.name, currentHardness, _Hardness);
    }

    IEnumerator Broken()
    {
        defaultObj.SetActive(false);
        brokenObj.SetActive(true);
        //TestbedManager.instance.IncreaseTreeScore(1);
        aSound.clip = finishAudio;
        aSound.Play();
        yield return new WaitForSeconds(20f);
        brokenObj.SetActive(false);
    }

    //private void Broken()
    //{
    //    defaultObj.SetActive(false);
    //    brokenObj.SetActive(true);

    //    aSound.clip = finishAudio;
    //    aSound.Play();
    //}
    public void PlayFeedbackEffecct()
    {
        aSound.Play();
        pSystem.Play();
    }
}
