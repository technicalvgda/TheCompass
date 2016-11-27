using UnityEngine;
using System.Collections;

public class Gutter : MonoBehaviour {

    public bool isSideGutter;

    public BowlingManager bowlManager;

    //event declaration for spawning pins
    public delegate void BallHandler ();
    public static event BallHandler LostBall;

    //event declaration for increasing score
    public delegate void IncreaseScoreDelegate();
    public static event IncreaseScoreDelegate HitPin;

    void OnTriggerStay2D(Collider2D col)
    {
       
        if (col.name == "BowlingBall(Clone)")
        {
            if(isSideGutter)
            {
                bowlManager.GutterBall();
            }
            Destroy(col.gameObject);
            LostBall();
        }
        if (col.name == "Pin(Clone)")
        {
            Destroy(col.gameObject);
            HitPin();
        }
            

    }
}
