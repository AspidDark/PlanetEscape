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
    public SoundSettings[] soundSettings;
    public float soundVolume;
    public float pitchMin = 0.1f;
    public float pitchMax = 3f;
    #endregion
    #region Music
    public SoundSettings[] musicSettings;
    public float MusicChangeSpeed;
    public float musicVolume;
    private bool canCangeMusicNow;
    #endregion
    public int allMuted;
  //  public bool muted { get { return allMuted <= 0;  } set { } }
    private void Awake()
    {
        canCangeMusicNow = true;
        instance = instance ?? this;
        DontDestroyOnLoad(gameObject);
        soundVolume = PlayerPrefs.GetFloat("soundVolume", 1);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1);
        allMuted = PlayerPrefs.GetInt("Muted", 0);


        foreach (var item in soundSettings)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.volume = soundVolume;
            item.source.clip = item.audioClip;
            item.source.priority = item.priority;
            item.source.loop = item.loop;

        }
        foreach (var item in musicSettings)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.volume = musicVolume;
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
            PlayerPrefs.SetInt("Muted", 1);
        else
            PlayerPrefs.SetInt("Muted", 0);
    }


    public void PlaySound(string soundName)
    {
        var s = Array.Find(soundSettings, soundSettings => soundSettings.sourseName == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound" + soundName + "Not found");
            return;
        }
        if (s.toPitch)
        {
            s.source.pitch= UnityEngine.Random.Range(pitchMin,pitchMax);
        }
        s.source.Play();
       
    }
    public void PlayMusic(string musicName)
    {
        if (nowPlayingMisicName == musicName)
        {
            return;
        }
        if (!canCangeMusicNow)
        {
            return;
        }
        canCangeMusicNow = false;
        var s = Array.Find(musicSettings, musicSettings => musicSettings.sourseName == musicName);
        if (s == null)
        {
            Debug.Log("No music with name" + musicName);
            return;
        }
        StartCoroutine(CChangeMusic(musicName));
    }
    public void SetSoundVolume(float volume)
    {
        foreach (var item in soundSettings)
        {
            item.source.volume = volume;
        }
        PlayerPrefs.SetFloat("soundVolume", volume);
    }
    public void SetMisicVolume(float volume)
    {
        foreach (var item in musicSettings)
        {
            item.source.volume = volume; 
        }
        var s = Array.Find(musicSettings, musicSettings => musicSettings.sourseName == nowPlayingMisicName);
        s.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
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
        canCangeMusicNow = true;
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

