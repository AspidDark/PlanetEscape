using UnityEngine;

public class ConstsLibrary
{
    #region Rocket Base Stats
    public const float startingWeight = 4;
    public const float startingFuelCapacity = 15;//10
    public const float startingRotation = 1f;//0.5f
    public const float startingEngine = 80;
    public const float startingHeatValue = 0;


    public const float fuelLossDelimeter = 1f;
    #endregion
    public const int steeerEngineUseLessFuel = 4;
    public const float mainEnginePowerDelimeter = 1.5f;

    public const int mainEngineUseLessFuelDelimeter = 8;

    public const int maxXspeedSquare = 900;
    public const float maxXSpeed=30;

    public const float groundSpawnBorder = 50;
    public static readonly Color32 proColor = new Color32(255, 255, 255, 255);//Base color
    public static readonly Color32 redFlashColor = new Color32(227, 15, 15, 255);//Red Flash Color
    public const int heightStartedFrom = 10;
    public const float waitForSecondAfterEnd = 1.5f;
    public const float normalGravityScale = 1;

    public const byte baseRed = 49;
    public const byte baseGreen = 77;
    public const byte baseBlue = 121;
    public const byte baseAlpha = 0;

    public const float surfacelevel = -3.61f;

    public const int minHardness = 0;
    public const int maxHardness = 10;

    public const int maxChance = 100;
    public const int minChance = 0;


    public const int moneyDelimeter = 10;

    public const int maxRocketHeat = 100;
    public const int minRocketHeat = 0;
    public const int disableRocketHeatLevel = 99;

    // public const int maxRocketHealth = 15;
    public const float speedSquareAferSpawnObjectOnMinHeight = 50;
    public const float offsetDelimeterOnHighSpeed = 2;

    public const float heightToSpawnWhenXisHigh = 15;




    public const float startingPositionY = -1.40f;
    public const float startingPositionX = 0.01f;


    #region Informaer messages
    public const string negativeCashToGamePressedHead = "Money is negative";
    public const string negativeCashToGamePressedBody = "You need ti skip day looking for money";

    public const string positiveCashLookForMoneyPressedHead = "Money is positive";
    public const string positiveCashLookForMoneyPressedBody = "Day cant be skipped!!! We Must Fly!!!";

    public const string negativeCashLookForMoneyPressedHead = "Money Added";
    public const string negativeCashLookForMoneyPressedBody = "Spent all day Lookong fore money";
    #endregion

    public const float maxObjectDistance = 120f;

    public const int maxAngleSquare = 90000;
    public const int fullAngle = 360;


    public const float halfOfSecondTimer = 0.5f;
    public const float oneSecondTimer = 1f;
    public const float twoSecondTimer = 2f;


    public const float heightToDestroyIfNotStarted = -3f;
    public const float timeInMinutesToDestroyAfterIfNotstrarted = 3f;


    #region Save Load Pefs Names
    public const string openNodesCount = "OpenNodesCount";
    public const string iteration = "iteration";
    public const string day = "day";
    public const string playerCash = "playerCash";
    public const string cashPerSecond = "cashPerSecond";
    public const string cashPerStart = "cashPerStart";
    public const string cashPerMission = "cashPerMission";
    public const string rocketCrashCost = "rocketCrashCost";
    public const string hardnessPrefs = "hardness";
    public const string rocketStagesValue = "rocketStagesValue";
    //System Values
    public const string soundEffectVolumePrefs = "soundEffectVolume";
    public const string musicVolumePrefs = "musicVolume";
    public const string mutedPrefs = "muted";
    #endregion

    public const int maxIterations = 100;
    public const float heightToreachToWin = 3000f;
    public const string newGameStarted = "NewGameStarted";

    public readonly string[] PrefsToClear = new string[10]
    {
        openNodesCount,
        iteration,
        day,
        playerCash,
        cashPerSecond,
        cashPerStart,
        cashPerMission,
        rocketCrashCost,
        hardnessPrefs,
         rocketStagesValue
    };


}

[System.Serializable]
public enum GameEventType
{
    Impulse,
    RotationImpulse,
    Damage,
    Heat,
    Control,
    Empty
}

[System.Serializable]
public enum QuestType
{
    ReachY,
    ReachX,
    NoDamage,
    HoldTheLine,
    ReachXReachY,
    Servive,
    EarnMoney,
    ReachAngle
}

[System.Serializable]
public enum AirObjectType
{
    Empty,
    DamageDealler,
    Healer,
    Fuel,
    HeatDecreaser,
    HeatIncreaser,
    Money
}

[System.Serializable]
public enum AirObjectClass
{
    Empty,
    SpaceTrash,
    SpaceSheep,
    Cunsumable
}

