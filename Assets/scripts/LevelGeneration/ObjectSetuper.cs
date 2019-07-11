using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetuper : MonoBehaviour {

    public static ObjectSetuper instance;

    public AirObjectByType[] airObjectByTypeArray;

    void Start () {
        instance = instance ?? this;
    }

    public void GenerateObjects(int number)
    {
        if (number > 0)
        {
            GenerateSpecialObjects(number);
            return;
        }
        GenerateRandomObjects(number);
    }

    private void GenerateSpecialObjects(int number)
    {
        if (number > airObjectByTypeArray.Length)
        {
            return;
        }

        Array.Clear(AirObjectGenerator.instance.airObjectByTypeArray, 0, AirObjectGenerator.instance.airObjectByTypeArray.Length);
        //if (AirObjectGenerator.instance.airObjectByTypeArray.Length >= 0)
        //{
        //AirObjectGenerator.instance.airObjectByTypeArray[0] = airObjectByTypeArray[number];
        //}
        AirObjectGenerator.instance.airObjectByTypeArray = new AirObjectByType[1] { airObjectByTypeArray[number] };
        // AirObjectGenerator.instance.HeightCheckTimerUpdate();
    }

    private void GenerateRandomObjects(int number)
    {
        List<AirObjectByType> airObjectList = new List<AirObjectByType>();
        foreach (var item in airObjectByTypeArray)
        {
            if (item.hardness == Mathf.Abs(number))
            {
                airObjectList.Add(item);
            }
        }
        if (airObjectList != null && airObjectList.Count > 0)
        {
            AirObjectGenerator.instance.airObjectByTypeArray = airObjectList.ToArray();
        }
       // AirObjectGenerator.instance.HeightCheckTimerUpdate();
    }
}
