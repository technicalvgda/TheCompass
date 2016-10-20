using UnityEngine;
using System.Collections;

public class ConfirmCanvasScript : MonoBehaviour {

    public Canvas confirmCanvas;

    //private bool isActive = false;

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
        confirmCanvas.enabled = false;

    }
}
