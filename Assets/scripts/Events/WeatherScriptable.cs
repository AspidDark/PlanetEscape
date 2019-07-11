using UnityEngine;
[CreateAssetMenu(fileName = "NewWeatherSet", menuName = "Setup/New Weather")]
public class WeatherScriptable : ScriptableObject
{
    public string weatherSetName;
    [Range(ConstsLibrary.minHardness, ConstsLibrary.maxHardness)]
    public int weatherHardness;
    public SimpleWeather[] activatedSimpleWeathers;
}
[System.Serializable]
public class SimpleWeather
{
    public string weatherName;
    public HeightsAndColors heightAndColors;

}
