using System.Collections.Generic;
using UnityEngine;

public class QuestMainEngine : MonoBehaviour
{

    public static QuestMainEngine instance;
    public List<QuestScriptable> questScriptableList;

    public bool questApplyedPressed;


    public int questCostDecrease;
    public int questAdditionalMoney;

    private CurrentSelectedQuest currentQuest = new CurrentSelectedQuest();
    private bool questCompletedSemaphore;

    #region Quest vars
    public bool angleCrossed;
    #endregion
    private float questCheckTimer = 0;

    private void Start()
    {
        instance = instance ?? this;
        questApplyedPressed = false;
        ResetQuest();
    }

    private void Awake()
    {
        //if (questScriptableList == null || questScriptableList.Count == 0)
        //{
        //    GetAllScriptableObjectInstances getAllScriptableObjectInstances = new GetAllScriptableObjectInstances();
        //    questScriptableList = getAllScriptableObjectInstances.GetAllInstances<QuestScriptable>();
        //}
    }
    private void Update()
    {
        
        if (!questApplyedPressed|| currentQuest == null)
        {
            return;
        }
        questCheckTimer -= MainCount.instance.deltaTime;

        if (questCheckTimer > 0)
        {
            return;
        }
        questCheckTimer = ConstsLibrary.halfOfSecondTimer;
        switch (currentQuest.currentQuestScriptable.questType)
        {
            case QuestType.NoDamage:
                NoDamage();
                break;
            case QuestType.EarnMoney:
                EarnMoney();
                break;
            case QuestType.HoldTheLine:
                HoldTheLine();
                break;
            case QuestType.ReachAngle:
                ReachAngle();
                break;
            case QuestType.ReachX:
                ReachX();
                break;
            case QuestType.ReachXReachY:
                ReachXReachY();
                break;
            case QuestType.ReachY:
                ReachY();
                break;
            case QuestType.Servive:
                Servive();
                break;
        }
    }
    private void QuestComoleted()
    {
        if (questCompletedSemaphore)
        {
            return;
        }
     print("<color=red>QUEST COMPLETED QUEST COMPLETED QUEST COMPLETED QUEST COMPLETED QUEST COMPLETED QUEST COMPLETED </color>");
        questCompletedSemaphore = true;
        InGameWiever.instance.QuestCompletedInformer("Quest Comoleted, Earned : " + currentQuest.questBringMoneyIfCompleted.ToString());
        PlayerStats.instance.AddCash(currentQuest.questBringMoneyIfCompleted);
    }

    private void NoDamage()
    {
        if (AllObjectData.instance.isSafeLanded &&
           RocketMovement.instance.healthCounter == RocketMovement.instance.maxRocketHealth)
        {
            QuestComoleted();
        }

    }
    private void EarnMoney()
    {
        if (AllObjectData.instance.rocketLanded && currentQuest.currentQuestScriptable.mainValue < PlayerStats.instance.inMissionEarnedCash)
        {
            QuestComoleted();
        }
    }
    private void HoldTheLine()
    {
        print("Checking  ____");
        float angle;
        if (AllObjectData.instance.angleZ * AllObjectData.instance.angleZ > ConstsLibrary.maxAngleSquare)
        {
            angle = AllObjectData.instance.angleZ - ConstsLibrary.fullAngle;
        }
        else
        {
            angle = AllObjectData.instance.angleZ;
        }
        if (angle * angle > currentQuest.currentQuestScriptable.mainValue * currentQuest.currentQuestScriptable.mainValue)
        {
            angleCrossed = true;
        }
        if (!angleCrossed && AllObjectData.instance.isSafeLanded)
        {

            QuestComoleted();
        }
    }
    private void ReachAngle()
    {
        if (AllObjectData.instance.angleZ * AllObjectData.instance.angleZ > 50)
        {
            return;
        }
        if (AllObjectData.instance.angleZ * AllObjectData.instance.angleZ > currentQuest.currentQuestScriptable.mainValue * currentQuest.currentQuestScriptable.mainValue)
        {
            QuestComoleted();
        }
    }
    private void ReachX()
    {
        if (AllObjectData.instance.posX * AllObjectData.instance.posX > currentQuest.currentQuestScriptable.mainValue * currentQuest.currentQuestScriptable.mainValue)
        {
            QuestComoleted();
        }
    }
    private void ReachXReachY()
    {
        if ((AllObjectData.instance.posX * AllObjectData.instance.posX > currentQuest.currentQuestScriptable.mainValue * currentQuest.currentQuestScriptable.mainValue) &&
            (AllObjectData.instance.posY > currentQuest.currentQuestScriptable.secondaryValue))
        {
            QuestComoleted();
        }
    }
    private void ReachY()
    {

        if (AllObjectData.instance.posY > currentQuest.currentQuestScriptable.mainValue)
        {
            QuestComoleted();
        }
    }
    private void Servive()
    {
        if (AllObjectData.instance.isSafeLanded)
        {
            QuestComoleted();
        }
    }

    public CurrentSelectedQuest GenerateQuest()
    {
        //print("CurrentSelectedQuest =>>" + GameMaster.instance.hardness);
        List<QuestScriptable> questList = new List<QuestScriptable>();
        foreach (var item in questScriptableList)
        {
            if (item.hardness == GameMaster.instance.hardness)
            {
                questList.Add(item);
            }
        }
       // print("CurrentSelectedQuest =>>" + questList.Count);
        if (questList.Count == 0 || questList == null)
        {
            return currentQuest;
        }
        int questToSpawn = MainCount.instance.IntegerRandom(0, questList.Count);
        int questCost = MainCount.instance.IntegerRandom(questList[questToSpawn].costFrom, questList[questToSpawn].costTo) - questCostDecrease;
        int questBringMoney = MainCount.instance.
            IntegerRandom(questList[questToSpawn].completeMoneyBonusFrom, questList[questToSpawn].completeMoneyBonusTo) + questAdditionalMoney;
        currentQuest.result = true;
        currentQuest.questCost = questCost;
        currentQuest.questBringMoneyIfCompleted = questBringMoney;
        currentQuest.currentQuestScriptable = questList[questToSpawn];
        return currentQuest;
    }

    public void ResetQuest()
    {
        // questApplyedPressed = false;

        questCompletedSemaphore = false;
        AllObjectData.instance.isSafeLanded = false;
        angleCrossed = false;
    }

    public void QuestApplyed(CurrentSelectedQuest quest)
    {
        PlayerStats.instance.AddCash(-quest.questCost);
        currentQuest = quest;
        questApplyedPressed = true;
    }
}

public class CurrentSelectedQuest
{
    public QuestScriptable currentQuestScriptable;
    public int questCost;
    public int questBringMoneyIfCompleted;
    public bool result = false;
}


