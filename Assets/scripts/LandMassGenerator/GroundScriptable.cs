using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GroundBlock", menuName = "MenuItems/GroundBlock")]
public class GroundScriptable : ScriptableObject
{
    public string groundName;
    public float groundSize;
    public float spawnXRadiusMax;
    public float spawnXRadiusMin;
}
