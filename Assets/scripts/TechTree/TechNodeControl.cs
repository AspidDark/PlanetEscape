using System;
using UnityEngine;
using UnityEngine.UI;

public class TechNodeControl : MonoBehaviour
{

    /// <summary>
    /// Current node condition
    /// </summary>
    public int nodeCondition;

    public TechNodeStats techNodeStats;
    public Button button;

    private void Start()
    {

    }

    //Sending Node description
    public void NodeButtonClick()
    {
        NodeInformer.instance.ShowTechDescription(techNodeStats.nodeDisplayName, techNodeStats.nodeDescription, techNodeStats.nodeName, this);
    }

    public void ResearchButtonClicked(string nodeName)
    {
        string response = TechTreeEngine.instance.TechTreeResearch(nodeName);
        NodeInformer.instance.UpdateInformer(response, techNodeStats.nodeDisplayName, techNodeStats.nodeDescription);
    }


    public void NodeActive()
    {
        button.interactable = true;
        button.image.sprite = techNodeStats.iconNodeNotUpgraded;
    }

    public void NodeResearched()
    {
        button.interactable = false;
        button.image.sprite = techNodeStats.iconNodeUpgrded;
        //Узел экономический?
        if (!techNodeStats.economicNode)
        {
            //Нет тогда добавляем тех параметры
            RocketStats.instance.UpgradeStatsUpdate(techNodeStats.nodeStatsDto);
        }
        else
        {
            //Да Добавляем экономические параметры
            EconomicStats.instance.EconomicResearchDone(techNodeStats.nodeName);
        }
   
    }

    public void NodeIncative()
    {
        button.interactable = false;
        button.image.sprite = techNodeStats.iconNodeNotUpgraded;
    }
}
