using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelpSaveLoad
{
    public static int GetValue(string key, int value=0)
    {
       return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : value;
    }
    public static float GetValue(string key, float value=0)
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : value;
    }
    public static string GetValue(string key, string value="")
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : value;
    }

    public static void SetValue(string key, int value)
    {
        PlayerPrefs.SetInt(key,value);
    }
    public static void SetValue(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public static void SetValue(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    
    private static bool IfExists(string key)
    {
        return true;
    }

    public static void DeleteAllExeptSystem()
    {
        float soundEffectVolume = GetValue(ConstsLibrary.soundEffectVolumePrefs, 0f);
        float musicVolume= GetValue(ConstsLibrary.musicVolumePrefs, 0f);
        int muted = GetValue(ConstsLibrary.mutedPrefs, 0);
        DeleteAll();
        SetValue(ConstsLibrary.soundEffectVolumePrefs, soundEffectVolume);
        SetValue(ConstsLibrary.musicVolumePrefs, musicVolume);
        SetValue(ConstsLibrary.mutedPrefs, muted);
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

}
