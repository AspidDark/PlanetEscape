using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameEventsStuper : MonoBehaviour
{

    public static InGameEventsStuper instance;
    public FlyEvent[] flyEvents;

    // Use this for initialization
    void Start()
    {
        instance = instance ?? this;
    }

    public void GenerateInGameEvents(int number, int hardness)
    {
        if (number == 0)
        {
            return;
        }
        if (number > 0)
        {
            GenerateSpecialInGameEvents(number-1);
            return;
        }
        GenerateRandomInGameEvents(hardness);
    }

    private void GenerateSpecialInGameEvents(int number)
    {
        if (number < flyEvents.Length)
        {
            Array.Clear(FlyEvents.instance.flyEvents, 0, FlyEvents.instance.flyEvents.Length);
            FlyEvents.instance.flyEvents[0] = flyEvents[number];
        }
    }

    private void GenerateRandomInGameEvents(int hardness)
    {
        List<FlyEvent> flyEventsList = new List<FlyEvent>();
        foreach (var item in flyEvents)
        {
            if (item.eventHardness == hardness)
            {
                flyEventsList.Add(item);
            }
        }
        if (flyEventsList.Count == 0 || flyEventsList == null)
        {
            return;
        }
        GenerateEvent(flyEventsList.ToArray());
    }

    private void GenerateEvent(FlyEvent[] flyEvent)
    {
        FlyEvents.instance.flyEvents = flyEvent;
    }
}
