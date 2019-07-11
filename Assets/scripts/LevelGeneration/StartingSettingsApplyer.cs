using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSettingsApplyer : MonoBehaviour {

    public static StartingSettingsApplyer instance;

    private void Awake()
    {
        instance = instance ?? this;
    }
    void Start () {
        instance = instance ?? this;
    }

    //Set Particles
    //Set Hardness
    //Set startingInitiation
    //Set Starting Menu
	
	// Update is called once per frame
	void Update () {
		
	}
}
