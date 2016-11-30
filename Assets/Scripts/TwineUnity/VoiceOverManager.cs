using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class VoiceOverManager : MonoBehaviour {
    public VoiceOverClip[] VoiceOvers;
    static Dictionary<int, AudioClip> voiceDict;
    static AudioSource source;
	void Start () {
        source = GetComponent<AudioSource>();
        voiceDict = new Dictionary<int, AudioClip>();
        foreach(VoiceOverClip voc in VoiceOvers)
        {
            voiceDict.Add(voc.PID, voc.Clip);
        }
	}
    public static void Play(PassageNode pn)
    {
        Play(pn.GetID());
    }
    public static void Play(int PID)
    {
        AudioClip toPlay;
        if (voiceDict.TryGetValue(PID, out toPlay))
        {
            source.Stop();
            source.clip = toPlay;
            source.Play();
            print(source.clip.name);
        }
    }
}
