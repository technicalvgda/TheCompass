using UnityEngine;
using System.Collections;

public class AnimEventSelf : MonoBehaviour
{
    void SelfDisable()
    {
        gameObject.SetActive(false);
    }
}
