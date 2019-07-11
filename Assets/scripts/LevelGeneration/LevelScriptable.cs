using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "MenuItems/LevelObject")]
public class LevelScriptable : ScriptableObject
{
    public string levelName;
    [Range(ConstsLibrary.minHardness, ConstsLibrary.maxHardness)]
    public int hardness;
    public int generationObjectsNumber;
    public int inGameEventsNumber;
    public int weatherNumber;
    public int inUpgradeMenuEventsNumber;
    public int randomEventsNumber;
    public bool specialLevel = false;
}
