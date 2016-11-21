using UnityEngine;
using System.Collections;

public class ParamsTransition : MonoBehaviour
{
    public UIMenu nextDesktop, nextMobile;
    public string exit, enter;

    public UIMenu next
    {
        get { return UIHelper.PLATFORM == 0 ? nextDesktop : nextMobile; }
    }

    public int exitHash { get; private set; }
    public int enterHash { get; private set; }

    protected virtual void Start()
    {
        exitHash = Animator.StringToHash(exit);
        enterHash = Animator.StringToHash(enter);
    }
}
