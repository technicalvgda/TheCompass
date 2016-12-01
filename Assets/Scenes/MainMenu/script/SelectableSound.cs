using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class SelectableSound : MonoBehaviour, ISubmitHandler//, ICancelHandler, ISelectHandler // buggy as HELL
{
    public UIAudio source;

    const int SELECT = 0;
    const int SUBMIT = 1;

    //Selectable sel;

    //void Awake()
    //{
    //    sel = GetComponent<Selectable>();
    //}

    void PlayAssignedBGM() { source.PlayBGM(true); }

    void PlaySelect() { source.PlaySE(SELECT); }
    void PlaySubmit() { source.PlaySE(SUBMIT); }

    //public void OnSelect(BaseEventData eventData)
    //{
    //    if (sel.IsInteractable())
    //    {
    //        //Debug.Log(gameObject.name + "Selected");
    //        PlaySelect();
    //    }
    //}

    public void OnSubmit(BaseEventData eventData)
    {
        // Interactable check isn't needed, objects firing submit are uninteractable

        //Debug.Log(gameObject.name + "Submit");
        PlaySubmit();
    }

    //public void OnCancel(BaseEventData eventData)
    //{
    //    if (sel.IsInteractable())
    //    {
    //        //Debug.Log(gameObject.name + "Cancel");
    //        PlaySubmit();
    //    }
    //}
}
