using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    #region Singlton
    public static SoundManager instance;

    private void Awake()
    {
        SetupSounds();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Try create another instance of game manager!");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField] private string battleMusic;
    [SerializeField] private string menuMusic;

    public string BattleMusic => battleMusic;
    public string MenuMusic => menuMusic;
    public Sound[] sounds;

    private Dictionary<Sound, float> soundTimerDictionary = new Dictionary<Sound, float>();

    private void Start()
    {

    }


    private void SetupSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip   = s.clip;
            s.source.volume = s.volume;
            s.source.pitch  = s.pitch;
            s.source.loop   = s.loop;
            if (s.isDelayNeed)
                soundTimerDictionary[s] = 0;
        }
    }

    public void SetAllFXVolume(float volumePower)
    {
        foreach (Sound s in sounds)
        {
            if (!s.isMusic)
            {
                s.source.volume = s.volume * volumePower;
            }
        }
    }

    public void SetAllMusicVolume(float volumePower)
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                s.source.volume = s.volume * volumePower;
            }
        }
    }

    public Sound GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Not have sound name : '{ name }' !");
            return null;
        }
        return s;
    }

    public void Play(string name)
    {
        Sound s = GetSound(name);
        if (s != null && CanPlaySound(s))
        {
            s.source.Play();
        }
    }

    public void StopSound(string name)
    {
        Sound s = GetSound(name);
        s.source.Stop();
    }

    private bool CanPlaySound(Sound sound)
    {
        switch (sound.isDelayNeed)
        {
            default:
                return true;
            case true:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerTimerMax = sound.clip.length;
                    if (lastTimePlayed + playerTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
        }
    }

    public void PlayBattleMusic()
    {
        Play(battleMusic);
    }

    public void PlayMenuMusic()
    {
        Play(menuMusic);
    }

    public void StopBattleMusic()
    {
        StopSound(battleMusic);
    }

    public void StopMenuMusic()
    {
        StopSound(menuMusic);
    }
}
