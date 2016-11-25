using UnityEngine;
using System.Collections;

public class LateDisable : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }
}
