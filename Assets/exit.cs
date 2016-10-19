using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            Debug.Log("End Level");
            Time.timeScale = 0;
        }
    }
}
