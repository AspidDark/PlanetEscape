using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPlacer : MonoBehaviour {


    public float xSize;
    public float ySize;
    [Space]
    public float xCamera;
    public float yCamera;


	// Use this for initialization
	void Start () {
        xSize = Screen.width;
        ySize = Screen.height;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
