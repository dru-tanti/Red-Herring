﻿using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

//--------------------------------------------------------------------------------------------------------------------------
// Stores all the audio and will be used to simplify audio playing.
//--------------------------------------------------------------------------------------------------------------------------

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager current { get; private set; }  // Defines the AudioManager as a singleton.
    private int level;
    
    void Awake() {
        // Check that the instance for GameManager exists, if not set to this class.
        if (current == null) {
            current = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
            return;
        }

        // Retrieves the properties of the different sound clips and applies them where appropriate.
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        level = SceneManager.GetActiveScene().buildIndex;
        SceneMusic(level);
    }
    
    private void Update() {
        int levelCheck = SceneManager.GetActiveScene().buildIndex;
        if(levelCheck != level) {
            level = levelCheck;
            SceneMusic(level);
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.source.Play();
    }

    public void StopAll() {
        for (int i = 0; i < sounds.Length; i++) {
            foreach (Sound s in sounds) {
                s.source.Stop();
            }
        }

    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
        s.source.Stop();
    }

    void SceneMusic(int level) {
        Debug.Log(level);
        switch(level) {
            case 0:
                AudioManager.current.StopAll();
                AudioManager.current.Play("MenuMusic");
                break;
            case 1:
                AudioManager.current.StopAll();
                AudioManager.current.Play("Level1Music");
                break;
            case 3:
                AudioManager.current.StopAll();
                AudioManager.current.Play("Level2Music");
                break;
            case 4:
                AudioManager.current.StopAll();
                AudioManager.current.Play("Level3Music");
                break;
            default: Debug.Log("No Music Found");
                break;
        }
    }
}