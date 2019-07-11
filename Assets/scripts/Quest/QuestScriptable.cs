using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "MenuItems/Quest")]
public class QuestScriptable : ScriptableObject
{
    public string questName;
    public string questDescription;
    public QuestType questType;
    [Range(ConstsLibrary.minHardness, ConstsLibrary.maxHardness)]
    public int hardness;
    public int completeMoneyBonusFrom;
    public int completeMoneyBonusTo;

    public int costFrom;
    public int costTo;

    public float mainValue;
    public float secondaryValue;
    public float thirdValue;
}
