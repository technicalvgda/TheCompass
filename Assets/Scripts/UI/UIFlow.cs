using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIFlow : MonoBehaviour
{
    const int platform = 0;
    Animator anim;
    CanvasGroup cg;
    public string nextScene { private get; set; }

    void Start()
    {
        anim = GetComponent<Animator>();
        cg = GetComponent<CanvasGroup>();
    }

    // Transitions from a submenu to another submenu.
    // A submenu with an exit animation has the following Mecanim controller setup:
    //
    //   state          trigger              state                  state
    // Any State --[Transition Away]--> Transition Away --> ... --> Exit
    //
    // If an exit animation is present, transition after it's finished playing,
    // otherwise transition immediately.

    public void Transition(UIFlow target)
    {
        if (anim != null && anim.HasState(0, 1258345539)) // = "Transition Away"
            StartCoroutine(_LongTransition(target));
        else
            InstantTransition(target);
    }

    IEnumerator _LongTransition(UIFlow target)
    {
        anim.SetTrigger(1258345539); // = "Transition Away"

        // Wait one frame before getting the Animation State Info,
        // otherwise you end up with the one from the current state.
        yield return null;

        // The animation is finished when this returns a different name hash
        while (anim.GetCurrentAnimatorStateInfo(0).shortNameHash == 1258345539)
            yield return null;

        InstantTransition(target);
    }

    void InstantTransition(UIFlow target)
    {
        UIHelper.selected = null;
        UIPointerNavigation.mousedOver = null;

        DisableCanvasGroupInteractable();
        gameObject.SetActive(false);

        if (target != null)
        {
            target.gameObject.SetActive(true);
        }
    }

    // For any button that brings up a platform-dependent menu, in the OnClick event
    // replace Transition with both of the followings.
    // Uncomment the directives and comment out the if conditionals before building.

    public void TransitionDesktop(UIFlow target)
    {
//#if UNITY_EDITOR || UNITY_STANDALONE
        if (platform == 0)
            Transition(target);
//#endif
    }

    public void TransitionMobile(UIFlow target)
    {
//#if UNITY_IOS || UNITY_ANDROID
        if (platform == 1)
            Transition(target);
//#endif
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

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadNextScene()
    {
        if (nextScene != null)
            SceneManager.LoadScene(nextScene);
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
