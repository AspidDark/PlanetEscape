using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ParticleSet
{
    public ParticleSystem mainEngineParticle;
    public ParticleSystem leftSteerEngineParticle;
    public ParticleSystem rightSteerEngineParticle;
}

[Serializable]
public class GradientColor
{
    public Color color;
    [Range(0,1)]
    public float timePosition;
}

[Serializable]
public class GradientAlpha
{
    [Range(0, 1)]
    public float alphaValue;
    [Range(0, 1)]
    public float timePosition;
}

[Serializable]
public class EngineFireSet
{
    public float maxMainEngineParticleSize = 0.9f;
    public float minMainEngineParticleSize = 0.7f;
    public float maxMainEngineParticleStartSpeed = 2.6f;
    public float minMainEngineParticleStartSpeed = 2f;

    public float maxSteerEngineParticleSize = 0.2f;
    public float minSteerEngineParticleSize = 0.24f;
    public float maxSteerEngineParticleStartSpeed = 1f;
    public float minSteerEngineParticleStartSpeed = 0.8f;

    public GradientColor[] gradientColors;
    public GradientAlpha[] gradientAlphas;
}

public class RocketEngineParticles : MonoBehaviour
{
    public static RocketEngineParticles instance;
    public ParticleSet[] particleSet;

    //private ParticleSystem mainEngineParticle;
    //private ParticleSystem leftSteerEngineParticle;
    //private ParticleSystem rightSteerEngineParticle;


    public EngineFireSet[] engineFireSets;

    private float maxMainEngineParticleSize = 0.9f;
    private float minMainEngineParticleSize = 0.7f;
    private float maxMainEngineParticleStartSpeed = 2.6f;
    private float minMainEngineParticleStartSpeed = 2f;

    private float maxSteerEngineParticleSize = 0.2f;
    private float minSteerEngineParticleSize = 0.24f;
    private float maxSteerEngineParticleStartSpeed = 1f;
    private float minSteerEngineParticleStartSpeed = 0.8f;

    public int nowActingParticleColorNumber;
    public int particleSetNum=0;


    private void Start()
    {
        instance = instance ?? this;
        SetDefaults();
    }

    public void SetDefaults()
    {
        foreach (var item in particleSet)
        {
            item.mainEngineParticle.gameObject.SetActive(false);
            item.leftSteerEngineParticle.gameObject.SetActive(false);
            item.rightSteerEngineParticle.gameObject.SetActive(false);
        }
    }
    public void SetFireSetNumber(int value)
    {
        if (engineFireSets[value] != null)
        {
            #region //Values Limit
            maxMainEngineParticleSize = engineFireSets[value].maxMainEngineParticleSize;
            minMainEngineParticleSize = engineFireSets[value].minMainEngineParticleSize;
            maxMainEngineParticleStartSpeed = engineFireSets[value].maxMainEngineParticleStartSpeed;
            minMainEngineParticleStartSpeed = engineFireSets[value].minMainEngineParticleStartSpeed;

            maxSteerEngineParticleSize = engineFireSets[value].maxSteerEngineParticleSize;
            minSteerEngineParticleSize = engineFireSets[value].minSteerEngineParticleSize;
            maxSteerEngineParticleStartSpeed = engineFireSets[value].maxSteerEngineParticleStartSpeed;
            minSteerEngineParticleStartSpeed = engineFireSets[value].minSteerEngineParticleStartSpeed;
            #endregion
            #region //Color Set
            var mainEngineParticleColor = particleSet[particleSetNum].mainEngineParticle.colorOverLifetime;
            var leftSteerEngineParticleColor = particleSet[particleSetNum].leftSteerEngineParticle.colorOverLifetime;
            var rightSteerEngineParticleColor = particleSet[particleSetNum].rightSteerEngineParticle.colorOverLifetime;
            GradientColorKey[] gradientColorKey = new GradientColorKey[engineFireSets[value].gradientColors.Length];
            for (int i = 0; i < engineFireSets[value].gradientColors.Length; i++)
            {
                gradientColorKey[i].color = engineFireSets[value].gradientColors[i].color;
                gradientColorKey[i].time = engineFireSets[value].gradientColors[i].timePosition;
            }
            GradientAlphaKey[] gradientAlphaKey = new GradientAlphaKey[engineFireSets[value].gradientAlphas.Length];
            for (int i = 0; i < engineFireSets[value].gradientAlphas.Length; i++)
            {
                gradientAlphaKey[i].alpha = engineFireSets[value].gradientAlphas[i].alphaValue;
                gradientAlphaKey[i].time = engineFireSets[value].gradientAlphas[i].timePosition;
            }
            Gradient gradient = new Gradient();
            gradient.SetKeys(gradientColorKey, gradientAlphaKey);
            mainEngineParticleColor.color = gradient;
            leftSteerEngineParticleColor.color = gradient;
            rightSteerEngineParticleColor.color = gradient;

            nowActingParticleColorNumber = value;
            #endregion

        }

    }

    public void SetParticleSetNumber(int number)
    {
        particleSetNum = number;
        //Debug.Log("SetParticleSetNumber --> " + number);
        //if (particleSet[number] != null)
        //{
        //    mainEngineParticle = particleSet[number].mainEngineParticle;
        //    leftSteerEngineParticle = particleSet[number].leftSteerEngineParticle;
        //    rightSteerEngineParticle = particleSet[number].rightSteerEngineParticle;
        //}
        SetFireSetNumber(nowActingParticleColorNumber);
    }

    #region main engine
    public void SetSize(float amount)
    {
        if (amount <= 0.001f)
        {
            particleSet[particleSetNum].mainEngineParticle.gameObject.SetActive(false);
            return;
        }
        else
        {
            particleSet[particleSetNum].mainEngineParticle.gameObject.SetActive(true);
        }
        SetStartSpeed(amount * 3);
        SetStartSize(amount);
    }

    private void SetStartSpeed(float amount)
    {
        if (amount > maxMainEngineParticleStartSpeed)
        {
            amount = maxMainEngineParticleStartSpeed;
        }
        if (amount < minMainEngineParticleStartSpeed)
        {
            amount = minMainEngineParticleStartSpeed;
        }
        particleSet[particleSetNum].mainEngineParticle.startSpeed = amount;
    }
    private void SetStartSize(float amount)
    {
        if (amount > maxMainEngineParticleSize)
        {
            amount = maxMainEngineParticleSize;
        }
        if (amount < minMainEngineParticleSize)
        {
            amount = minMainEngineParticleSize;
        }
        particleSet[particleSetNum].mainEngineParticle.startSize = amount;
    }
    #endregion

    #region Left engine
    public void SetLeftSize(float amount)
    {
        
        if (amount <= 0.001f)
        {
            particleSet[particleSetNum].leftSteerEngineParticle.gameObject.SetActive(false);
            return;
        }
        else
        {
            particleSet[particleSetNum].leftSteerEngineParticle.gameObject.SetActive(true);
        }
        SetLeftStartSpeed(amount * 2);
        SetLeftStartSize(amount);
    }

    private void SetLeftStartSpeed(float amount)
    {
        if (amount > maxSteerEngineParticleStartSpeed)
        {
            amount = maxSteerEngineParticleStartSpeed;
        }
        if (amount < minSteerEngineParticleStartSpeed)
        {
            amount = minSteerEngineParticleStartSpeed;
        }
        particleSet[particleSetNum].leftSteerEngineParticle.startSpeed = amount;
    }
    private void SetLeftStartSize(float amount)
    {
        if (amount > maxSteerEngineParticleSize)
        {
            amount = maxSteerEngineParticleSize;
        }
        if (amount < minSteerEngineParticleSize)
        {
            amount = minSteerEngineParticleSize;
        }
        particleSet[particleSetNum].leftSteerEngineParticle.startSize = amount;
    }
    #endregion

    #region Right engine
    public void SetRightSize(float amount)
    {
        
        if (amount <= 0.001f)
        {
            particleSet[particleSetNum].rightSteerEngineParticle.gameObject.SetActive(false);
            return;
        }
        else
        {
            particleSet[particleSetNum].rightSteerEngineParticle.gameObject.SetActive(true);
        }
        SetRightStartSpeed(amount * 2);
        SetRightStartSize(amount);
    }

    private void SetRightStartSpeed(float amount)
    {
        if (amount > maxSteerEngineParticleStartSpeed)
        {
            amount = maxSteerEngineParticleStartSpeed;
        }
        if (amount < minSteerEngineParticleStartSpeed)
        {
            amount = minSteerEngineParticleStartSpeed;
        }
        particleSet[particleSetNum].rightSteerEngineParticle.startSpeed = amount;
    }
    private void SetRightStartSize(float amount)
    {
        if (amount > maxSteerEngineParticleSize)
        {
            amount = maxSteerEngineParticleSize;
        }
        if (amount < minSteerEngineParticleSize)
        {
            amount = minSteerEngineParticleSize;
        }
        particleSet[particleSetNum].rightSteerEngineParticle.startSize = amount;
    }
    #endregion

}
