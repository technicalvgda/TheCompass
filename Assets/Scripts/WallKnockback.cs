using UnityEngine;
using System.Collections;

public class WallKnockback : MonoBehaviour
{
    public float knockback = 50;
    public bool UpWall;

    // Use this for initialization
    void Start ()
    {
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (UpWall)
        {
            
            other.rigidbody.AddForce(transform.up * knockback);
        }
        else
        {
            
            other.rigidbody.AddForce(-transform.up * knockback);
        }
        
        

        
    }
}
