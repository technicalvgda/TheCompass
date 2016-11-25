using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// object
// oriented
// programming

public class SettingsVolume : MonoBehaviour
{
    //TODO: public audio manager
    public Slider master, music, sounds, voice;
    Volume _master, _music, _sounds, _voice;

    const string
        keyMaster = SettingsConst.VOLUME_MASTER_KEY,
        keyMusic = SettingsConst.VOLUME_MUSIC_KEY,
        keySounds = SettingsConst.VOLUME_SOUNDS_KEY,
        keyVoice = SettingsConst.VOLUME_VOICE_KEY;

    const float
        defaultMaster = SettingsConst.VOLUME_MASTER_DEFAULT,
        defaultMusic = SettingsConst.VOLUME_MUSIC_DEFAULT,
        defaultSounds = SettingsConst.VOLUME_SOUNDS_DEFAULT,
        defaultVoice = SettingsConst.VOLUME_VOICE_DEFAULT;

    void Awake()
    {
        ReadPrefs();

        SetMaster(master.value);
        SetMusic(music.value);
        SetSounds(sounds.value);
        SetVoice(voice.value);
    }

    void ReadPrefs()
    {
        _master = new Volume(master, keyMaster, defaultMaster);
        _music = new Volume(music, keyMusic, defaultMusic);
        _sounds = new Volume(sounds, keySounds, defaultSounds);
        _voice = new Volume(voice, keyVoice, defaultVoice);
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

            ReadPrefs();
        }

        void ReadPrefs()
        {
            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetFloat(key, defaultValue);
                Debug.Log("Write default (" + key + ", " + defaultValue + ")");
            }

            slider.value = PlayerPrefs.GetFloat(key);
            Debug.Log("Read (" + key + ", " + slider.value + ")");
        }

        public void WritePrefs()
        {
            PlayerPrefs.SetFloat(key, slider.value);
            Debug.Log("Write (" + key + ", " + slider.value + ")");
        }
    }
}
