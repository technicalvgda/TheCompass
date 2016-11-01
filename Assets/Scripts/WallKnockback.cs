using UnityEngine;
using System.Collections;

public class WallKnockback : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Player player;
    public Collider2D col;
    public float knockback = 10f;
    public GameObject theObject;
    public Vector2 bounceIntensity;

    // Use this for initialization
    void Start()
    {
        if (theObject == null)
            Debug.Log("Object isn't set!");
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    //public void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force);

    //void OnTriggerEnter2D(Collider2D col)
    //{
        //if (col.gameObject.tag == "Wall")
        //{
            //rb2d.AddForce(Vector2.up * knockback);
            //Destroy(gameObject);
            //rb2d.AddForce(-transform.forward * 500);
            //transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y - 0.2f);
            //transform.Translate(new Vector2(1, 1) * Time.deltaTime);
       //}
        
    //}
}
