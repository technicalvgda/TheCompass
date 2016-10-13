using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    public GameObject shield, shieldPrefab;
    private bool collision;
    private float waitTime;

    // Use this for initialization
    void Start()
    {
        collision = true;
        waitTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= 0 && shield != null)
        {
            Destroy(shield);
            collision = true;
        }

        waitTime -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        waitTime = 5.0f;
        if (col.gameObject.tag != ("Shield") && collision)
        {
            shield = (GameObject)Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            Debug.Log("Penis");
            collision = false;
        }

    }
}
