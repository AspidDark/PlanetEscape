using UnityEngine;
using UnityEngine.UI;

public class SoundMenuSetuper : MonoBehaviour
{
    public Toggle isMutedToggle;
    public Slider musicSlider;
    public Slider effectsSlider;

    void Start()
    {
        isMutedToggle.isOn = HelpSaveLoad.GetValue(ConstsLibrary.mutedPrefs, 0) == 1;
        musicSlider.value = HelpSaveLoad.GetValue(ConstsLibrary.musicVolumePrefs, 1f);
        effectsSlider.value = HelpSaveLoad.GetValue(ConstsLibrary.soundEffectVolumePrefs, 1f);
    }
}
