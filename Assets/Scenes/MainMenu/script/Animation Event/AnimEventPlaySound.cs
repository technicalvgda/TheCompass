using UnityEngine;
using System.Collections;

public class AnimEventPlaySound : MonoBehaviour
{
    public UIAudio source;

    void PlayBGM()
    {
        source.PlayBGM(true);
    }

    void PlaySE(int index)
    {
        source.PlaySE(index);
    }
}
