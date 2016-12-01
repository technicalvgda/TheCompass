using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Used in Animation Events to do things that normally wouldn't be possible
// - calling functions on objects in the scene
// - disabling self

public class AnimEventInvoke : MonoBehaviour
{
    public UnityEvent[] events;

    // Do not name this Invoke(). It coincides with the one from MonoBehaviour
    // and the Animation Event will try to execute every other script's Invoke().

    void InvokeAtIndex(int index)
    {
        events[index].Invoke();
    }
}
