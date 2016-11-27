using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsVolume : MonoBehaviour
{
    //TODO: public audio manager
    public Slider master, music, sounds, voice;

    void Awake()
    {
        // Read
        float valueMaster = PlayerPrefsHelper.Read(
            SettingsConst.VOLUME_MASTER_KEY,
            SettingsConst.VOLUME_MASTER_DEFAULT);
        float valueMusic = PlayerPrefsHelper.Read(
            SettingsConst.VOLUME_MUSIC_KEY,
            SettingsConst.VOLUME_MUSIC_DEFAULT);
        float valueSounds = PlayerPrefsHelper.Read(
            SettingsConst.VOLUME_SOUNDS_KEY,
            SettingsConst.VOLUME_SOUNDS_DEFAULT);
        float valueVoice = PlayerPrefsHelper.Read(
            SettingsConst.VOLUME_VOICE_KEY,
            SettingsConst.VOLUME_VOICE_DEFAULT);

        // Apply
        SetMaster(valueMaster);
        SetMusic(valueMusic);
        SetSounds(valueSounds);
        SetVoice(valueVoice);

        // Apply UI
        master.value = valueMaster;
        music.value = valueMusic;
        sounds.value = valueSounds;
        voice.value = valueVoice;
    }

    public void WritePrefs()
    {
        PlayerPrefsHelper.Write(SettingsConst.VOLUME_MASTER_KEY, master.value);
        PlayerPrefsHelper.Write(SettingsConst.VOLUME_MUSIC_KEY, music.value);
        PlayerPrefsHelper.Write(SettingsConst.VOLUME_SOUNDS_KEY, sounds.value);
        PlayerPrefsHelper.Write(SettingsConst.VOLUME_VOICE_KEY, voice.value);
    }

    // WARNING: Volume range is 0 .. 10 whole numbers. Normalize the values before using.

    public void SetMaster(float value)
    {
        Debug.Log("Master volume set to " + value);
        // TODO
    }

    public void SetMusic(float value)
    {
        Debug.Log("Music volume set to " + value);
        // TODO
    }

    public void SetSounds(float value)
    {
        Debug.Log("Sounds volume set to " + value);
        // TODO
    }

    public void SetVoice(float value)
    {
        Debug.Log("Voice volume set to " + value);
        // TODO
    }
}
