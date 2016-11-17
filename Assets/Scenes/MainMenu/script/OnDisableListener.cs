using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnDisableListener : MonoBehaviour
{
    public UnityEvent onDisable;

    void OnDisable()
    {
        onDisable.Invoke();
    }
}
