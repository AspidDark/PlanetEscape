using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AirObjectByType
{
    public string poolName;
    public AirObjectScriptable[] airObjectList;
    [HideInInspector]
    public float heightCheckTimer;
    public float timeBetweenHeightChecks = 1.5f;
    [Range(0, 10)]
    public int hardness;
}

public class AirObjectGenerator : MonoBehaviour {

    public static AirObjectGenerator instance;
    //Conncted With
    public ObjectPoolList objectPoolList;
    public NodeInformer nodeInformer;

    public float objectSpawnOffset = 2;

    #region HeightCheck

    public AirObjectByType[] airObjectByTypeArray;

    public float higherFromPlayerToSpawnMax = 100;
    public float higherFromPlayerToSpawnMin = 10;
    #endregion

   
    // Use this for initialization

    void Start ()
    {
        instance = instance ?? this;
        if (airObjectByTypeArray == null || airObjectByTypeArray.Length == 0)
        {
            return;
        }
        HeightCheckTimerUpdate();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (airObjectByTypeArray == null || airObjectByTypeArray.Length == 0)
        {
            return;
        }
        foreach (var item in airObjectByTypeArray)
        {
            if (item.heightCheckTimer <= 0)
            {
                item.heightCheckTimer = item.timeBetweenHeightChecks;
                SpawnFlyingObject(item.poolName);

            }
            item.heightCheckTimer -= MainCount.instance.deltaTime;
        }

    }
    public void HeightCheckTimerUpdate()
    {
        foreach (var item in airObjectByTypeArray)
        {
            item.heightCheckTimer = item.timeBetweenHeightChecks;
        }
    }

    private void SpawnFlyingObject(string poolName)
    {
        //print(poolName);
        //Geting List of Objects whitch can be spawned on player's Height
        List<AirObjectScriptable> listOfObjectsOnThisHeight = new List<AirObjectScriptable>();
        var listOfAirObjects = airObjectByTypeArray.FirstOrDefault(n => n.poolName == poolName);
        foreach (var item in listOfAirObjects.airObjectList)
        {
            if (AllObjectData.instance.posY <= item.maxHeight 
                && AllObjectData.instance.posY >= item.minHeight)
            {
                listOfObjectsOnThisHeight.Add(item);
            }
        }
        //If no object found
        if (listOfObjectsOnThisHeight == null || listOfObjectsOnThisHeight.Count == 0)
        {
            return;
        }
        //Object flies from left to right?
        float objectXposition;
        if (MainCount.instance.BoolRandom())
        {
            objectXposition = -nodeInformer.cameraXWidth- objectSpawnOffset;
        }
        else
        {
            objectXposition = nodeInformer.cameraXWidth+ objectSpawnOffset;
        }
        int[] airObjectWeights = new int[listOfObjectsOnThisHeight.Count];
        for (int i = 0; i < listOfObjectsOnThisHeight.Count; i++)
        {
            airObjectWeights[i] = listOfObjectsOnThisHeight[i].spawnChance;
        }
        int numberOfObjectToSpawn= MainCount.instance.DifferentWeightRandom(airObjectWeights);//mainCount.IntegerRandom(0, listOfObjectsOnThisHeight.Count);

        float startingYposToSpawn = MainCount.instance.FloatRandom(higherFromPlayerToSpawnMin, higherFromPlayerToSpawnMax);

        if ((AllObjectData.instance.gameobjectVelocity.x * AllObjectData.instance.gameobjectVelocity.x > ConstsLibrary.speedSquareAferSpawnObjectOnMinHeight))
        {
                startingYposToSpawn = MainCount.instance.FloatRandom(-ConstsLibrary.heightToSpawnWhenXisHigh, ConstsLibrary.heightToSpawnWhenXisHigh);
       
        }
        if (AllObjectData.instance.gameobjectVelocity.y < 0&& AllObjectData.instance.posY> higherFromPlayerToSpawnMax)
        {
            startingYposToSpawn *= -1;
        }
        string name = listOfObjectsOnThisHeight[numberOfObjectToSpawn].objectName;
        float xPosition;
        if (AllObjectData.instance.gameobjectVelocity.x >= 0)
        {
            if (objectXposition < 0)
            {
                xPosition = AllObjectData.instance.posX + objectXposition;
            }
            else
            {
                xPosition = AllObjectData.instance.posX + objectXposition + AllObjectData.instance.gameobjectVelocity.x;
            }
        }
        else
        {
            if (objectXposition > 0)
            {
                xPosition = AllObjectData.instance.posX + objectXposition;
            }
            else
            {
                xPosition = AllObjectData.instance.posX + objectXposition + AllObjectData.instance.gameobjectVelocity.x;
            }
        }


        Vector3 position = new Vector3(xPosition//AllObjectData.instance.posX + objectXposition + AllObjectData.instance.gameobjectVelocity.x
            , AllObjectData.instance.posY+ startingYposToSpawn+ AllObjectData.instance.gameobjectVelocity.y);
        objectPoolList.GetPooledObject(name, position, Quaternion.identity, true); ///SpawningObject
    }
}
