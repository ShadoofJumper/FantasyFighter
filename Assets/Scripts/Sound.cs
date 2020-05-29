﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string       name;
    public AudioClip    clip;

    [Range(0.0f, 1.0f)]
    public float    volume;
    [Range(0.1f, 3.0f)]
    public float    pitch;
    public bool     loop;
    public bool     isMusic;
    public bool     isDelayNeed;

    [HideInInspector]
    public AudioSource source;

}
