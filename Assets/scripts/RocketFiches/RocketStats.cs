﻿using UnityEngine;

public class RocketStats : MonoBehaviour
{
    public static RocketStats instance;

    //private int firstLineCounter;
    //private int secondLineCounter;
    //private int thirdLineCounter;
    //private int fourthLineCounter;
    //private int fifthLineCounter;
    private void Awake()
    {
        instance = instance ?? this;
    }



    private void Start()
    {
        instance = instance ?? this;
        StartingInitiation();
    }
    private const int nodeCount=4;
    //Starting Stats


    //Max Stats After Upgrade
    public float MaxWeight { get; private set; }
    public float MaxFuelCapacity { get; private set; }
    public float MaxRotation { get; private set; }
    public float MaxEngine { get; private set; }
    public float HeatValue { get; private set; }
    /// <summary>
    /// Update Upgraded stats
    /// </summary>
    public void UpgradeStatsUpdate(NodeStatsDto nodeStatsDto)
    {
        if (nodeStatsDto.isFinalNode)
        {
            GameMaster.instance.WonGame();
            return;
        }
        PlayerStats.instance.AddTechNode();
        MaxWeight += nodeStatsDto.weightNodeChange;
        MaxFuelCapacity += nodeStatsDto.fuelNodeChange;
        MaxRotation += nodeStatsDto.rotationNodeValueChange;
        MaxEngine += nodeStatsDto.engineNodeChane;
        HeatValue += nodeStatsDto.heatNodeChange;
        RocketMovement.instance.UpdateValues();
    }
    /// <summary>
    /// Starting Initiation of Stats
    /// </summary>
    public void StartingInitiation()
    {
        MaxWeight += ConstsLibrary.startingWeight;
        MaxFuelCapacity += ConstsLibrary.startingFuelCapacity;
        MaxRotation += ConstsLibrary.startingRotation;
        MaxEngine += ConstsLibrary.startingEngine;
        HeatValue += ConstsLibrary.startingHeatValue;
        RocketMovement.instance.UpdateValues();

        ////Line Counters
        //firstLineCounter = 0;
        //secondLineCounter = 0;
        //thirdLineCounter = 0;
        //fourthLineCounter = 0;
        //fifthLineCounter = 0;
    }
    public void ResetValues()
    {

    }


    private bool IsFinalTechInRowReached(int numberOfTechs)
    {
        return numberOfTechs >= nodeCount;
    }
}
