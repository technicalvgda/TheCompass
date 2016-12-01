using UnityEngine;
using System.Collections;

public class MenuBack : MonoBehaviour
{
    public string exit, enter;
    
    public int exitHash { get; private set; }
    public int enterHash { get; private set; }

    protected virtual void Awake()
    {
        exitHash = Animator.StringToHash(exit);
        enterHash = Animator.StringToHash(enter);
    }
}
