using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnPause : MonoBehaviour
{
    public bool pausable;
    public UnityEvent onPause;

    const string PAUSE = "Pause";

    void Update()
    {
        if (Input.GetButtonDown(PAUSE) && pausable)
        {
            onPause.Invoke();
        }
    }
}
