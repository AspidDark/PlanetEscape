using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStageEngine : MonoBehaviour {

    public static RocketStageEngine instance;

    public RocketEngineParticles rocketEngineParticles;

    public GameObject singleRocketStage;
    public GameObject firstToDropRocketStage;
    public GameObject secondToDropRocketStage;

    public GameObject singleRocketStageDroped;
    public GameObject firstToDropRocketStageDroped;
    public GameObject secondToDropRocketStageDroped;

    private void Awake()
    {
        instance = instance ?? this;
    }

    public void SetRocketType()
    {
        switch (RocketMovement.instance.additionalStagesCount)
        {
            case 1:
                SetParticleToAct(1);
                break;
            case 2:
                SetParticleToAct(5);
                break;
            default:
                SetParticleToAct(0);
                break;
        }
    }

    public void DropSingleStage(Vector3 rbVelocityValue, float rbAngularVelocityValue)
    {
        Vector3 vector = singleRocketStage.transform.position;
        Quaternion quaternion = singleRocketStage.transform.rotation;
        singleRocketStage.SetActive(false);
        SetParticleToAct(2);
        GameObject objectToSpawn = Instantiate(singleRocketStageDroped);
        Rigidbody2D rb = objectToSpawn.GetComponent<Rigidbody2D>();
        rb.velocity = rbVelocityValue;
        rb.angularVelocity = rbAngularVelocityValue;
        objectToSpawn.transform.position = vector;
        objectToSpawn.transform.rotation = quaternion;
        objectToSpawn.SetActive(true);


    }
    public void DropFirstStage(Vector3 rbVelocityValue, float rbAngularVelocityValue)
    {
        Vector3 vector = firstToDropRocketStage.transform.position;
        Quaternion quaternion = firstToDropRocketStage.transform.rotation;
        firstToDropRocketStage.SetActive(false);
        SetParticleToAct(4);
        GameObject objectToSpawn = Instantiate(firstToDropRocketStageDroped);
        Rigidbody2D rb = objectToSpawn.GetComponent<Rigidbody2D>();
        rb.velocity = rbVelocityValue;
        rb.angularVelocity = rbAngularVelocityValue;
        objectToSpawn.transform.position = firstToDropRocketStage.transform.position;
        objectToSpawn.transform.rotation = quaternion;
    }
    public void DropSecondStage(Vector3 rbVelocityValue, float rbAngularVelocityValue)
    {
        Vector3 vector = secondToDropRocketStage.transform.position;
        Quaternion quaternion = secondToDropRocketStage.transform.rotation;
        secondToDropRocketStage.SetActive(false);
        SetParticleToAct(3);
        GameObject objectToSpawn = Instantiate(secondToDropRocketStageDroped);
        Rigidbody2D rb = objectToSpawn.GetComponent<Rigidbody2D>();
        rb.velocity = rbVelocityValue;
        rb.angularVelocity = rbAngularVelocityValue;
        objectToSpawn.transform.position = firstToDropRocketStage.transform.position;
        objectToSpawn.transform.rotation = quaternion;
    }

    private void SetParticleToAct(int number)
    {
        rocketEngineParticles.SetParticleSetNumber(number);
    }

    public void ResetStages()
    {
        singleRocketStage.SetActive(true);
        firstToDropRocketStage.SetActive(true);
        secondToDropRocketStage.SetActive(true);
        SetRocketType();
    }
}
