using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SettingsBrightness : MonoBehaviour
{
    public EffectBrightness mainCamera;

    const string key = SettingsConst.BRIGHTNESS_KEY;
    const float defaultValue = SettingsConst.BRIGHTNESS_DEFAULT;

    // Settings scripts take public references to UI elements
    // to initialize them and save their values to PlayerPrefs.
    // Do not directly modify their values in Set() functions.

    public Slider brightness;

    void Awake()
    {
        brightness.value = PlayerPrefsHelper.Read(key, defaultValue);

        // Unlike toggles, sliders don't fire the event if the value doesn't change.
        Set(brightness.value);
    }
    
    public void WritePrefs() { PlayerPrefsHelper.Write(key, brightness.value); }

    // All Set functions should take the dynamic value from slider instead of
    // getting it from the slider reference. Write it to have one float parameter
    // and set it as a callback of the slider's OnValueChanged().

    public void Set(float value)
    {
        mainCamera.Set(1 + value / 10.0f);
    }
}
