using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FuelGauge : MonoBehaviour
{
    private Player player;
    [Space]
    public Animator fuel;
    public Image mask, glow;

    int apid; // animator parameter id

    void Start ()
    {
        // Obtain the animator parameter's hashed ID to use in Set* functions
        // Do NOT pass strings as parameter
        apid = fuel.GetParameter(0).nameHash;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

	void Update ()
    {
        if (player == null)
            TestManual();
        else
            TestPlayer();
    }

    const float MAX = 100;
    float value = 100;
    void TestManual()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            value -= 0.5f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            value += 0.5f;
        value = Mathf.Clamp(value, 0, MAX);

        float percentage = value / MAX;
        fuel.SetFloat(apid, percentage);
        mask.fillAmount = percentage;
        glow.fillAmount = percentage;
    }

    void TestPlayer()
    {
        float percentage = player.getFuel01();
        fuel.SetFloat(apid, percentage);
        mask.fillAmount = percentage;
        glow.fillAmount = percentage;
    }
}
