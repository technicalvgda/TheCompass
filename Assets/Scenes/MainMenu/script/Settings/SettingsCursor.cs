using UnityEngine;
using System.Collections;

public class SettingsCursor : MonoBehaviour
{
    // nice engine dude
    // https://feedback.unity3d.com/suggestions/allow-access-to-sprite-packer-atlas-to-retrieve-sprites-by-name

    //public Sprite[] cursors;
    public Texture2D[] cursors;
    public Animator cursorChange;
    int value = -1;

    const string key = SettingsConst.CURSOR_KEY;
    const int defaultValue = SettingsConst.CURSOR_DEFAULT;

    void Awake()
    {
        //Set(PlayerPrefsHelper.Read(key, defaultValue));

        // Don't show "that"
        value = PlayerPrefsHelper.Read(key, defaultValue);
        Cursor.SetCursor(cursors[value], Vector2.zero, CursorMode.Auto);
    }

    public void WritePrefs() { PlayerPrefsHelper.Write(key, value); }

    public void Set(int index)
    {
        if (index == value) return;

        Cursor.SetCursor(cursors[index], Vector2.zero, CursorMode.Auto);
        cursorChange.SetTrigger(1246633515);

        value = index;

        Debug.Log("Cursor set to " + index);
    }
}
