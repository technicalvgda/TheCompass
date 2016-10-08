using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/*
    I make this Script an EventTrigger so that I can
    get access to the OnPointerEnter() and OnPointerClick()
    functions to override them.

    ***Do not add this script manually!
*/
public class ButtonSoundTrigger : EventTrigger
{
    public AudioClip ButtonHoverSound;
    public AudioClip ButtonClickSound;
    public AudioSource Source;
    /*
    This script runs automatically whenever the mouse hovers over the UI
    object. It plays an instance of the hover sound (can be run simultaneously
    with other sounds)
    */
    public override void OnPointerEnter(PointerEventData eventData)
    {
        Source.PlayOneShot(ButtonHoverSound, 1f);
    }
    /*
    This script runs automatically when the mouse is clicked on the UI
    object. It plays an instance of the click sound (can be run simultaneously
    with other sounds)
    */
    public override void OnPointerClick(PointerEventData eventData)
    {
        Source.PlayOneShot(ButtonClickSound, 1f);
    }
}
