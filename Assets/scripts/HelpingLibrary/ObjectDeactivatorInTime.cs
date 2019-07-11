using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class ObjectDeactivatorInTime : MonoBehaviour {

    public float deactivationTime=1.5f;

    ParticleSystem particle;
    private void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();  
    }
    private void OnEnable()
    {
       // particle.Play();
        Invoke("Disabler", deactivationTime);
    }

    private void Disabler()
    {
        //  particle.Clear();
        gameObject.SetActive(false);
    }
}
