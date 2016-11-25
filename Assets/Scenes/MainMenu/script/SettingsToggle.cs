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
        ReadPrefs();
        Init();
    }

    protected abstract void Init();

    void ReadPrefs()
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, defaultValue);
            Debug.Log("Write default (" + key + ", " + defaultValue + ")");
        }
        
        value = PlayerPrefs.GetInt(key);

        Debug.Log("Read (" + key + ", " + value + ")");
    }

    public void WritePrefs()
    {
        PlayerPrefs.SetInt(key, value);
        Debug.Log("Write (" + key + ", " + value + ")");
    }
}
