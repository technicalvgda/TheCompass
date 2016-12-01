using UnityEngine;
using System.Collections;

public class AnimEventPlayClip : MonoBehaviour
{
    public UIAudio source;
    public AudioClip[] clips;

    void PlayBGM(int index)
    {
        if (clips[index] != null)
        {
            source.PlayBGM(clips[index], true);
        }
    }

    void PlaySE(int index)
    {
        if (clips[index] != null)
        {
            source.PlaySE(clips[index]);
        }
    }
}
