using UnityEngine;
using System.Collections;

public class PreprocessorPlatform : MonoBehaviour
{
    public GameObject[] desktop, mobile;

    void Awake()
    {
#if UNITY_EDITOR
        if (SettingsConst.EDITOR_PLATFORM == 0)
        {
            OnDesktop();
        }
        else
        {
            OnMobile();
        }
#elif UNITY_STANDALONE
        OnDesktop();
#else
        OnMobile();
#endif
    }

    void OnDesktop()
    {
        SetActiveGroup(desktop, true);
        SetActiveGroup(mobile, false);
    }

    void OnMobile()
    {
        SetActiveGroup(desktop, false);
        SetActiveGroup(mobile, true);
    }

    // Reusable
    protected void SetActiveGroup(GameObject[] group, bool active)
    {
        for (int i = 0; i < group.Length; i++)
        {
            group[i].SetActive(active);
        }
    }
}
