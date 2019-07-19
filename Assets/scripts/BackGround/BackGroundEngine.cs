using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundEngine : MonoBehaviour {

    public static BackGroundEngine instance;

    private GameObject[] groundBlocks;

    public Transform[] layers = new Transform[3];
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    public float backGroundSize = 20;
    
    [Space]
    public float yPosition;
    public float largerThanScreen = 3;
    public float winningHeight = 300;
    public float yOffset = 0;
    // Use this for initialization
    void Start()
    {
        instance = instance ?? this;
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    public float timer;
    // Update is called once per frame
    void Update()
    {
        timer =- Time.deltaTime;
        if (timer < 0)
        {
            CountYPosition();
            timer = 1.5f;
        } 
        if (layers == null || layers.Length == 0)
        {
            return;
        }

        if (AllObjectData.instance.posX < (layers[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }
        if (AllObjectData.instance.posX > (layers[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }

    }
     

    private void ScrollLeft()
    {
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backGroundSize, yPosition, 0);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }
    //scrolling background
    private void ScrollRight()
    {
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backGroundSize, yPosition, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }

    private void CountYPosition()
    {

        yPosition = AllObjectData.instance.posY * (1 - largerThanScreen / winningHeight) + yOffset;
        foreach (var item in layers)
        {
            item.transform.position = new Vector3(item.transform.position.x, yPosition, item.transform.position.z);
        }
    }
}
