using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public static GameMaster instance;

    public int hardness;

    public int iteration;

    public int day;
    [HideInInspector]
    public bool clicked;

    private void Awake()
    {
        instance = instance ?? this;
        SetStartingValues();
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start()
    {
        clicked = false;
        instance = instance ?? this;
        InGameWiever.instance.SetDayText(day);
        // GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!clicked && AllObjectData.instance.isStarted)
        {
            clicked = true;
            ActivationClick();
        }
    }
    //Disable =0   Special>0(needed weather number)  RandomWeather <0(As lower harder)
    private void GenerateLevel(LevelScriptable level)
    {
        hardness = level.hardness;
        GenerateWeather(level.hardness, level.weatherNumber);
        GenerateObjects(level.generationObjectsNumber);
       // print("Actual => " + level.generationObjectsNumber);
        GenerateInGameEvents(level.inGameEventsNumber, level.hardness);
        GenerateInUpgradeMenuEvents(level.hardness, level.inUpgradeMenuEventsNumber);
        GenerateRandomEvents(level.hardness, level.randomEventsNumber);
        SetAllTypes(level.hardness);
    }

    private void GenerateWeather(int hardness, int number)
    {
        WeatherSetuper.instance.GenerateWeather(hardness, number);
    }

    private void GenerateObjects(int number)
    {
        ObjectSetuper.instance.GenerateObjects(number);
    }

    private void GenerateInGameEvents(int number, int hardness)
    {
        InGameEventsStuper.instance.GenerateInGameEvents(number, hardness);
    }
    //No need
    private void GenerateInUpgradeMenuEvents(int hardness, int number)
    {
        InMenuEventsSetuper.instance.GenerateInMenuEvents(hardness, number);
    }
    //No need now
    private void GenerateRandomEvents(int hardness, int number)
    {
        RandomEventsSetuper.instance.GenerateRandomEvents(hardness, number);
    }
    private void SetAllTypes(int hardness)
    {
        NodeInformer.instance.questHardness = hardness;
    }


    public void ActivationClick()
    {
        LevelScriptable level = LevelLibrary.instance.GetLevel(iteration, iteration % 5 == 0);
        if (level == null)
        {
            level = new LevelScriptable
            {
                name="nullName",
                hardness = 0,
                generationObjectsNumber = 0,
                inGameEventsNumber = 0,
                weatherNumber = 0,
                inUpgradeMenuEventsNumber = 0,
                randomEventsNumber = 0
            };
        }
        //print(level.name);
        GenerateLevel(level);
    }


    public void GameMaterReset()
    {
        clicked = false;
    }

    public void AddIteration()
    {
        iteration++;
        HelpSaveLoad.SetValue(ConstsLibrary.iteration, iteration);
    }
    public void AddDay()
    {
        day++;
        HelpSaveLoad.SetValue(ConstsLibrary.day, day);
        InGameWiever.instance.SetDayText(day);
    }

    private void SetStartingValues()
    {
        iteration = HelpSaveLoad.GetValue(ConstsLibrary.iteration, 0);
        day= HelpSaveLoad.GetValue(ConstsLibrary.day, 0);
    }
}

