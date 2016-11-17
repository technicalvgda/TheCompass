using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class MenuTransition : MonoBehaviour
{
    public UIMenu current, nextDesktop, nextMobile;
    public string exit, enter;
    int exitHash, enterHash;
    Button btn;

    void Start ()
    {
        exitHash = Animator.StringToHash(exit);
        enterHash = Animator.StringToHash(enter);
        btn = GetComponent<Button>();

        // Uncomment the directives and comment out the if conditionals before building.
        //#if UNITY_EDITOR || UNITY_STANDALONE
        if (UIHelper.PLATFORM == 0)
            btn.onClick.AddListener(
                () => current.Transition(true, false, nextDesktop, exitHash, enterHash));
        //#elif UNITY_IOS || UNITY_ANDROID
        else
            btn.onClick.AddListener(
                () => current.Transition(true, false, nextMobile, exitHash, enterHash));
        //#endif
    }
}
