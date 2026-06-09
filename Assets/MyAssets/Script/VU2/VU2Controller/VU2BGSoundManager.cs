using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VU2BGSoundManager : MonoBehaviour
{
    public static VU2BGSoundManager Instance { get; private set; }

    [Header("----Audio Source----")]
    [SerializeField]
    private AudioSource camAudioSource;
    [SerializeField]
    private AudioSource environmentSource;
    [SerializeField]
    private AudioSource localAudioSource;
    /*[SerializeField]
    private AudioSource IntroAudioSource;*/

    [Header("----Audio Clip----")]
    [SerializeField]
    private AudioClip s_Fire;
    [SerializeField]
    private AudioClip s_Thunder;
    [SerializeField]
    private AudioClip s_Rain;

    [Header("----Audio Annouance----")]
    [SerializeField]
    private AudioClip s_1MinRemained;

    [Header("----Tree Audio----")]
    [SerializeField]
    private AudioClip s_helpMe;
    [SerializeField]
    private AudioClip s_wilhelmScream;

    [Header("----Ending BG Audio ----")]
    /// 1 Worst
    /// 2 Normal
    /// 3 Best
    [SerializeField]

    private AudioClip[] s_Endings;

    [Header("----Coin SFX ----")]
    [SerializeField]
    private AudioClip s_Coin;

    [Header("----Cutting SFX ----")]
    [SerializeField]
    private AudioClip s_Cutting;

    [Header("----BG SFX ----")]
    [SerializeField]
    private AudioClip BGdecrease;
    [SerializeField]
    private AudioClip BGLevel2;
    [SerializeField]
    private AudioClip BGLevel3;

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
    }
    private void Start()
    {
        VU2ForestProtectionEventManager.Instance.OnGameStop += StopAllSFX;
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += PlayRainSFX;
        VU2ForestProtectionEventManager.Instance.OnUpdateFireEffect += PlayFireSFX;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnGameStop -= StopAllSFX;
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= PlayRainSFX;
        VU2ForestProtectionEventManager.Instance.OnUpdateFireEffect -= PlayFireSFX;
    }
    public void PlayCoinSFX()
    {
        PlaySoundOnce(s_Coin);
    }
    public void PlayCutSoundSFX()
    {
        PlaySoundOnce(s_Cutting);
    }

    public void StopAllSFX()
    {
        StopAllCoroutines();
        if(camAudioSource.isPlaying) camAudioSource.Stop();
        if(localAudioSource.isPlaying) localAudioSource.Stop();
        isLocalPlaying = false;
    }

    public void PlayEndingBGSFX(int index)
    {
        if (s_Endings[index] == null) return;
        camAudioSource.PlayOneShot(s_Endings[index]);
    }

    /*
     * -1 BG down
     * 2 BG2
     * 3 BG3
     *
     * */

    public void PlayBGChangeSFX(int BG)
    {
        if (environmentSource.isPlaying)
        {
            environmentSource.Stop();  
        }
        AudioClip tmpAC;
        switch (BG)
        {
            case -1:
                tmpAC = BGdecrease;
                break;
            case 2:
                tmpAC = BGLevel2;
                break;
            case 3:
                tmpAC = BGLevel3;
                break;
            default:
                tmpAC = BGdecrease;
                break;
        }
        environmentSource.PlayOneShot(tmpAC);
    }

    public void PlayFireSFX(bool play)
    {
        //Debug.Log("Play fire SFX");
        //StopAllCoroutines();
        StopCoroutine(ThunderAndRainSoundCoroutine());
        if (play)
        {
            camAudioSource.clip = s_Fire;
            camAudioSource.loop = true; 
            camAudioSource.Play();
        }
        else
        {
            camAudioSource.Stop();
        }
    }

    public void PlayRainSFX(bool play)
    {
        if (play)
        {
            StartCoroutine(ThunderAndRainSoundCoroutine());
        }
        else
        {
            //StopAllCoroutines();
            StopCoroutine(ThunderAndRainSoundCoroutine());
            camAudioSource.Stop();
        }
    }
    
    public void AnnounceOneMinRemained()
    {
        camAudioSource.PlayOneShot(s_1MinRemained);
    }

    IEnumerator ThunderAndRainSoundCoroutine()
    {
        PlaySoundOnce(s_Thunder);
        yield return new WaitForSeconds(s_Thunder.length);
        PlaySoundLoop(s_Rain);

    }
    private void PlaySoundOnce(AudioClip audC)
    {
        //camAudioSource.clip = audC;
        camAudioSource.PlayOneShot(audC);
    }
    private void PlaySoundLoop(AudioClip audC)
    {
        camAudioSource.clip = audC;
        camAudioSource.loop = true;
        camAudioSource.Play();
    }
    [SerializeField]
    private bool isLocalPlaying = false;
    [SerializeField]
    private float clipLength = 0f;



    //private List<GameObject> cAudioObjects;
    public void PlayTreeSoundEffect(GameObject tree,int type)
    {
        //if (cAudioObjects.Contains(tree)) return;

        //cAudioObjects.Add(tree);
        AudioSource audioS = Instantiate(localAudioSource, tree.transform.position, Quaternion.identity);
        switch (type)
        {
            /// Help ME
            case 0:
                audioS.clip = s_helpMe;
                clipLength = s_helpMe.length;
                break;
            /// Wilhlem 
            case 1:
                audioS.clip = s_wilhelmScream;
                clipLength = s_wilhelmScream.length;
                break;
            case 2:
                audioS.clip = s_Cutting;
                clipLength = s_Cutting.length;
                break;
        }
        
        audioS.Play();
        //StartCoroutine(RemoveAuidoSourceAfter(audioS.gameObject, tree, clipLength));
        Destroy(audioS.gameObject, clipLength);

        /*if (!isLocalPlaying)
        {
            switch (type)
            {
                /// Help ME
                case 0:
                    localAudioSource.clip = s_helpMe;
                    clipLength = 4f;
                    break;
                /// Wilhlem 
                case 1:
                    localAudioSource.clip = s_wilhelmScream;
                    clipLength = 1.5f;
                    break;
            }
            localAudioSource.gameObject.transform.position = tree.transform.position;
            localAudioSource.Play();
            StartCoroutine(LocalTreeSoundEffectCoroutine());
        }*/


    }
    /*private void RemoveAudioObject(GameObject audObj, GameObject tree)
    {
        Destroy(audObj);
        cAudioObjects.Remove(tree);
    }*/
    
    IEnumerator RemoveAuidoSourceAfter(GameObject audObj, GameObject tree,float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(audObj);
        //cAudioObjects.Remove(tree);
    }

    IEnumerator LocalTreeSoundEffectCoroutine()
    {
        isLocalPlaying = true;
        yield return new WaitForSeconds(clipLength);
        isLocalPlaying = false;
    }
}
