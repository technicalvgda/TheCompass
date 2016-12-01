using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class AnimEventPlayUISound : MonoBehaviour
{
    public UIAudio source;

    const int BUTTON_HIGHLIGHTED = 0;
    const int BUTTON_PRESSED = 1;

    void PlayReservedBGM() { source.PlayBGM(true); }

    void PlayButtonHighlighted() { source.PlaySE(BUTTON_HIGHLIGHTED); }
    void PlayButtonPressed() { source.PlaySE(BUTTON_PRESSED); }
}
