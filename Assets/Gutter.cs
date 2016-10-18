using UnityEngine;
using System.Collections;

public class Gutter : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.name == "BowlingBall(Clone)")
        {
            Destroy(col.gameObject);
        }
       
    }
}
