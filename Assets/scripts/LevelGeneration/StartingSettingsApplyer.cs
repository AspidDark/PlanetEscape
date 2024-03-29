﻿using UnityEngine;

public class StartingSettingsApplyer : MonoBehaviour {

    public static StartingSettingsApplyer instance;

    private void Awake()
    {
        instance = instance ?? this;
      //  DontDestroyOnLoad(gameObject);
    }
    void Start () {
        instance = instance ?? this;
        LevelInitiation();
    }
    //Set Particles+
    //Set Starting Menu

    private void LevelInitiation()
    {
        int nodesCount = HelpSaveLoad.GetValue(ConstsLibrary.openNodesCount, 0);
        PlayerStats.instance.OpenTechNodesCount(nodesCount);
        GameMaster.instance.day = GameMaster.instance.iteration;
     //   print("day LevelInitiation " + day);
        HelpSaveLoad.SetValue(ConstsLibrary.day, GameMaster.instance.iteration);
        InGameWiever.instance.SetDayText(GameMaster.instance.iteration);
        GameMaster.instance.hardness = HelpSaveLoad.GetValue(ConstsLibrary.hardnessPrefs, 0);
    }

}
