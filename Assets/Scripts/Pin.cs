using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour
{

    BowlingManager manager;

    public void SetManager(BowlingManager mngr)
    {
        manager = mngr;
    }

    void OnDestroy()
    {
        manager.KnockDownPin();
    }

}
