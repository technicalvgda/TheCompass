using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public bool counterClock = false;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        RotateObj();
    }

    //Physics
    void RotateObj()
    {
        if(counterClock)
        {
            transform.Rotate(-Vector3.forward);
        }
        else
        {
            transform.Rotate(Vector3.forward);
        }
        
    }
}