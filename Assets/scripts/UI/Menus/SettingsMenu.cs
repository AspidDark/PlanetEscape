using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public SoundScriptable menuTheme;

    private AudioSource mainTheme;

    public GameObject mainMenu;
    public GameObject settingsMenu;
    /// <summary>
    /// Аудио
    /// </summary>
    public AudioMixer audioMixer;

    /// <summary>
    /// Выбор разрешения экрана
    /// </summary>
    public Dropdown resolutionsDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            //   options.Add(resolutions[i].width + "x" + resolutions[i].height);

            if ((resolutions[i].width == Screen.currentResolution.width) &&
                (resolutions[i].height == Screen.currentResolution.height))
            {
                currentResolutionIndex = i;
            }

        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        //music in Menu
        mainTheme = gameObject.AddComponent<AudioSource>();
        mainTheme.clip = menuTheme.audioClip;
        mainTheme.volume= PlayerPrefs.GetFloat(ConstsLibrary.musicVolumePrefs, 1)* menuTheme.volumeDecreaser;
        mainTheme.loop = true;
        mainTheme.mute = PlayerPrefs.GetInt(ConstsLibrary.mutedPrefs, 0) == 1;



        PlayFirstMusic();

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
    public void OnBackButtonClick()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void SetSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat(ConstsLibrary.soundEffectVolumePrefs, volume);
    }


    public void SetMisicVolume(float volume)
    {
        if(mainTheme!=null)
        mainTheme.volume = volume * menuTheme.volumeDecreaser;
        PlayerPrefs.SetFloat(ConstsLibrary.musicVolumePrefs, volume);

    }

    public void MuteAll(bool muted)
    {
        mainTheme.mute = muted;
        if (muted)
            PlayerPrefs.SetInt(ConstsLibrary.mutedPrefs, 1);
        else
            PlayerPrefs.SetInt(ConstsLibrary.mutedPrefs, 0);
    }

    private void PlayFirstMusic()
    {
        mainTheme.Play();
    }
}
