using UnityEngine;
using System.Collections;

public class UIAudio : MonoBehaviour
{
    public AudioSource sourceBGM;
    public AudioSource[] sourceSE;
    public AudioClip[] clips;

    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetBGMVolume(float value)
    {
        sourceBGM.volume = value;
    }

    public void SetSEVolume(float value)
    {
        for (int i = 0; i < sourceSE.Length; i++)
        {
            if (sourceSE[i] != null)
                sourceSE[i].volume = value;
        }
    }

    public void PlayBGM(AudioClip clip, bool loop)
    {
        if (sourceBGM.isPlaying)
        {
            sourceBGM.Stop();
        }
        sourceBGM.clip = clip;
        sourceBGM.loop = loop;
        sourceBGM.Play();
    }

    public void PlayBGM(bool loop)
    {
        if (sourceBGM.isPlaying)
        {
            sourceBGM.Stop();
        }
        sourceBGM.loop = loop;
        sourceBGM.Play();
    }

    // PlayOneShot instantiates an Audio Source and destroys it 4U
    // Produces garbage. Do not use it especially for repeated sound effects

    // Crude pooling that iterates every time and picks out an unused source
    // Ideally a linked list would be used for O(1)

    public void PlaySE(AudioClip clip)
    {
        for (int i = 0; i < sourceSE.Length; i++)
        {
            if (!sourceSE[i].isPlaying)
            {
                sourceSE[i].clip = clip;
                sourceSE[i].Play();
                return;
            }
        }
        // If all sources are busy, no sound will be played
        // Maybe force the oldest source to play instead
    }

    public void PlaySE(int index)
    {
        PlaySE(clips[index]);
    }

    //void Awake()
    //{
    //    sourceBGM = (AudioSource)Instantiate(baseBGM, transform);
    //    sourceBGM.name = "Source BGM";

    //    sourceSE = new AudioSource[10];
    //    for (int i = 0; i < sourceSE.Length; i++)
    //    {
    //        sourceSE[i] = (AudioSource)Instantiate(baseSE, transform);
    //        sourceSE[i].name = "Source SE " + i;
    //    }

    //    Destroy(baseBGM.gameObject);
    //    Destroy(baseSE.gameObject);
    //}
}
