using UnityEngine;
using System.Collections;

public class ParamsBack : MonoBehaviour
{
    public string exit, enter;
    
    public int exitHash { get; private set; }
    public int enterHash { get; private set; }

    protected virtual void Start()
    {
        exitHash = Animator.StringToHash(exit);
        enterHash = Animator.StringToHash(enter);
    }
}
