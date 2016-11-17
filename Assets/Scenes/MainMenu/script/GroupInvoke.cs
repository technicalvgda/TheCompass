using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Used in Animation Events to call functions on objects in the scene.

public class GroupInvoke : MonoBehaviour
{
    public UnityEvent[] events;

    public void Invoke(int index)
    {
        if (index < events.Length)
            events[index].Invoke();
    }
}
