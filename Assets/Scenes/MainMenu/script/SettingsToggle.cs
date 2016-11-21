using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class SettingsToggle : MonoBehaviour
{
    public readonly string key;
    public int defaultValue;
    public ToggleGroup toggleGroup;
    public Toggle[] toggles;

    protected int value;

    protected void _Start()
    {
        ReadPrefs();
        Init();
    }

    void ReadPrefs()
    {
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetInt(key, defaultValue);

        value = PlayerPrefs.GetInt(key);
    }

    public void WritePrefs()
    {
        PlayerPrefs.SetInt(key, value);
    }

    protected abstract void Init();
}
