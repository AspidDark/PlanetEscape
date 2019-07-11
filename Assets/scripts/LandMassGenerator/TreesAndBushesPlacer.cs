using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlacedObjects
{
    public string tag;
    public float zAxisOffset;
    public float xAxisMiddleDistace;
    public float xObjectPlacementOffset;
    public float yAxisOffset;
    public List<GameObject> placedObjects;
}

public class TreesAndBushesPlacer : MonoBehaviour
{

    public float sceneWidth;
    public bool startGenerationFromZero;
    public List<PlacedObjects> placeToScene;


    void Awake()
    {
        float startingPosition = startGenerationFromZero ? 0 : -sceneWidth / 2;


        foreach (var objectListToPlace in placeToScene)
        {
            int numberOfObjects = (int)(sceneWidth / objectListToPlace.xAxisMiddleDistace);
            float xPos = startingPosition;
            for (int i = 0; i < numberOfObjects; i++)
            {
                xPos += Random.Range(-objectListToPlace.xObjectPlacementOffset, objectListToPlace.xObjectPlacementOffset) + objectListToPlace.xAxisMiddleDistace;
                float yPos = Random.Range(-objectListToPlace.yAxisOffset, objectListToPlace.yAxisOffset);
                int numberOfObjectToSpawn = Random.Range(0, objectListToPlace.placedObjects.Count);

                GameObject obj = Instantiate(objectListToPlace.placedObjects[numberOfObjectToSpawn]);
                obj.transform.position = new Vector3(xPos, yPos, objectListToPlace.yAxisOffset);
            }

        }
    }
}
