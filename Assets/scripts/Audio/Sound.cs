using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name;

    [Range(0, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public int prority;
    public bool pausable;

    public bool loop;
    [HideInInspector]
    public AudioSource source;

  
}
