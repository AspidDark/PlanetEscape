using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParticleColor : MonoBehaviour {

   // public GameObject go;
    public ParticleSystem ps;

    // Use this for initialization
    void Start () {
		//ps=go.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("A");
            TryChangeParticleColor();
        }
	}

    private void TryChangeParticleColor()
    {
        var col= ps.colorOverLifetime;
        Gradient gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[] 
        {new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.green, 0.191f), new GradientColorKey(Color.green, 0.45f)}, 
        new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(.9f, 0.115f), new GradientAlphaKey(0.0f, 1.0f) });
        col.color = gradient;
    }
}
