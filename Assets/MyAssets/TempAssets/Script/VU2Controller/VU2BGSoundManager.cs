using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VU2BGSoundManager : MonoBehaviour
{
    public static VU2BGSoundManager Instance { get; private set; }

    [Header("----Audio Source----")]
    [SerializeField]
    private AudioSource camAudioSource;
    [SerializeField]
    private AudioSource localAudioSource;

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
        
    }

    public void PlayFireSFX(bool play)
    {

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

    public void AnnouanceOneMinRemained()
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
    public void PlayTreeSoundEffect(GameObject tree,int type)
    {
        if (!isLocalPlaying)
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
                    clipLength = s_wilhelmScream.length;
                    break;
            }
            localAudioSource.gameObject.transform.position = tree.transform.position;
            localAudioSource.Play();
            StartCoroutine(LocalTreeSoundEffectCoroutine());
        }
    }

    
    IEnumerator LocalTreeSoundEffectCoroutine()
    {
        isLocalPlaying = true;
        yield return new WaitForSeconds(clipLength);
        isLocalPlaying = false;
    }
}
