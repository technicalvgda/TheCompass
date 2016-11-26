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

    protected void _Awake()
    {
        value = PlayerPrefsHelper.Read(key, defaultValue);
        Init();
    }

    protected abstract void Init();

    public void WritePrefs() { PlayerPrefsHelper.Write(key, value); }
}
