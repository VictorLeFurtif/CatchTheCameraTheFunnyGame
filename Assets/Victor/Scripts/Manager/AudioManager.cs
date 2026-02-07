using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public Sound[] sounds;

    public AudioMixerSnapshot defaultSnapshot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        Play("AMB_City");
        Play("AMB_HipHop");
    }

    public void Play(string name, float pitch = 0, float volume = 0)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        foreach (AudioSource source in s.sources)
        {
            source.outputAudioMixerGroup = s.audioMixerGroup;
            source.clip = s.clip;
            source.loop = s.loop;
            source.volume = s.volume + volume;
            source.pitch = s.pitch + pitch;
            source.Play();
        }
        
        Debug.Log(name + "is playing");
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        foreach (AudioSource source in s.sources)
        {
            source.Stop();
        }
        
        Debug.Log(name + "stopped");
    }
}
