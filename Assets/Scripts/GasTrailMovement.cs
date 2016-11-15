using UnityEngine;
using System.Collections;

public class GasTrailMovement : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public int _gravityScale = 20;
    public float _blackHoleBoundary = 7f;
    public int _acceleration = 950;

    GameObject blackHole;

    void FixedUpdate()
    {
        
        _rb2d = GetComponent<Rigidbody2D>();
        Vector2 distance = blackHole.transform.position - transform.position;
        float near = Vector2.Distance(transform.position, transform.position);

        if (near > _blackHoleBoundary)//boundary set; if outside the inner boundary of black hole, object is still being pulled
        {
            _rb2d.AddForce((_acceleration / near) * distance.normalized * _rb2d.mass * _gravityScale * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        if (distance.magnitude < 1)
        {
            Destroy(gameObject);
        }    
    }
    

    public void SetBlackHole(GameObject hole)
    {
        blackHole = hole;
    }
}
