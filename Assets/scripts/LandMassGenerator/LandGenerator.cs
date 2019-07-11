using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGenerator : MonoBehaviour
{
    public static LandGenerator instance;
    //Conncted With
    public GroundScriptable[] groundScriptable;
    public ObjectPoolList objectPoolList;
   // public MainCount mainCount;

    public GroundBlockMover groundBlockMover;

    public float heightToSpawnGround = 30f;
    public float spawnBorder = 30;
    public float baseSpawnY = 0f;

    public int groundObjectMax = 5;

    private bool landGenerated = false;
    void Start()
    {
        instance = instance ?? this;
        SetDefaults();
    }
    public void SetDefaults()
    {
        landGenerated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if we have to spawn ground
        if (!landGenerated && (AllObjectData.instance.posY < heightToSpawnGround) && (Mathf.Abs(AllObjectData.instance.posX) >= spawnBorder))
        {
            GenerateGround();
        }
    }
    public void ReCraetObjectCount()
    {
        landGenerated = false;
    }
    private void GenerateGround()
    {
        NameAndNumberDto groundNameAndSize = ChoseGround();
        if (groundNameAndSize == null)
        {
            return;
        }
        float startingPosition;
        int modifier;
        if (AllObjectData.instance.posX > 0)
        {
            modifier = 1;
        }
        else
        {
            modifier = -1;
        }

        if (Mathf.Abs(AllObjectData.instance.posX) <= spawnBorder + groundNameAndSize.size * groundObjectMax / 2)
        {
            startingPosition = modifier * (spawnBorder + 10);
        }
        else
        {
            startingPosition = Mathf.Round(AllObjectData.instance.posX - AllObjectData.instance.posX % groundNameAndSize.size) - 2 * groundNameAndSize.size * modifier;
        }

        if (modifier > 0)
        {
            for (int i = 1; i <= 5; i++)
            {
                Vector3 groundPosition = new Vector3(startingPosition + i * modifier * groundNameAndSize.size, baseSpawnY, 0);
                objectPoolList.GetPooledObject(groundNameAndSize.Name, groundPosition, Quaternion.identity, false);
            }
        }
        else
        {
            for (int i = 5; i >= 1; i--)
            {
                Vector3 groundPosition = new Vector3(startingPosition + i * modifier * groundNameAndSize.size, baseSpawnY, 0);
                objectPoolList.GetPooledObject(groundNameAndSize.Name, groundPosition, Quaternion.identity, false);
            }
        }
        landGenerated = true;
        groundBlockMover.enabled = true;
    }
    // if (AllObjectData.instance.posY >= item.fromHeight && AllObjectData.instance.posY <= item.toHeight)
    /// <summary>
    /// Choosing Random Prefab to Spawn
    /// </summary>
    /// <returns></returns>
    private NameAndNumberDto ChoseGround()
    {
        List<GroundScriptable> listOfGroundToSpawn = new List<GroundScriptable>();
        foreach (var item in groundScriptable)
        {
            if (Mathf.Abs(AllObjectData.instance.posX) >= item.spawnXRadiusMin && Mathf.Abs(AllObjectData.instance.posX) <= item.spawnXRadiusMax)
            {
                listOfGroundToSpawn.Add(item);
            }
        }
        //If no object found
        if (listOfGroundToSpawn == null || listOfGroundToSpawn.Count == 0)
        {
            return new NameAndNumberDto
            {
                Name= groundScriptable[0].groundName,
                size= groundScriptable[0].groundSize
            };
        }
        int numberOfObjectToSpawn = MainCount.instance.IntegerRandom(0, listOfGroundToSpawn.Count);
        NameAndNumberDto nameAndNumberDto = new NameAndNumberDto()
        {
            Name = listOfGroundToSpawn[numberOfObjectToSpawn].groundName,
            size = listOfGroundToSpawn[numberOfObjectToSpawn].groundSize
        };
        return nameAndNumberDto;
    }
}

//No tuples in this vsersion...sad
public class NameAndNumberDto
{
    public string Name;
    public float size;
}

