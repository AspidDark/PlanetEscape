using UnityEngine;

public class TempWeatherActivator : MonoBehaviour
{

    public string[] weathers;
    public string[] weatherFronts;
    public bool activateWeather;
    void Start()
    {
        if (activateWeather)
        {
            if (weathers != null)
            {
                foreach (var item in weathers)
                {
                    WeatherEngine.instance.ActivateWeather(item);
                }
            }
        }
    }
}
