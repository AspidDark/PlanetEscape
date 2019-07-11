using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AirObject", menuName = "MenuItems/AirObject")]
public  class AirObjectScriptable : ScriptableObject {

    public string objectName;
    public float minHeight;
    public float maxHeight;
    public float maxSpeed;
    public float minSpeed;
    public float maxYSpeed;
    public float minYSpeed;
    public int spawnChance;
    public int type;
}
