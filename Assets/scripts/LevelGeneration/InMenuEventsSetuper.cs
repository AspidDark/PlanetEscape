using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMenuEventsSetuper : MonoBehaviour {

    public static InMenuEventsSetuper instance;
    // Use this for initialization
    void Start()
    {
        instance = instance ?? this;
    }

    public void GenerateInMenuEvents(int hardness, int number)
    {
        if (number == 0)
        {
            return;
        }
        if (number > 0)
        {
            GenerateSpecialInMenuEvents(number);
            return;
        }
        GenerateRandomInMenuEvents(number);
    }

    private void GenerateSpecialInMenuEvents(int number)
    {

    }

    private void GenerateRandomInMenuEvents(int number)
    {

    }
}
