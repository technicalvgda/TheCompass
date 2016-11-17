using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMenu : MonoBehaviour
{
    static Stack menuStack;
    CanvasGroup cg;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();

        if (menuStack == null)
            menuStack = new Stack();

        //Hide();
    }

    // delet this
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            while (menuStack.Count > 0)
            {
                Debug.Log(menuStack.Pop());
            }
        }
    }

    // Transition sequence
    //      current   ------>    next
    //    Exit State          Enter State
    //   (Push / Pop)       Unhide | enable
    // (Hide | disable)

    // pushCurrent: Pushes the current menu to the menu stack. Back buttons shouldn't push.
    // overlay: Overlays the next menu on top of the current menu and takes control away from it.

    public void Transition(bool pushCurrent, bool overlay, UIMenu next, int exitHash, int enterHash)
    {
        NavigationVoodoo();

        if (pushCurrent)
            menuStack.Push(this);

        if (overlay)
            Enter(next, enterHash);
        else
            StartCoroutine(_Transition(next, exitHash, enterHash));
    }

    IEnumerator _Transition(UIMenu next, int exitHash, int enterHash)
    {
        Animator anim = GetComponent<Animator>();
        if (anim.HasState(0, exitHash))
        {
            anim.SetTrigger(exitHash);

            // Wait one frame before getting the Animation State Info,
            // otherwise you end up with the one from the current state.

            do
                yield return null;
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

            // The exit state must not transition to any other state,
            // otherwise normalizedTime resets and the yield loop repeats.
        }

        //Hide();
        gameObject.SetActive(false);
        Enter(next, enterHash);
    }

    void Enter(UIMenu next, int enterHash)
    {
        // This code can cause stacking of multiple submenus. If the target was disabled
        // by the time the transition away animation finishes, it will be brought back.
        // To avoid this, make sure the target is not interactable for the entirety
        // of the transition away animation.

        if (next != null)
        {
            if (next.isActiveAndEnabled)

                // This line by itself allows plinking on the frame the target menu is enabled.
                // Do not bring it outside of this conditional. What this conditional does is
                // restore interactability to menus coming from an overlay menu. Regular menus
                // already have this method called in their Enter animation clip.

                next.EnableCanvasGroupInteractable();
            else
            {
                next.gameObject.SetActive(true);

                Animator nextAnim = next.GetComponent<Animator>();
                if (nextAnim.HasState(0, enterHash))
                    nextAnim.SetTrigger(enterHash);
            }
        }

        // Implementation using hiding. The drawback is all child objects' animations have
        // to be manually triggered, whereas enabling the parent object does this by design.

        //if (next != null)
        //{
        //    if (next.hidden &&
        //        next.anim != null &&
        //        next.anim.HasState(0, enterHash))
        //        next.anim.SetTrigger(enterHash);
        //
        //    next.Unhide();
        //}
    }

    public void Back(int exitHash, int enterHash)
    {
        Transition(false, false, (UIMenu)menuStack.Pop(), exitHash, enterHash);
    }

    public void EnableCanvasGroupInteractable()
    {
        if (cg != null)
            cg.interactable = true;
    }

    public void DisableCanvasGroupInteractable()
    {
        if (cg != null)
            cg.interactable = false;
    }

    // don't use this
    public void ToggleCanvasGroupInteractable()
    {
        if (cg != null)
            cg.interactable = !cg.interactable;
    }

    void NavigationVoodoo()
    {
        UIHelper.selected = null;
        UIPointerNavigation.mousedOver = null;
        DisableCanvasGroupInteractable();
    }

    // Menus are scaled to zero and made uninteractable when "disabled", and restored when
    // "enabled". All menus have to be enabled from the start in the editor.
    // Objects with zero scale will not drive up the number of draw batches and setpasses.

    //bool hidden;

    //void Hide()
    //{
    //    hidden = true;
    //    transform.localScale = Vector3.zero;
    //    DisableCanvasGroupInteractable();
    //}

    //void Unhide()
    //{
    //    hidden = false;
    //    transform.localScale = Vector3.one;
    //    EnableCanvasGroupInteractable();
    //}
}
