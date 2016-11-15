using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIHelper : MonoBehaviour
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";

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

    public static void SetSelected(GameObject selected)
    {
        EventSystem.current.SetSelectedGameObject(selected);
    }

    public static GameObject currentSelected
    {
        get
        {
            return EventSystem.current.currentSelectedGameObject;
        }
    }

    
    public static bool isButtonNavigation
    {
        get
        {
            if (Input.GetAxis(HORIZONTAL) != 0 || Input.GetAxis(VERTICAL) != 0)
                return true;

            return false;
        }
    }
}
