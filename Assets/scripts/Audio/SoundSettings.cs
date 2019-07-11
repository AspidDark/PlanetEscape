using UnityEngine;
[System.Serializable]
public class SoundSettings
{   [HideInInspector]
    public AudioSource source;
    public AudioClip audioClip;
    public string sourseName;
    [Range(0, 255f)]
    public int priority;
    public bool loop;
    [Range(0, 1f)]
    public float volume;
    public bool pausable;
    public bool toPitch;
    public float pitchMin = 0.1f;
    public float pitchMax = 3f;
}
