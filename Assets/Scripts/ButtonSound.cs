using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
/*
    Adds all required scripts at runtime for each button to have
    a sound on both hover and click, eliminating the need for
    other developers to add and configure a script, AudioSource,
    and EventTrigger component.

    To use, all you need to do is drop the script onto the UI
    element and place the appropriate sound files in the variable
    fields.
*/
public class ButtonSound : MonoBehaviour{
    public AudioClip ButtonHover;
    public AudioClip ButtonClick;
    public void Start()
    {
        //Adds the ButtonSoundTrigger script to do my event handling for me.
        ButtonSoundTrigger trigger = transform.gameObject.AddComponent<ButtonSoundTrigger>();

        //Adds the AudioSource to the gameObject to play sounds.
        trigger.Source = trigger.gameObject.AddComponent<AudioSource>();

        //Duplicates the variable fields onto the Trigger.
        trigger.ButtonClickSound = ButtonClick;
        trigger.ButtonHoverSound = ButtonHover;
    }
}
