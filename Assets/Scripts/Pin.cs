using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour
{

    void OnEnable()
    { BowlingManager.DestroyPin += CleanUp; }
    void OnDisable()
    { BowlingManager.DestroyPin -= CleanUp; }

    void CleanUp()
    {
        Destroy(this.gameObject);
    }

}
