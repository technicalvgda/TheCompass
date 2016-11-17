using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnEnableListener : MonoBehaviour
{
    public UnityEvent onEnable;

    void OnEnable()
    {
        onEnable.Invoke();
    }
}
