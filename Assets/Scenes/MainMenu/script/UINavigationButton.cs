using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Logic to handle object selection when a directional button/axis is detected
/// or the Cancel key/button is pressed. Per menu, attach this script to submenu objects.
/// </summary>

[RequireComponent(typeof(CanvasGroup))]
public class UINavigationButton : MonoBehaviour
{
    public Button back;
    public Selectable first;
    CanvasGroup cg;

    void Start()
    {
        cg = this.GetComponent<CanvasGroup>();
    }
    
    void Update()
    {
        if (!cg.interactable) return;

        if (UIHelper.isButtonNavigation)
        {
            // If directional key/axis is detected and nothing is selected,
            // selects the designated first element. Otherwise un-highlights
            // the object being moused over so that there is only one
            // highlighted object in the scene. See the function's summary.

            if (UIHelper.selected == null)
                first.Select();
            else
                UINavigationPointer.ResetMousedOver();
        }

        else if (UIHelper.isBackPressed && back != null)
        {
            if (UIHelper.selected == back.gameObject)
                back.onClick.Invoke();
            else
            {
                back.Select();

                // Unlike with directional navigation, pressing the Cancel key/button
                // will not prevent the back button and the element being moused over
                // from being highlighted at the same time, even when that element is
                // told to leave the Highlighted state.

                // Adding this here completely snuffs out the problem, at the expense of
                // one coroutine produced on the frame the Cancel button is pressed.

                StartCoroutine(_Hack());
            }
        }
    }

    IEnumerator _Hack()
    {
        yield return null; UINavigationPointer.ResetMousedOver();
        yield return null; UINavigationPointer.ResetMousedOver();
    }
}
