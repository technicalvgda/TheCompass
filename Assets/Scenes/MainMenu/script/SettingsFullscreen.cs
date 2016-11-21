using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Array's exact order
// 0: Windowed
// 1: Fullscreen

public class SettingsFullscreen : SettingsToggle
{
    Resolution res;

    void Start() { base._Start(); }

    protected override void Init()
    {
        Debug.Log("SettingsFullscreen.Init()");
        res = Screen.currentResolution;
    }

    public void SetWindowed() { Set(0, false); }
    public void SetFullscreen() { Set(1, true); }

    void Set(int option, bool fullscreen)
    {
        Toggle t = toggles[option];

        Debug.Log(t + " tried to set fullscreen");

        if (!t.isOn) return;

        Screen.SetResolution(res.width, res.height, fullscreen);
        value = fullscreen ? 1 : 0;

        Debug.Log(t + " tried to set resolution");
    }
}
