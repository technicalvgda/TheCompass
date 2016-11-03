using UnityEngine;
using System.Collections;

public class WallKnockback : MonoBehaviour
{
    internal static float x;
    internal static float y;
    private Knockback _player;
    public float knockback;
    public bool horizontalWall;

    // Use this for initialization
    void Start ()
    {
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (horizontalWall)
        {
            if (other.transform.position.y > transform.position.y)
                other.rigidbody.AddForce(new Vector2(0, 10) * knockback);
            else
                other.rigidbody.AddForce(new Vector2(0, -10) * knockback);
        }
        else
        {
            if (other.transform.position.x < transform.position.x)
                other.rigidbody.AddForce(new Vector2(-10, 0) * knockback);
            else
                other.rigidbody.AddForce(new Vector2(10, 0) * knockback);
        }
        

        
    }
}
