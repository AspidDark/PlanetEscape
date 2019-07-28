using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class WetherFollowEffectStats
{
    public float fromHeight;
    public float toHeight;
    public GameObject weatherEffectPrefab;
    public string weatherName;
    public string upperBorderEffect;
    public string lowerBorderEffect;
}

public class WeatherEngine : MonoBehaviour
{
    public static WeatherEngine instance;

    public WetherFollowEffectStats[] allWetherEffectStats;
    private List<WetherFollowEffectStats> thisRoundWetherEffectStats = new List<WetherFollowEffectStats>();

    public float borderAdd;
    // Use this for initialization
    void Start()
    {
        instance = instance ?? this;
      //  StartingInitiation();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in thisRoundWetherEffectStats)
        {
            if (AllObjectData.instance.posY >= item.fromHeight && AllObjectData.instance.posY <= item.toHeight)
            {
                item.weatherEffectPrefab.SetActive(true);
            }
            else
            {
                item.weatherEffectPrefab.SetActive(false);
            }
        }
        
    }

    public void StartingInitiation()
    {
        foreach (var item in allWetherEffectStats)
        {
            item.weatherEffectPrefab.SetActive(false);
        }

    }

    public void ActivateWeather(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }
        WetherFollowEffectStats weather = allWetherEffectStats.Where(n => n.weatherName == name).FirstOrDefault();
        if (weather != null)
        {
            thisRoundWetherEffectStats.Add(weather);
            Vector3 vector3Lower = new Vector3(0, weather.fromHeight, 0);
            ObjectPoolList.instance.GetPooledObject(weather.lowerBorderEffect, vector3Lower, Quaternion.identity);
            Vector3 vector3Upper = new Vector3(0, weather.toHeight, 0);
            ObjectPoolList.instance.GetPooledObject(weather.lowerBorderEffect, vector3Upper, Quaternion.identity);
        }
    }
}
