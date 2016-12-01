using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System;

// Array's exact order
// 0: 1920 x 1080 (16:9)
// 1: 1600 x 900 (16:9)
// 2: 1280 x 720 (16:9)
// 3: 1366 x 768 (?)
// 4: 1360 x 768 (??)
// 5: 1280 x 1024 (5:4)
// 6: 1280 x 960 (4:3)
// 7: 1152 x 864 (4:3)
// 8: 1024 x 768 (4:3)

public class SettingsResolution : SettingsToggle
{
    public UnityEvent onResolutionChange;

    protected override string key { get { return SettingsConst.RESOLUTION_KEY; } }
    protected override int defaultValue { get { return SettingsConst.RESOLUTION_DEFAULT; } }

    Toggle lastToggle;
    bool revert = false;

    struct ResolutionOption
    {
        public readonly int width, height;
        public ResolutionOption(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
    ResolutionOption[] resOption;

    void Awake() { base._Awake(); }

    protected override void Apply()
    {
        resOption = new ResolutionOption[9] {
            new ResolutionOption(1920, 1080),
            new ResolutionOption(1600, 900),
            new ResolutionOption(1280, 720),
            new ResolutionOption(1366, 768),
            new ResolutionOption(1360, 768),
            new ResolutionOption(1280, 1024),
            new ResolutionOption(1280, 960),
            new ResolutionOption(1152, 864),
            new ResolutionOption(1024, 768)
        };

        Screen.SetResolution(
            resOption[value].width,
            resOption[value].height,
            Screen.fullScreen);
    }

    protected override void ApplyUI()
    {
        lastToggle = toggles[value];
        // Reminder: if this property is touched at all, OnValueChanged() will fire.
        lastToggle.isOn = true;
    }

    public void Set1920x1080() { Set(0); }
    public void Set1600x900() { Set(1); }
    public void Set1280x720() { Set(2); }
    public void Set1366x768() { Set(3); }
    public void Set1360x768() { Set(4); }
    public void Set1280x1024() { Set(5); }
    public void Set1280x960() { Set(6); }
    public void Set1152x864() { Set(7); }
    public void Set1024x768() { Set(8); }

    void Set(int index)
    {
        // Always send event when toggle is clicked, even if value didn't change
        // due to already active toggle in a toggle group being clicked.
        // It's up to the user to ignore a selection being set to the same value it already was, if desired.
        //
        // t. Unity 2016

        Toggle t = toggles[index];
        Debug.Log(t + " tried to set resolution");

        // Ignore toggles that are turned off (because they still fire the event),
        // or if the player is clicking on an already toggled toggle.

        if (!t.isOn || t == toggles[value]) return; // Change the second check to lastToggle

        Screen.SetResolution(
            resOption[index].width,
            resOption[index].height,
            Screen.fullScreen);

        // If the resolution is being reverted, the prompt should not be brought up.
        if (!revert)
        {
            onResolutionChange.Invoke();
        }

        lastToggle = toggles[value];
        value = index;
        revert = false;
        
        Debug.Log(t + " successfully set resolution. RESOLUTION IS NOW " +
            resOption[index].width + " x " + resOption[index].height);
    }

    public void Revert()
    {
        revert = true;
        lastToggle.isOn = true;
    }
}
