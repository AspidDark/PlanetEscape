using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechTreeEngine : MonoBehaviour
{
    public static TechTreeEngine instance;
    public TechNodeControl [] techNodeControlList;
#region Consts
    public const string notEnoughMoney = "Not Enough Money";
    public const string alreadyResearched = "Already Researched";
    public const string researchDone = "Research Done";
    public const string someOfRequiredTechsNotResearched = "Some Of Required Techs Not Researched";
    public const int nodeDonePP = 2;
    public const int nodeOpenPP = 1;
    public const int nodeClosedPP = 0;

    #endregion
    public enum NodeCondition { NodeClosed, NodeOpen, NodeDone }
   
    // Use this for initialization
    void Start () {
        instance = this;
        if (techNodeControlList == null || techNodeControlList.Length <= 0)
        {

          //  Debug.Log("No techs attached");
        }
        StartingNodeInitiation();
	}
    public string TechTreeResearch(string nodeName)
    {
        string response = string.Empty;
        int indexOfNode = FindNodeIndex(nodeName);
       //No such a tech..How could it be?
        if (indexOfNode < 0)
        {
            return response;
            //Debug
        }
        //Node already Researched?
        if (techNodeControlList[indexOfNode].nodeCondition== nodeDonePP)
        {
            return alreadyResearched;
        }

        //Not enough Money
        if(techNodeControlList[indexOfNode].techNodeStats.nodeCost> PlayerStats.instance.playerCash)
        {
            MenuButtonControl.instance.OnNotEnoughMoney();
            return notEnoughMoney;
        }
        //All "depended from" teches Researched?
        if (!AllDependedFromNodesOpen(techNodeControlList[indexOfNode].techNodeStats.nodeDependsOn))
        {
            return someOfRequiredTechsNotResearched;
        }
        //Research Done
        PlayerStats.instance.AddCash(-techNodeControlList[indexOfNode].techNodeStats.nodeCost);
        techNodeControlList[indexOfNode].nodeCondition= nodeDonePP;
        techNodeControlList[indexOfNode].NodeResearched();//---
        SaveNodeState(nodeName, nodeDonePP);
        OpenDependedNodes(nodeName);
        PlayerStats.instance.SaveCurrentAll(); //Economic
        return researchDone;
    }
    #region Private
    private void StartingNodeInitiation()
    {
        foreach (var nodeControl in techNodeControlList)
        {
            int nodeState = HelpSaveLoad.GetValue(nodeControl.techNodeStats.nodeName, 0);
            if ((nodeState != nodeDonePP) && (nodeControl.techNodeStats.nodeDependsOn == null || nodeControl.techNodeStats.nodeDependsOn.Length <=0))
            {
                nodeState = nodeOpenPP;
            }
            nodeControl.nodeCondition = nodeState;
            switch (nodeState)
            {
                case 0: //Not open
                    nodeControl.NodeIncative();
                    break;
                case 1: //Open
                    nodeControl.NodeActive();
                    break;
                default: //Researched
                    nodeControl.NodeResearched();//---
                    break;
            }
        }
    }

    private void OpenDependedNodes(string nodeName)
    {
        for (int i = 0; i < techNodeControlList.Length; i++)
        {
            if (AllDependedFromNodesOpen(techNodeControlList[i].techNodeStats.nodeDependsOn))
            {
                if (techNodeControlList[i].nodeCondition < nodeDonePP)
                {
                    techNodeControlList[i].NodeActive();
                    SaveNodeState(techNodeControlList[i].techNodeStats.nodeName, nodeOpenPP);
                    techNodeControlList[i].nodeCondition = nodeOpenPP;
                }
            }
        }
    }


    private int FindNodeIndex(string nodeName)
    {
        int index = -1;
        for (int i = 0; i < techNodeControlList.Length; i++)
        {
            if (techNodeControlList[i].techNodeStats.name == nodeName)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    //All "depended From" nodes Open?
    private bool AllDependedFromNodesOpen(string[] nodes)
    {
        foreach (var item in nodes)
        {
            int index = FindNodeIndex(item);
            if (index < 0)
            {
               // Debug.Log("No tech named:"+ item+"In dependencies");
            }
            if (techNodeControlList[index].nodeCondition!= nodeDonePP)
            {
                return false;
            }

        }
        return true;
    }
   

    private void SaveNodeState(string nodeName, int value)
    {
        HelpSaveLoad.SetValue(nodeName, value);
    }

    #endregion
   
}

