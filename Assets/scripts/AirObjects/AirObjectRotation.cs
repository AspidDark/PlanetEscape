using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirObjectRotation : MonoBehaviour {

    public float rotationSpeedMin;
    public float rotationSpeedMax;
    float rotationSpeed;
	// Use this for initialization
	void Start ()
    {
        StartingInitiation();

    }
    private void OnEnable()
    {
        StartingInitiation();
    }

    private void OnDisable()
    {
        this.gameObject.transform.Rotate(Vector3.zero);
    }

    // Update is called once per frame
    void Update () {
        this.gameObject.transform.Rotate(new Vector3(0, 0, rotationSpeed * MainCount.instance.deltaTime));
	}

    private void StartingInitiation()
    {
        rotationSpeed = MainCount.instance.FloatRandom(rotationSpeedMin, rotationSpeedMax) * MainCount.instance.PositiveNegativeRandom();
    }

}
