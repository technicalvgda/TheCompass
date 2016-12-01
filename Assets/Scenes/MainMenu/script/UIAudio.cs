using UnityEngine;
using System.Collections;

public class UIAudio : MonoBehaviour
{
    public AudioSource reservedBGM;
    public AudioSource[] reservedSE;
    public AudioSource[] freeSE;

    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetBGMVolume(float value)
    {
        reservedBGM.volume = value;
    }

    public void SetSEVolume(float value)
    {
        for (int i = 0; i < reservedSE.Length; i++)
        {
            if (reservedSE[i] != null)
                reservedSE[i].volume = value;
        }

        for (int i = 0; i < freeSE.Length; i++)
        {
            if (freeSE[i] != null)
                freeSE[i].volume = value;
        }
    }

    public void PlayBGM(bool loop)
    {
        if (reservedBGM.isPlaying)
        {
            reservedBGM.Stop();
        }
        reservedBGM.loop = loop;
        reservedBGM.Play();
    }

    public void PlayBGM(AudioClip clip, bool loop)
    {
        if (reservedBGM.isPlaying)
        {
            reservedBGM.Stop();
        }
        reservedBGM.clip = clip;
        reservedBGM.loop = loop;
        reservedBGM.Play();
    }

    public void PlaySE(int index)
    {
        if (reservedSE[index].isPlaying)
        {
            reservedSE[index].Stop();
        }
        reservedSE[index].Play();

        // Instantiates an Audio Source and destroys it 4u
        // Produces garbage

        //reservedSE[index].PlayOneShot(reservedSE[index].clip);
    }

    public void PlaySE(AudioClip clip)
    {
        for (int i = 0; i < freeSE.Length; i++)
        {
            if (!freeSE[i].isPlaying)
            {
                freeSE[i].clip = clip;
                freeSE[i].Play();
                return;
            }
        }
        // If all sources are busy, no sound will be played
        // Next time use a linked list instead
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
