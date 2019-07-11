using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SoundSettings
{   [HideInInspector]
    public AudioSource source;
    public AudioClip audioClip;
    public string sourseName;
    public int priority;
    public bool loop;
    [Range(0, 1f)]
    public float volume;
    public bool pausable;
    public bool toPitch;
}
