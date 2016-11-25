using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsVolume : MonoBehaviour
{
    //TODO: public audio manager
    public Slider master, music, sounds, voice;
    Volume _master, _music, _sounds, _voice;

    void Awake()
    {
        _master = new Volume(
            master,
            SettingsConst.VOLUME_MASTER_KEY,
            SettingsConst.VOLUME_MASTER_DEFAULT);

        _music = new Volume(
            music,
            SettingsConst.VOLUME_MUSIC_KEY,
            SettingsConst.VOLUME_MUSIC_DEFAULT);

        _sounds = new Volume(
            sounds,
            SettingsConst.VOLUME_SOUNDS_KEY,
            SettingsConst.VOLUME_SOUNDS_DEFAULT);

        _voice = new Volume(
            voice,
            SettingsConst.VOLUME_VOICE_KEY,
            SettingsConst.VOLUME_VOICE_DEFAULT);

        SetMaster(master.value);
        SetMusic(music.value);
        SetSounds(sounds.value);
        SetVoice(voice.value);
    }

    public void WritePrefs()
    {
        _master.WritePrefs();
        _music.WritePrefs();
        _sounds.WritePrefs();
        _voice.WritePrefs();
    }

    // WARNING: Volume range is 0 .. 10 whole numbers. Normalize the values before using.

    public void SetMaster(float value)
    {
        // TODO
        Debug.Log("Master volume set to " + value);
    }

    public void SetMusic(float value)
    {
        // TODO
        Debug.Log("Music volume set to " + value);
    }

    public void SetSounds(float value)
    {
        // TODO
        Debug.Log("Sounds volume set to " + value);
    }

    public void SetVoice(float value)
    {
        // TODO
        Debug.Log("Voice volume set to " + value);
    }

    // Manages initialization and reading/writing for each slider
    private class Volume
    {
        Slider slider;
        string key;
        float defaultValue;

        public Volume(Slider slider, string key, float defaultValue)
        {
            this.slider = slider;
            this.key = key;
            this.defaultValue = defaultValue;

            slider.value = PlayerPrefsHelper.Read(key, defaultValue);
        }

        public void WritePrefs() { PlayerPrefsHelper.Write(key, slider.value); }
    }
}
