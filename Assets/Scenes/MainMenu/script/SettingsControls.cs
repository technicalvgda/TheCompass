using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SettingsControls : SettingsToggle
{
    public GameObject aGray, aColored, bGray, bColored;

    protected override string key { get { return SettingsConst.CONTROLS_KEY; } }
    protected override int defaultValue { get { return SettingsConst.CONTROLS_DEFAULT; } }
    Player player;

    Toggle lastToggle;

    void Awake() { base._Awake(); }

    protected override void Init()
    {
        FindPlayer();

        if (value == SettingsConst.CONTROLS_A)
            lastToggle = toggles[0];
        else
            lastToggle = toggles[1];
        lastToggle.isOn = true;

        SetDemos();
    }

    void FindPlayer()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
            player = _player.GetComponent<Player>();
    }

    public void SetA() { Set(0, SettingsConst.CONTROLS_A); }
    public void SetB() { Set(1, SettingsConst.CONTROLS_B); }

    void Set(int index, int option)
    {
        Toggle t = toggles[index];
        Debug.Log(t + " tried to set control type from " + value + " to " + option);

        // Every toggle fires when a toggle is clicked.
        // Only let through the ones that are on.

        if (!t.isOn || t == lastToggle) return;

        lastToggle = t;
        value = option;
        SetDemos();

        Debug.Log(t + " successfully set control type to " + value);
    }

    void SetDemos()
    {
        bool a = value == SettingsConst.CONTROLS_A;
        aGray.SetActive(!a);
        aColored.SetActive(a);
        bGray.SetActive(a);
        bColored.SetActive(!a);
    }
}
