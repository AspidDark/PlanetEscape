using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TechNodeStat", menuName = "MenuItems/TechNodeStat")]
public class TechNodeStats : ScriptableObject
{
    /// <summary>
    /// Node Name (used in code)
    /// </summary>
    public string nodeName;
    /// <summary>
    /// Node name showed in game
    /// </summary>
    public string nodeDisplayName;
    /// <summary>
    /// Text Description of Node
    /// </summary>
    public string nodeDescription;
    /// <summary>
    /// Node Cost
    /// </summary>
    public float nodeCost;
    /// <summary>
    /// Nodes this node "depends on"
    /// </summary>
    public string[] nodeDependsOn;
    /// <summary>
    /// Icon when node not done
    /// </summary>
    public Sprite iconNodeNotUpgraded;
    /// <summary>
    /// Icon when node done
    /// </summary>
    public Sprite iconNodeUpgrded;
    /// <summary>
    /// Stats to Change
    /// </summary>
    public NodeStatsDto nodeStatsDto;
    //Is Node Econimic?
    public bool economicNode;

}
[System.Serializable]
public struct NodeStatsDto
{
    public float weightNodeChange;
    public float fuelNodeChange;
    public float engineNodeChane;
    public float rotationNodeValueChange;
    public float heatNodeChange;
    /// <summary>
    /// Line of node
    /// </summary>
    public NodeLine nodeLine;
    public bool isFinalNode;
}

[System.Serializable]
public enum NodeLine
{
FirstLine,
SecondLine,
ThirdLine,
FourthLine,
FifthLine
}
