using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlockMover : MonoBehaviour
{

    public static GroundBlockMover instance;

    public bool scrolling = true;

    public AllObjectData allObjectData;
    public float spawnBorder = 30;
    public float heightToSpawnGround = 10f;

    private GameObject[] groundBlocks;

    private Transform[] layers = new Transform[5];
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float backGroundSize = 20;
    public float yPosition;
    // Use this for initialization
    void Start()
    {
        instance = instance ?? this;
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }


    // Update is called once per frame
    void Update()
    {
        if ((allObjectData.posY > heightToSpawnGround) || (Mathf.Abs(allObjectData.posX) < spawnBorder))
        {
            this.enabled = false;
            return;

        }
        if (layers == null || layers.Length == 0)
        {
            return;
        }

        if (AllObjectData.instance.posX < (layers[leftIndex].transform.position.x + viewZone))
        {
            //  print("IF cameraTransform.position.x->" + cameraTransform.position.x + "layers[leftIndex].transform.position.x-> " + layers[leftIndex].transform.position.x);
            ScrollLeft();
        }
        if (AllObjectData.instance.posX > (layers[rightIndex].transform.position.x - viewZone))
        {
            //  print("ELSE  cameraTransform.position.x->" + cameraTransform.position.x + "layers[leftIndex].transform.position.x-> " + layers[leftIndex].transform.position.x);
            ScrollRight();
        }

    }

    private void OnEnable()
    {
        groundBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");
        for (int i = 0; i < groundBlocks.Length; i++)
        {
            layers[i] = groundBlocks[i].transform;
        }
    }

    private void OnDisable()
    {

        foreach (var item in groundBlocks)
        {
            if (item != null)
                item.SetActive(false);
        }
        LandGenerator.instance.ReCraetObjectCount();
    }

    private void ScrollLeft()
    {
        // print("ScrollLeft");
       // int lastRight = rightIndex;
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
        //  print(" ScrollRight");
       // int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backGroundSize, yPosition, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
