using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIHelper : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";
    const string CANCEL = "Cancel";
    
    public static GameObject selected
    {
        get { return EventSystem.current.currentSelectedGameObject; }
        set { EventSystem.current.SetSelectedGameObject(value); }
    }

    public static bool isButtonNavigation
    {
        get { return Input.GetAxis(HORIZONTAL) != 0 || Input.GetAxis(VERTICAL) != 0; }
    }

    public static bool isBackPressed
    {
        get { return Input.GetButtonDown(CANCEL); }
    }

    // WaitForSeconds that works under timeScale 0
    // http://blog.projectmw.net/wait-for-real-seconds-class-using-static-function-in-unity

    public static IEnumerator WaitForRealSeconds(float seconds)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + seconds)
        {
            yield return null;
        }
    }
}
