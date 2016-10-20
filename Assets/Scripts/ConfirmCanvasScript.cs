using UnityEngine;
using System.Collections;

public class ConfirmCanvasScript : MonoBehaviour {

    public Canvas confirmCanvas;

    void Start()
    {
        confirmCanvas.enabled = false;
    }

    public void Activate()
    {
        confirmCanvas.enabled = true;
    }

    public void Yes()
    {
        confirmCanvas.enabled = false;
    }

    public void No()
    {

        Screen.SetResolution(resolutionWidthCurrent, resolutionHeightCurrent, fullscreenToggle.isOn);
        confirmCanvas.enabled = false;

    }
}
