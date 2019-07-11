using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventsSetuper : MonoBehaviour {

    public static RandomEventsSetuper instance;
    // Use this for initialization
    void Start () {
        instance = instance ?? this;
    }

    public void GenerateRandomEvents(int hardness, int number)
    {
        if (number == 0)
        {
            return;
        }
        if (number > 0)
        {
            GenerateSpecialRandomEvents(number);
            return;
        }
        GenerateRandomRandomEvents(number);
    }

    private void GenerateSpecialRandomEvents(int number)
    {

    }
    //Hmm Rename this?? No!
    private void GenerateRandomRandomEvents(int number)
    {

    }
}
