using UnityEngine;
using System.Collections;

public class PinSpawn : MonoBehaviour
{

    void OnEnable()
    {
        BowlingManager.SpawnPin += SpawnPin;
    }


    void OnDisable()
    {
        BowlingManager.SpawnPin -= SpawnPin;
    }

    void SpawnPin(GameObject pinObj)
    {
        //instantiate pin
        //GameObject pin = Instantiate(pinObj, transform.position, transform.rotation) as GameObject;
        Instantiate(pinObj, transform.position, transform.rotation);
    }
}
