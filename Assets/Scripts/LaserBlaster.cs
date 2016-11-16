using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserBlaster : MonoBehaviour
{
    public GameObject laserProjectile;
    private List<GameObject> Projectiles = new List<GameObject>();
    public float timer;
    private float laserSpeed;
    [SerializeField]
    private float updown, leftright; //choose a direction with -1,0,1


    // Use this for initialization
    void Start()
    {
        laserSpeed = 3;
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //fires a "laser" every 2 seconds
        if (timer > 2)
        {
            GameObject laser = (GameObject)Instantiate(laserProjectile, transform.position, Quaternion.identity);
            Projectiles.Add(laser);
            timer = 0;
        }
        for (int i = 0; i < Projectiles.Count; i++)
        {
            GameObject firedShot = Projectiles[i];
            if (firedShot != null)
            {
                firedShot.transform.Translate(new Vector2(leftright, updown) * Time.deltaTime * laserSpeed);

            }
            if (Projectiles[i] == null)
            {
                Projectiles.RemoveAt(i);
            }

        }
    }
}












