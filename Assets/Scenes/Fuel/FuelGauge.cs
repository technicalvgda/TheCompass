using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FuelGauge : MonoBehaviour
{
    public Animator gauge;
    public RectTransform mask, glow;

    int apid; // animator parameter id

    void Start ()
    {
        // Obtain the animator parameter's hashed ID to use in Set* functions
        // Do NOT pass strings as parameter
        apid = gauge.GetParameter(0).nameHash;

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Set(float value)
    {
        gauge.SetFloat(apid, value);
        mask.sizeDelta = new Vector2(252 * value / 1f, 28);
        glow.sizeDelta = new Vector2(252 * value / 1f, 28);
    }

    //private Player player;

    //void Update()
    //{
    //    if (player == null)
    //        TestManual();
    //    else
    //        TestPlayer();
    //}

    //const float MAX = 100;
    //float value = 100;
    //void TestManual()
    //{
    //    if (Input.GetKey(KeyCode.RightArrow))
    //        value -= 0.5f;
    //    else if (Input.GetKey(KeyCode.LeftArrow))
    //        value += 0.5f;
    //    value = Mathf.Clamp(value, 0, MAX);

    //    float percentage = value / MAX;
    //    gauge.SetFloat(apid, percentage);
    //    mask.sizeDelta = new Vector2(252 * percentage / 1f, 28);
    //    glow.sizeDelta = new Vector2(252 * percentage / 1f, 28);
    //}

    //void TestPlayer()
    //{
    //    float percentage = player.getFuel01();
    //    gauge.SetFloat(apid, percentage);
    //    mask.sizeDelta = new Vector2(252 * percentage / 1f, 28);
    //    glow.sizeDelta = new Vector2(252 * percentage / 1f, 28);
    //}
}
