using UnityEngine;
using System.Collections;

public abstract class MenuTransition : MonoBehaviour
{
    public abstract Menu next { get; }
    public string exit, enter;
    public int exitHash { get; private set; }
    public int enterHash { get; private set; }

    protected virtual void Awake()
    {
        exitHash = Animator.StringToHash(exit);
        enterHash = Animator.StringToHash(enter);
    }
}
