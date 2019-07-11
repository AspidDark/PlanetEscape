using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelLibrary: MonoBehaviour
{
    public static LevelLibrary instance;

    private void Awake()
    {
        instance = instance ?? this;
    }

    private void Start()
    {
        instance = instance ?? this;
    }

    public LevelScriptable[] levelScriptables;

    public LevelScriptable GetLevel(int iteration, bool special)
    {
        int finalhardness = HardnessCount(iteration);
        
        List<LevelScriptable> chosenlevelScriptables = levelScriptables.Where(a=>a.hardness==(finalhardness)).Where(a=>a.specialLevel== special).ToList();
        if (chosenlevelScriptables.Count > 0)
        {
            int numberOfLevelToSpawn = MainCount.instance.IntegerRandom(0, chosenlevelScriptables.Count);
            return chosenlevelScriptables[numberOfLevelToSpawn];
        }
        return null;
    }

    private int HardnessCount(int iteration)
    {
        if (iteration <= 5)
        {
            return 0;
        }
        if (iteration<=10)
        {
            if (PlayerStats.instance.openTechNodesCount < 3)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        if (iteration <= 15)
        {
            if (PlayerStats.instance.openTechNodesCount < 4)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        if (iteration <= 20)
        {
            if (PlayerStats.instance.openTechNodesCount < 6)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        if (iteration <= 25)
        {
            if (PlayerStats.instance.openTechNodesCount < 7)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        if (iteration <= 30)
        {
            if (PlayerStats.instance.openTechNodesCount < 9)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }
        if (iteration <= 35)
        {
            if (PlayerStats.instance.openTechNodesCount < 10)
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }
        if (iteration <= 40)
        {
            if (PlayerStats.instance.openTechNodesCount < 12)
            {
                return 6;
            }
            else
            {
                return 7;
            }
        }
        if (iteration <= 45)
        {
            if (PlayerStats.instance.openTechNodesCount < 13)
            {
                return 7;
            }
            else
            {
                return 8;
            }
        }
        if (iteration <= 60)
        {
            if (PlayerStats.instance.openTechNodesCount < 13|| PlayerStats.instance.openEconomicNodesCount<5)
            {
                return 8;
            }
            else
            {
                return 9;
            }
        }
        if (iteration <= 75)
        {
            if (PlayerStats.instance.openTechNodesCount < 15 || PlayerStats.instance.openEconomicNodesCount < 7)
            {
                return 9;
            }
            else
            {
                return 10;
            }
        }
        return 10;
    }

    private LevelScriptable GetRandomLevel(int finalhardness)
    {
        LevelScriptable levelData = new LevelScriptable
        {

        };
        return levelData;
    }

}


