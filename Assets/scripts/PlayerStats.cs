using UnityEngine;

public class PlayerStats : MonoBehaviour
{
     
    public float tmpChash;
    public static PlayerStats instance;

    public float startingCashPerMission = 50f;
    public float startingRocketCrashCost = 5;
    public float startingCashPerStart = 20f;
    public float startingCashPerSecond = 1;
    public float skipDayButtonClickedMoney = 20;

    #region //Economic Stats
    public float playerCash;
    public float cashPerSecond;
    public float cashPerStart;
    public float cashPerMission;
    public float rocketCrashCost;
    #endregion
    //public float playerCash { get { return playerCash; } set { if (playerCash < 0) playerCash = 0; } }

    public float inMissionEarnedCash;


    private bool stopAddingMoney;

    private float moneyTimer;
    private float moneyTimerCD=3f/ConstsLibrary.moneyDelimeter;

    public int openTechNodesCount;
    public int openEconomicNodesCount;

    // Use this for initialization
    private void Awake()
    {
        instance = instance ?? this;
    }

    void Start()
    {
        //Starting Initiation
        instance = instance ?? this;

        SetDefaults();

    }

    // Update is called once per frame
    void Update()
    {
        if (AllObjectData.instance.isStarted&&!stopAddingMoney)
        {
            if (AllObjectData.instance.rocketLanded)
            {
                if (AllObjectData.instance.rocketDestroyed||!AllObjectData.instance.isSafeLanded)
                {
                    AddCash(-rocketCrashCost);
                }
                stopAddingMoney = true;
            }
            else
            {
                moneyTimer -= MainCount.instance.deltaTime;
                if (moneyTimer < 0)
                {
                    moneyTimer = moneyTimerCD;
                    inMissionEarnedCash += cashPerSecond/ ConstsLibrary.moneyDelimeter;
                    AddCash(cashPerSecond/ ConstsLibrary.moneyDelimeter);
                }
            }
        }
        #region//For Testing
        if (Input.GetKeyDown("m"))
        {
            playerCash += tmpChash;
            print("CashCame" + playerCash);
        }
        if (Input.GetKeyDown("o"))
        {
            PlayerPrefs.DeleteAll();
            print("DeleteAll");
        }
        if (Input.GetKeyDown("k"))
        {
            RocketMovement.instance.AddFuel(15);
        }

        ////////////Ok
        //if (Input.GetKeyDown("u"))
        //{
        //    RocketMovement.instance.DoHeat(10);
        //    print("Add Heat" + 10.ToString());
        //}

        //if (Input.GetKeyDown("j"))
        //{
        //    RocketMovement.instance.DoHeat(-15);
        //    print("Remove Heat" + 15.ToString());
        //}

        /////////////OK
        //if (Input.GetKeyDown("y"))
        //{
        //    RocketMovement.instance.DoDamage(1);
        //    print("DoDamage" + 1.ToString());
        //}
        //if (Input.GetKeyDown("h"))
        //{
        //    RocketMovement.instance.DoDamage(-2);
        //    print("Heal" + 2.ToString());
        //}

        #endregion
        InGameWiever.instance.SetMoneyText(playerCash);
    }

    public void SetDefaults()
    {
        LoadCurrent();
        inMissionEarnedCash = 0;
        stopAddingMoney = false;
        moneyTimer = moneyTimerCD;
       
    }

    public void AddCash(float cash)
    {
        playerCash += cash;
        //Remove if we can have... borrow?
        //if (playerCash < 0)
        //{
        //    playerCash = 0;
        //}
        HelpSaveLoad.SetValue(ConstsLibrary.playerCash, playerCash);
    }



    public void ChangeCashPerSecond(float cash)
    {
        cashPerSecond += cash;
        HelpSaveLoad.SetValue(ConstsLibrary.cashPerSecond, cashPerSecond);
    }

    public void ChangeCashPerStart(float cash)
    {
        cashPerStart += cash;
        HelpSaveLoad.SetValue(ConstsLibrary.cashPerStart, cashPerStart);
    }

    public void ChangeCashPerMission(float cash)
    {
        cashPerMission += cash;
        HelpSaveLoad.SetValue(ConstsLibrary.cashPerMission, cashPerMission);
    }

    public void ChangeRocketCrashCost(float cash)
    {
        rocketCrashCost += cash;
        HelpSaveLoad.SetValue(ConstsLibrary.rocketCrashCost, rocketCrashCost);
    }


    public void SaveCurrentAll()
    {
        HelpSaveLoad.SetValue(ConstsLibrary.playerCash, playerCash);
        HelpSaveLoad.SetValue(ConstsLibrary.cashPerSecond, cashPerSecond);
        HelpSaveLoad.SetValue(ConstsLibrary.cashPerStart, cashPerStart);
        HelpSaveLoad.SetValue(ConstsLibrary.cashPerMission, cashPerMission);
        HelpSaveLoad.SetValue(ConstsLibrary.rocketCrashCost, rocketCrashCost);
    }

    public void LoadCurrent()
    {
        playerCash = HelpSaveLoad.GetValue(ConstsLibrary.playerCash, 0f);
        cashPerSecond= HelpSaveLoad.GetValue(ConstsLibrary.cashPerSecond, startingCashPerSecond);
        cashPerStart = HelpSaveLoad.GetValue(ConstsLibrary.cashPerStart, startingCashPerStart);
        cashPerMission = HelpSaveLoad.GetValue(ConstsLibrary.cashPerMission, startingCashPerMission);
        rocketCrashCost = HelpSaveLoad.GetValue(ConstsLibrary.rocketCrashCost, startingRocketCrashCost);
    }


    public void AddTechNode()
    {
        openTechNodesCount++;
        OpenTechNodesCount(openTechNodesCount);
    }
    public void AddEcconomicNode()
    {
        openEconomicNodesCount++;
    }

    public void DaySkiped()
    {
        AddCash(skipDayButtonClickedMoney);
        GameMaster.instance.AddDay();
    }


    public void OpenTechNodesCount(int count)
    {
        HelpSaveLoad.SetValue(ConstsLibrary.openNodesCount, count);
        MaxSpeedSetuper(count);
        MaxHealthSetuper(count);
        EngineColorSetuper(count);
        if (count == 0)
        {
            return;
        }
        if (count >= 3)
        {
            InGameWiever.instance.EventInformerActivator();
        }
        else
        {
            InGameWiever.instance.EventInformerActivator(false);
        }
        if (count > 4)
        {
            InGameWiever.instance.HeightCheckerActivation();
        }
        else
        {
            InGameWiever.instance.HeightCheckerActivation(false);
        }

        if (count >= 8)
        {
            RocketMovement.instance.AdditionalStagesCount = 1;
        }
        if (count >= 12)
        {
            RocketMovement.instance.AdditionalStagesCount = 2;
        }
        if (count >= 16)
        {
            InGameWiever.instance.ClosestObjectActivation();
        }
        else
        {
            InGameWiever.instance.ClosestObjectActivation(false);
        }
    }

    private void MaxSpeedSetuper(int count)
    {
        if (count > 12)
        {
            RocketMovement.instance.maxSpeed = 45;
            return;
        }
        if (count > 8)
        {
            RocketMovement.instance.maxSpeed = 40;
            return;
        }
        if (count > 4)
        {
            RocketMovement.instance.maxSpeed = 35;
            return;
        }
        RocketMovement.instance.maxSpeed = 30;
    }

    private void MaxHealthSetuper(int count)
    {
        if (count > 16)
        {
            RocketMovement.instance.maxRocketHealth = 20;
            return;
        }
        if (count > 12)
        {
            RocketMovement.instance.maxRocketHealth = 16;
            return;
        }
        if (count > 8)
        {
            RocketMovement.instance.maxRocketHealth = 14;
            return;
        }
        if (count > 4)
        {
            RocketMovement.instance.maxRocketHealth = 12;
            return;
        }
        RocketMovement.instance.maxRocketHealth = 10;

    }

    private void EngineColorSetuper(int count)
    {
        if (count > 17)
        {
            RocketMovement.instance.particleClorNumber = 4;
            return;
        }
        if (count > 13)
        {
            RocketMovement.instance.particleClorNumber = 3;
            return;
        }
        if (count > 9)
        {
            RocketMovement.instance.particleClorNumber = 2;
            return;
        }
        if (count > 5)
        {
            RocketMovement.instance.particleClorNumber = 1;
            return;
        }
        RocketMovement.instance.particleClorNumber = 0;
    }
}
