using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Array's exact order
// 0: Windowed
// 1: Fullscreen

public class SettingsFullscreen : SettingsToggle
{
    protected override string key { get { return SettingsConst.FULLSCREEN_KEY; } }
    protected override int defaultValue { get { return SettingsConst.FULLSCREEN_DEFAULT; } }
    Resolution res;

    void Awake() { base._Awake(); }

    protected override void Init()
    {
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

        Debug.Log(t + " successfully set fullscreen");
    }
}
