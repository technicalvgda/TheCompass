using UnityEngine;
using System.Collections;

public class WallKnockback : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float thrust;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
            rb2d.AddForce()
        }
    }
}
