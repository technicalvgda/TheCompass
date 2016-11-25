using UnityEngine;
using System.Collections;

public class DELET : MonoBehaviour
{
	void Awake ()
    {
        Debug.Log("PlayerPrefs deleted! PlayerPrefs deleted! PlayerPrefs deleted!");
        PlayerPrefs.DeleteAll();
    }
}
