using UnityEngine;
using System.Collections;

public class Delet : MonoBehaviour
{
    void OnEnable()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs purged");
        gameObject.SetActive(false);
    }
}
