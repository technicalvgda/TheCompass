using UnityEngine;
using System.Collections;

// This mostly exists so that all the logging is in one place
// and error from copy pasting code is minimized

abstract class PlayerPrefsHelper : MonoBehaviour
{
    public static int Read(string key, int defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, defaultValue);
            //Debug.Log("Write default " + key + " = (int) " + defaultValue);
        }

        int value = PlayerPrefs.GetInt(key);
        //Debug.Log("Read " + key + " = (int) " + value);

        return value;
    }

    public static float Read(string key, float defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, defaultValue);
            //Debug.Log("Write default " + key + " = (float) " + defaultValue);
        }

        float value = PlayerPrefs.GetFloat(key);
        //Debug.Log("Read " + key + " = (float) " + value);

        return value;
    }

    public static void Write(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        //Debug.Log("Write " + key + " = (int) " + value);
    }

    public static void Write(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        //Debug.Log("Write " + key + " = (int) " + value);
    }
}
