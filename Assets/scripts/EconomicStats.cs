using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicStats : MonoBehaviour
{

    public static EconomicStats instance;

    private void Awake()
    {
        instance = instance ?? this;
    }

    private void Start()
    {
        instance = instance ?? this;
    }


    public void EconomicResearchDone(string name)
    {
        PlayerStats.instance.AddEcconomicNode();
        switch (name)
        {
            //barnch 1 tech 1 stage 1 
            case "MoneyPerSecond1":
                MoneyPerSecond1();
                break;
            //barnch 1 tech 2 stage 1 
            case "Enshuranse":
                Enshuranse();
                break;
            //barnch 2 tech 1 stage 1
            case "MissionMutypyer":
                MissionMutypyer();
                break;
            //barnch 2 tech 2 stage 1 
            case "GovenmentProgram1":
                GovenmentProgram1();
                break;
            //barnch 1 tech 1 stage 2
            case "MoneyPerSecond2":
                MoneyPerSecond2();
                break;
            //barnch 2 tech 1 stage 2
            case "GovenmentProgram2":
                GovenmentProgram2();
                break;
            //barnch 1 tech 1 stage 3
            case "Enthusiasts":
                Enthusiasts();
                break;
            //barnch 2 tech 1 stage 3
            case "Corporations":
                Corporations();
                break;
            //barnch 0(united) tech 1 stage 4
            case "SpaceCulture":
                SpaceCulture();
                break;
            //barnch 0(united) tech 1 stage 4
            case "SpaceMania":
                SpaceMania();
                break;
            default:
                break;
        }
    }

    //barnch 1 tech 1 stage 1
    private void MoneyPerSecond1()
    {
        PlayerStats.instance.ChangeCashPerSecond(1);
        PlayerStats.instance.ChangeRocketCrashCost(10);
    }

    //barnch 1 tech 2 stage 1
    private void Enshuranse()
    {
        PlayerStats.instance.ChangeRocketCrashCost(-10);
    }

    //barnch 2 tech 1 stage 1
    private void MissionMutypyer()
    {
        PlayerStats.instance.ChangeCashPerMission(0.5f);
    }

    //barnch 2 tech 2 stage 1
    private void GovenmentProgram1()
    {
        PlayerStats.instance.ChangeCashPerSecond(1);
        PlayerStats.instance.ChangeRocketCrashCost(15);
        PlayerStats.instance.ChangeCashPerStart(10);
    }

    //barnch 1 tech 1 stage 2
    private void MoneyPerSecond2()
    {
        PlayerStats.instance.ChangeCashPerSecond(2);
        PlayerStats.instance.ChangeRocketCrashCost(15);
    }

    //barnch 2 tech 1 stage 2
    private void GovenmentProgram2()
    {
        PlayerStats.instance.ChangeCashPerSecond(1);
        PlayerStats.instance.ChangeRocketCrashCost(10);
        PlayerStats.instance.ChangeCashPerStart(5);
    }

    //barnch 1 tech 1 stage 3
    private void Enthusiasts()
    {
        PlayerStats.instance.ChangeCashPerStart(20);
        PlayerStats.instance.ChangeRocketCrashCost(10);
    }

    //barnch 2 tech 1 stage 3
    private void Corporations()
    {
        PlayerStats.instance.ChangeCashPerMission(0.5f);
        PlayerStats.instance.ChangeRocketCrashCost(10);
    }

    //barnch 0(united) tech 1 stage 4
    private void SpaceCulture()
    {
        PlayerStats.instance.ChangeCashPerSecond(2);
        PlayerStats.instance.ChangeRocketCrashCost(10);
    }

    //barnch 0(united) tech 1 stage 4
    private void SpaceMania()
    {
        PlayerStats.instance.ChangeCashPerSecond(3);
        PlayerStats.instance.ChangeRocketCrashCost(50);
        PlayerStats.instance.ChangeCashPerMission(1);
    }



}
