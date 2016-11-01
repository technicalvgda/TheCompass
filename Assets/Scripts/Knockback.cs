using UnityEngine;
using System.Collections;
using System;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float Xdif;
    private float Ydif;
    private object player;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Xdif = Player.x - 10;
        Ydif = Player.y - 10;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
            rb2d.AddForce(new Vector2(Xdif, Ydif) * 150);
    }
}