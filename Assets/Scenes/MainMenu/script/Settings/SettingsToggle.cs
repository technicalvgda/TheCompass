using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class SettingsToggle : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Toggle[] toggles;

    protected abstract string key { get; }
    protected abstract int defaultValue { get; }
    protected int value;

    // Initialize in Awake() instead of Start() since menus' Late Disable script
    // can unpredictably prevent Start() from running.
    // P.S.
    // This is no longer a concern as settings initialize independently from the menus.
    
    // Each settings script does the following on initialization:
    // - Read a value from PlayerPrefs
    // - Optionally apply the settings to the game
    //  (dim/brighten the screen, adjust the volume levels, adjust the resolution)
    // - Optionally apply the changes to the UI elements if there are any attached
    //  (setting the sliders and toggles to the correct state)

    protected void _Awake()
    {
        value = PlayerPrefsHelper.Read(key, defaultValue);
        Apply();

        if (toggleGroup != null && toggles.Length > 0)
        {
            ApplyUI();
        }
    }

    protected virtual void Apply() { }
    protected virtual void ApplyUI() { }

    public void WritePrefs() { PlayerPrefsHelper.Write(key, value); }
}
