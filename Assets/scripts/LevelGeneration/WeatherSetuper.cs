using System;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSetuper : MonoBehaviour {

    public static WeatherSetuper instance;
    public WeatherScriptable[] weathers;

    // Use this for initialization
    void Start()
    {
        instance = instance ?? this;
    }

    public void GenerateWeather(int hardness, int number)
    {
       // print("hardness =>"+ hardness + "number=>"+ number);
        if (number == 0)
        {
            return;
        }
        if (number > 0)
        {
            GenerateSpecialWeather(number - 1);
            return;
        }
        GenerateRandomWeather(hardness);
    }

    private void GenerateSpecialWeather(int number)
    {
        GenerateWeatherInsatnce(number);
    }

    private void GenerateRandomWeather(int hardness)
    {
        List<WeatherScriptable> weatherList = new List<WeatherScriptable>();
        foreach (var item in weathers)
        {
            if (item.weatherHardness == hardness)
            {
                weatherList.Add(item);
            }
        }
        if (weatherList.Count == 0|| weatherList==null)
        {
            return;
        }
        int waetherToSpawn = MainCount.instance.IntegerRandom(0, weatherList.Count);
        GenerateWeatherInsatnce(Array.IndexOf(weathers, weatherList[waetherToSpawn]));
    }



    private void GenerateWeatherInsatnce(int number)
    {
        var currentWeather = weathers[number];
        HeightsAndColors[] heightsAndColors = new HeightsAndColors[currentWeather.activatedSimpleWeathers.Length];
        for (int i = 0; i < currentWeather.activatedSimpleWeathers.Length; i++)
        {
            heightsAndColors[i] = currentWeather.activatedSimpleWeathers[i].heightAndColors;
            SetWeatherToSpawn(currentWeather.activatedSimpleWeathers[i].weatherName);
        }
        SetBackGroud(heightsAndColors);
    }


    private void SetWeatherToSpawn(string weatherName)
    {
        WeatherEngine.instance.ActivateWeather(weatherName);
    }

    private void SetBackGroud(HeightsAndColors[] heightsAndColorsNew)
    {
        //Array.Clear(VisualEffectHelper.instance.heightsAndColors, 0, VisualEffectHelper.instance.heightsAndColors.Length);
        VisualEffectHelper.instance.heightsAndColors = heightsAndColorsNew;
        VisualEffectHelper.instance.CountAllDeltas();
    }
}
