using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Used in Animation Events to call functions on objects in the scene.

public class InvokeFunction : MonoBehaviour
{
    public UnityEvent[] events;

    // Do not name this Invoke(). It coincides with the one from MonoBehaviour
    // and the Animation Event will try to execute every other script's Invoke().

    public void InvokeAtIndex(int index)
    {
        events[index].Invoke();
    }
}
