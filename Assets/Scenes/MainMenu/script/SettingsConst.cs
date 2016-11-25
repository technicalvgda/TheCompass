using UnityEngine;
using System.Collections;

public class SettingsConst : MonoBehaviour
{
    public const int EDITOR_PLATFORM = 0;

    public const string CONTROLS_KEY = "CONTROLS";
    public const int CONTROLS_DEFAULT = CONTROLS_A;
    public const int CONTROLS_A = 1;
    public const int CONTROLS_B = 2;

    public const string BRIGHTNESS_KEY = "BRIGHTNESS";
    public const float BRIGHTNESS_DEFAULT = 0;

    public const string RESOLUTION_KEY = "RESOLUTION";
    public const int RESOLUTION_DEFAULT = 2;

    public const string FULLSCREEN_KEY = "FULLSCREEN";
    public const int FULLSCREEN_DEFAULT = 1;

    public const string VOLUME_MASTER_KEY = "VOLUME_MASTER";
    public const float VOLUME_MASTER_DEFAULT = 8;

    public const string VOLUME_MUSIC_KEY = "VOLUME_MUSIC";
    public const float VOLUME_MUSIC_DEFAULT = 8;

    public const string VOLUME_SOUNDS_KEY = "VOLUME_SOUNDS";
    public const float VOLUME_SOUNDS_DEFAULT = 8;

    public const string VOLUME_VOICE_KEY = "VOLUME_VOICE";
    public const float VOLUME_VOICE_DEFAULT = 8;
}
