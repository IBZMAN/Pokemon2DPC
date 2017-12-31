using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if (s.Music)
            {
                s.source.outputAudioMixerGroup = s.audioMixer.FindMatchingGroups("Music")[0];
            }
            else
            {
                s.source.outputAudioMixerGroup = s.audioMixer.FindMatchingGroups("SFX")[0];
            }
        }
    }

    void Start()
    {
        Play("Littlerot_Town");
    }

    public void Play(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Could not find sound ");
            return;
        }

        s.source.Play();
    }
}
