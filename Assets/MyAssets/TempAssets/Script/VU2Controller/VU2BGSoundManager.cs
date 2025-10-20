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
        
        StopAllCoroutines();
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
            StopAllCoroutines();
            camAudioSource.Stop();
        }
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
}
