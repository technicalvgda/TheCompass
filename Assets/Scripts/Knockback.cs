using UnityEngine;
using System.Collections;
using System;

public class Knockback : MonoBehaviour
{
    //recommend setting knockback to 50 at least, 100 for drama
    private Rigidbody2D _rb2d;
    private float _Xdif;
    private float _Ydif;
    private object _player;
    public float knockback;

    // Use this for initialization
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //I apologize to the Player script writer for I wrote in line 88-90 for my script to work
        _Xdif = Player.x - 10;
        _Ydif = Player.y - 10;
    }
        
    //Set desire object tag to "Wall" to make the pilot bounce off if collide
    //It also used the WallKnockback script unknown use to KingdomCross
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
            if (other.transform.position.x < transform.position.x)
                _rb2d.AddForce(new Vector2(-_Xdif,0) * knockback);
            else
                _rb2d.AddForce(new Vector2(_Xdif,0) * knockback);

            if (other.transform.position.y < transform.position.y)
                _rb2d.AddForce(new Vector2(0, -_Ydif) * knockback);
            else
                _rb2d.AddForce(new Vector2(0, _Ydif) * knockback);
    }
}