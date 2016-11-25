using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SettingsBrightness : MonoBehaviour
{
    const string key = SettingsConst.BRIGHTNESS_KEY;
    const float defaultValue = SettingsConst.BRIGHTNESS_DEFAULT;

    public EffectBrightness mainCamera;

    // Settings scripts take public references to UI elements
    // to initialize them and save their values to PlayerPrefs.
    // Do not directly modify their values in Set() functions.

    public Slider brightness;

    void Awake()
    {
        ReadPrefs();

        // Unlike toggles, sliders don't fire the event if the value doesn't change.
        SetBrightness(brightness.value);
    }

    void ReadPrefs()
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, defaultValue);
            Debug.Log("Write default (" + key + ", " + defaultValue + ")");
        }

        brightness.value = PlayerPrefs.GetFloat(key);

        Debug.Log("Read (" + key + ", " + brightness.value + ")");
    }

    public void WritePrefs()
    {
        PlayerPrefs.SetFloat(key, brightness.value);
        Debug.Log("Write (" + key + ", " + brightness.value + ")");
    }

    // All Set functions should take the dynamic value from slider instead of
    // getting it from the slider reference. Write it to have one float parameter
    // and set it as a callback of the slider's OnValueChanged().

    public void SetBrightness(float value)
    {
        mainCamera.Set(1 + value / 10.0f);
    }
}
