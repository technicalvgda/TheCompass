using UnityEngine;
using System.Collections;

[RequireComponent(typeof(OnEnableListener), typeof(UIMenu))]
public class MenuDefault : MonoBehaviour
{
    public UIMenu next;
    public string enter;
    int enterHash;
    OnEnableListener oel;

    void Start()
    {
        enterHash = Animator.StringToHash(enter);
        oel = GetComponent<OnEnableListener>();

        oel.onEnable.AddListener(
            () => next.Transition(false, false, next, 0, enterHash));
    }
}
