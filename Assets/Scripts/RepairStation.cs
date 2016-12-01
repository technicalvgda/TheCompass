using UnityEngine;
using System.Collections;

public class RepairStation : MonoBehaviour {

    GameObject MapIcon;
	// Use this for initialization
	void Start ()
    {
        MapIcon = transform.FindChild("MapIcon").gameObject;
	}
	
    public void ActivateIcon()
    {
        MapIcon.SetActive(true);
    }

    public void DeactivateIcon()
    {
        MapIcon.SetActive(false);
    }
}
