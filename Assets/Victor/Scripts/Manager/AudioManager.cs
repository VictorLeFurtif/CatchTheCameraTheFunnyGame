using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        
    }

    public void Play()
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        foreach (AudioSource source in s.sources)
        {
            source.outputAudioMixerGroup = s.audioMixerGroup;
            source.clip = s.clip;
            source.loop = s.loop;
            source.volume = s.volume;
            source.pitch = s.pitch;
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
