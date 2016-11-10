using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class UINavigation : MonoBehaviour, IPointerEnterHandler
{
    public bool first;

    Animator anim;
    int hashNormal;

    void Start()
    {
        anim = this.GetComponent<Animator>();

        //hashNormal = animator.GetParameter(0).nameHash;
        // don't call this for every UI element

        //"Normal" = 640249298
        //"Enter" = 2024927082
        //"menuFrameExit" = 2053353468
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIHelper.SetSelected(gameObject);
    }

    void Update()
    {
        if (UIHelper.isButtonNavigation)
        {
            if (UIHelper.currentSelected == null)
            {
                if (first)
                    UIHelper.SetSelected(gameObject);
            }

            // An object becomes VISUALLY highlighted when it's moused over, regardless of
            // whether it's actually being selected. If a directional button is pressed
            // during this, there are now two highlighted objects: the real selected object
            // and the object underneath the mouse pointer.
            //
            // This code corrects each object's visual state

            else if (UIHelper.currentSelected != gameObject)
            {
                if (anim != null)
                    anim.SetTrigger(640249298);
            }
        }
    }

}
