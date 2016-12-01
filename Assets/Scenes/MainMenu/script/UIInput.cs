using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIInput : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";
    const string CANCEL = "Cancel";

    const float DEAD_ZONE = 0.05f;

    public static GameObject selected
    {
        get { return EventSystem.current.currentSelectedGameObject; }
        set { EventSystem.current.SetSelectedGameObject(value); }
    }

    // Plain GetAxis check is extremely wonky with key presses,
    // with deflection periods that are not supposed to be there etc.
    // This is caused by keys and sticks all being grouped under the same tag.

    public static float horizontalAxis { get { return Input.GetAxis(HORIZONTAL); } }
    public static float verticalAxis { get { return Input.GetAxis(VERTICAL); } }

    public static bool isButtonNavigation
    {
        get
        {
            return
                isLeftKey || Mathf.Abs(horizontalAxis) > DEAD_ZONE ||
                isRightKey || Mathf.Abs(verticalAxis) > DEAD_ZONE;
        }
    }

    public static bool isBack
    {
        get { return Input.GetButtonDown(CANCEL); }
    }

    // Although the "Horizontal" group covers this, there NEEDS to be a distinction
    // between keys and sticks because they just don't output the same way.

    public static bool isLeftKey
    {
        get
        {
            return
                Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.A);
        }
    }

    public static bool isRightKey
    {
        get
        {
            return
                Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.D);
        }
    }

    //static float xPrev = 0;

    //public static bool isPositiveX { get; private set; }
    //public static bool isNegativeX { get; private set; }

    //void Update()
    //{
    //    float xCurrent = Input.GetAxis(HORIZONTAL);

    //    isPositiveX = xCurrent >= xPrev && xCurrent > DEAD_ZONE;
    //    isNegativeX = xCurrent <= xPrev && xCurrent < -DEAD_ZONE;

    //    xPrev = xCurrent;
    //}
}
