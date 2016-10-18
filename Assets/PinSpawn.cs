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

    void SpawnPin(GameObject pinObj, BowlingManager man)
    {
        //instantiate pin
        GameObject pin = Instantiate(pinObj, transform.position, transform.rotation) as GameObject;
        //set the pins reference to bowling manager
        pin.GetComponent<Pin>().SetManager(man);
    }
}
