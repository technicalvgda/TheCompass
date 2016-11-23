using UnityEngine;
using System.Collections;

public class ParamsTransition : MonoBehaviour
{
    public UIMenu nextDesktop, nextMobile;
    public string exit, enter;

    public UIMenu next
    {
        get
        {
#if UNITY_EDITOR
            return SettingsConst.EDITOR_PLATFORM == 0 ? nextDesktop : nextMobile;
#elif UNITY_STANDALONE
            return nextDesktop;
#else
            return nextMobile;
#endif
        }
    }

    public int exitHash { get; private set; }
    public int enterHash { get; private set; }

    protected virtual void Start()
    {
        exitHash = Animator.StringToHash(exit);
        enterHash = Animator.StringToHash(enter);
    }
}
