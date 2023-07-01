using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public bool isLoop = false;

    [Range(0f,1f)]
    public float volume = 1f;
    [Range(0, 255)]
    public int priority = 128;

    [HideInInspector]
    public AudioSource soundSource;
}
