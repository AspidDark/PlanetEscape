using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SoundManager : MonoBehaviour {
    //Static & system
    private static string nowPlayingMisicName;
    public static SoundManager instance;
    #region Sound
    public float soundVolume;
    public SoundScriptable[] soundSettings;

    //public float pitchMin = 0.1f;
    //public float pitchMax = 3f;
    #endregion
    #region Music
    public float MusicChangeSpeed;
    public float musicVolume;
    public SoundScriptable[] musicSettings;

    private bool canChangeMusicNow;
    #endregion
    public int allMuted;
  //  public bool muted { get { return allMuted <= 0;  } set { } }
    private void Awake()
    {
        canChangeMusicNow = true;
        instance = instance ?? this;
        DontDestroyOnLoad(gameObject);
        soundVolume = PlayerPrefs.GetFloat(ConstsLibrary.soundEffectVolumePrefs, 1);
        musicVolume = PlayerPrefs.GetFloat(ConstsLibrary.musicVolumePrefs, 1);
        allMuted = PlayerPrefs.GetInt(ConstsLibrary.mutedPrefs, 0);

        foreach (var item in soundSettings)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.volume = soundVolume* item.volumeDecreaser;
            item.source.clip = item.audioClip;
            item.source.priority = item.priority;
            item.source.loop = item.loop;

        }
        foreach (var item in musicSettings)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.volume = musicVolume*item.volumeDecreaser;
            item.source.clip = item.audioClip;
            item.source.priority = item.priority;
            item.source.loop = item.loop;
        }
        if (allMuted == 1)
        {
            MuteAll(true);
        }
        else
            MuteAll(false);

    }

    // Use this for initialization
    void Start ()
    {
        instance = instance ?? this;
        PlayFirstMusic();
    }

    public void MuteAll(bool muted)
    {
        foreach (var item in soundSettings)
        {
              item.source.mute = muted;
        }
        foreach (var item in musicSettings)
        {
              item.source.mute = muted;
        }
        if (muted)
            PlayerPrefs.SetInt(ConstsLibrary.mutedPrefs, 1);
        else
            PlayerPrefs.SetInt(ConstsLibrary.mutedPrefs, 0);
    }


    public void PlaySound(string soundName)
    {
        var s = Array.Find(soundSettings, soundSettings => soundSettings.sourseName == soundName);
        if (s == null)
        {
            print("Sound" + soundName + "Not found");
            return;
        }
        if (s.toPitch)
        {
            s.source.pitch= UnityEngine.Random.Range(s.pitchMin, s.pitchMax);
        }
        s.source.Play();
       
    }

    public void StopSound(string soundName)
    {
        var s = Array.Find(soundSettings, soundSettings => soundSettings.sourseName == soundName);
        if (s == null)
        {
            print("Sound" + soundName + "Not found");
            return;
        }
        s.source.Stop();
    }

    public void PlayMusic(string musicName)
    {
        if (nowPlayingMisicName == musicName)
        {
            return;
        }
        if (!canChangeMusicNow)
        {
            return;
        }
        canChangeMusicNow = false;
        var s = Array.Find(musicSettings, musicSettings => musicSettings.sourseName == musicName);
        if (s == null)
        {
            print("No music with name" + musicName);
            return;
        }
        StartCoroutine(CChangeMusic(musicName));
    }
    public void SetSoundVolume(float volume)
    {
        foreach (var item in soundSettings)
        {
            item.source.volume = volume * item.volumeDecreaser;
        }
        PlayerPrefs.SetFloat(ConstsLibrary.soundEffectVolumePrefs, volume);
    }
    public void SetMisicVolume(float volume)
    {
        foreach (var item in musicSettings)
        {
            item.source.volume = volume * item.volumeDecreaser; 
        }
        var s = Array.Find(musicSettings, musicSettings => musicSettings.sourseName == nowPlayingMisicName);
        s.volume = volume;
        PlayerPrefs.SetFloat(ConstsLibrary.musicVolumePrefs, volume);
    }
    public void AllAudioOnOff(bool audioSwitcher)
    {
        AudioListener.pause = !audioSwitcher;
    }

    public void HasFocus(bool hasFocus)
    {
        AllAudioOnOff(hasFocus);
    }
    private void PlayFirstMusic()
    {
        musicSettings.First().source.Play();
        nowPlayingMisicName = musicSettings.First().sourseName;
    }
      
    private IEnumerator CChangeMusic(string musicName)
    {
        var s = Array.Find(musicSettings, musicSettings => musicSettings.sourseName == nowPlayingMisicName);
        float musicVolume = s.source.volume;
        while (s.source.volume > 0)
        {
            s.source.volume -= Time.deltaTime / MusicChangeSpeed;
            yield return null;
        }
        s.source.Stop();
        s.source.volume = musicVolume;
        var newMusic = Array.Find(musicSettings, musicSettings => musicSettings.sourseName == musicName);
        newMusic.source.volume = 0;
        newMusic.source.Play();
        while (newMusic.source.volume < musicVolume)
        {
            newMusic.source.volume += Time.deltaTime / MusicChangeSpeed; ;
            yield return null;
        }
        nowPlayingMisicName = musicName;
        canChangeMusicNow = true;
        yield break;
    }

    private float timer=0;
    public void PlayOneATime(string name, float time)
    {
        if (timer > 0)
        {
            return;
        }
        timer = time;
        PlaySound(name);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }
}

