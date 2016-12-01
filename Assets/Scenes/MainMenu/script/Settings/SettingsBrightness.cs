using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SettingsBrightness : MonoBehaviour
{
    // Settings scripts take public references to UI elements
    // to initialize them and save their values to PlayerPrefs.
    // Do not directly modify their values in Set() functions.

    public EffectBrightness mainCamera;
    public Slider brightness;

    void Awake()
    {
        // Read
        float value = PlayerPrefsHelper.Read(
            SettingsConst.BRIGHTNESS_KEY,
            SettingsConst.BRIGHTNESS_DEFAULT);

        // Apply
        Set(value);

        // ApplyUI
        if (brightness != null) brightness.value = value;

        // Unlike toggles, sliders don't fire the event if the value doesn't change.
    }


    // Write functions assume a slider is present and write its value to PlayerPrefs.
    // (without a slider to configure the setting, there would be no reason to call these)

    public void WritePrefs()
    {
        PlayerPrefsHelper.Write(SettingsConst.BRIGHTNESS_KEY, brightness.value);
    }

    // All Set functions should take the dynamic value from slider instead of
    // getting it from the slider reference. Write it to have one float parameter
    // and set it as a callback of the slider's OnValueChanged().

    public void Set(float value)
    {
        mainCamera.Set(1 + value / 10.0f);
    }
}
