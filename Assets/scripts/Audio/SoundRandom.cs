using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandom : MonoBehaviour
{
    public static SoundRandom instance;
    public string[] smallExposions;
    public string[] trashCrush;
    public string[] screamFalling; 
    private void Awake()
    {
        instance = instance ?? this;
    }
    private void Start()
    {
        instance = instance ?? this;
    }

    public void ExecuteRandomTrashCrush()
    {
        int trashSoundToExecute = MainCount.instance.IntegerRandom(0, trashCrush.Length);
        SoundManager.instance.PlaySound(trashCrush[trashSoundToExecute]);
    }

    public void ExecuteRandomSmallExpolsion()
    {
        int smallExposionsSoundToExecute = MainCount.instance.IntegerRandom(0, smallExposions.Length);
        SoundManager.instance.PlaySound(smallExposions[smallExposionsSoundToExecute]);
    }

    public void ExecuteFallingSound()
    {
        int fallSoundToExecute = MainCount.instance.IntegerRandom(0, screamFalling.Length);
        SoundManager.instance.PlaySound(screamFalling[fallSoundToExecute]);
    }
}
