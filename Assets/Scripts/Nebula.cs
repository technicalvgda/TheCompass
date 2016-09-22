//============================
//Task Description: Damaging Nebula: An area which when the player crosses into kills them, and then they respawn outside of it.
//Last edited : 9/18/16
//============================

using UnityEngine;
using System.Collections;

public class Nebula : MonoBehaviour
{   
    /* Nebula is a trigger collider, both the player object and nebula have 2D box colliders and 
     * kinematic rigid bodies. For now, the player's position is just reset to origin upon collision
     */
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "PlayerPlaceholder" && col.gameObject.activeSelf)    //If nebula collides with player object
        {
            GameObject player = col.gameObject;

            //When the below is uncommented, the player object will be immediately "destroyed"
            //Should be replaced with code to decrease health
            //player.SetActive(false);

            player.transform.position = new Vector3(0, 0, 0);   //Temporary code for respawning, resets the player obj to origin
        }
    }
}
