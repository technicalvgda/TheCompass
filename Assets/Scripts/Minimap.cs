using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

    public Transform Target;


    void Start() {
        transform.position = new Vector3(Target.position.x, Target.position.y, Target.position.z);
    }

    void Update () {
        transform.position = new Vector3(Target.position.x, Target.position.y, Target.position.z);
        transform.rotation = new Quaternion(0, 0, Target.rotation.z, 1);
    }
}
