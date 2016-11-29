using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

/// <summary>
/// Logic to handle object selection when a UI element is moused over.
/// Per object, attach this script to individual buttons/sliders.
/// </summary>

[RequireComponent (typeof(Selectable))]
public class UINavigationPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static GameObject mousedOver;
    Selectable sel;

    //void Start()
    void Awake()
    {
        sel = GetComponent<Selectable>();
        //Debug.Log(gameObject + " initialized");
    }

    // An object that is being moused over plays the Highlighted animator/sprite state,
    // but does not appear as selected in EventSystem. This needs to be done manually.

    public void OnPointerEnter(PointerEventData eventData)
    {
        // If this line is enabled, the player is prevented from selecting an UI element
        // until it has finished transitioning in. But they are able to make two buttons
        // light up at the same time by doing the following sequence:
        //   Bring up a menu -> Hover on a button -> Press left/right. The menu's first
        //   button and the button underneath the mouse cursor will both light up.
        // The button underneath the mouse cursor is cursor-highlighted but not selected,
        // and pressing Space/Enter/Confirm on it will not have any effect.

        // If this line is disabled, the player will be able to preemptively select an
        // UI element during its enter transition, but the sequence above will not work.
        //   Bring up a menu -> Hover on a button then leave it at any point during the
        //   transition. The button will be selected at the end of the transition.
        // The button underneath the mouse cursor is selected, and pressing Space/Enter/
        // Confirm will fire its OnClick() event.

        // tl;dr fuck PC

        //if (!sel.IsInteractable()) return;

        mousedOver = gameObject;

        // (SOLVED) The program occasionally coughs up a NullRefException around this part.

        // Reproduction:
        // Enter any menu while LMB is held.
        // The button in the subsequent menu at the mouse position will throw the error.
        // This can be done by holding LMB over a button while pressing the button with Enter.

        // Cause:
        // Menus other than the initial (and subsequently their buttons) start out disabled,
        // and sel was never initialized by the time it is moused over.
        // This only happens once per menu on the very first frame it is enabled.

        // Solution:
        // Move initialization from Start() to Awake().
        // Attach the Late Disable script to every menu that isn't the initial menu,
        // then enable them all by default.
        // Since Start() executes following Awake(), objects are initialized then disabled.

        if (sel == null)
            Debug.Log("NULL detected in " + gameObject);

        sel.Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Bad last minute hack

        ResetMousedOver();
        mousedOver = null;
    }

    /// <summary>
    /// Corrects a behavior where an object becomes visually highlighted when it is
    /// moused over, regardless of whether it is actually being selected. <para/>
    /// If a directional button/axis input occurs during this, two objects now appear
    /// highlighted: the next in the navigation graph and the one underneath the pointer.
    /// </summary>

    public static void ResetMousedOver()
    {
        if (mousedOver != null && UIHelper.selected != null &&
            UIHelper.selected != mousedOver)
        {
            // Sets the object's visual state to Normal.
            // Requires an auto-generated Animator, otherwise the problem persists.

            Animator anim = mousedOver.GetComponent<Animator>();
            if (anim != null && anim.isInitialized)
                anim.SetTrigger(640249298); // = "Normal"

            // + Avoids hardcoding the trigger hash
            // - Passes by string
            //Selectable sel = mousedOver.GetComponent<Selectable>();
            //Animator anim = sel.animator;
            //if (anim != null && anim.isInitialized)
            //    anim.SetTrigger(sel.animationTriggers.normalTrigger);
        }
    }

}
