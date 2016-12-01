using UnityEngine;
using System.Collections;

public class MenuTransitionPlatform : MenuTransition
{
    public Menu nextDesktop, nextMobile;

    public override Menu next
    {
        get
        {
#if UNITY_EDITOR
            return SettingsConst.EDITOR_PLATFORM == 0 ? nextDesktop : nextMobile;
#elif UNITY_STANDALONE
            return nextDesktop;
#else
            return nextMobile;
#endif
        }
    }
}
