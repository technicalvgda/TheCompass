using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class MenuOverlay : MonoBehaviour
{
    public UIMenu current, nextDesktop, nextMobile;
    public string enter;
    int enterHash;
    Button btn;

    void Start()
    {
        enterHash = Animator.StringToHash(enter);
        btn = GetComponent<Button>();

        // Uncomment the directives and comment out the if conditionals before building.
        //#if UNITY_EDITOR || UNITY_STANDALONE
        if (UIHelper.PLATFORM == 0)
            btn.onClick.AddListener(
                () => current.Transition(true, true, nextDesktop, 0, enterHash));
        //#elif UNITY_IOS || UNITY_ANDROID
        else
            btn.onClick.AddListener(
                () => current.Transition(true, true, nextDesktop, 0, enterHash));
        //#endif
    }
}
