using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherEffectScroller : MonoBehaviour {

    public float viewZone = 5;
    public GameObject[] weatherBorderBlocks;
    private int leftIndex;
    private int rightIndex;
    public float backGroundSize = 20;

    public float startPosY;
    public float onEnablePosY;
    // Use this for initialization
    void Start () {
        // weatherBorderBlocks = gameObject.GetComponentsInChildren<GameObject>();
        startPosY = gameObject.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (AllObjectData.instance.posX < (weatherBorderBlocks[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }
        if (AllObjectData.instance.posX > (weatherBorderBlocks[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
    }

    private void ScrollLeft()
    {
       // int lastRight = rightIndex;
        weatherBorderBlocks[rightIndex].transform.position = new Vector3(weatherBorderBlocks[leftIndex].transform.position.x - backGroundSize, startPosY,0);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = weatherBorderBlocks.Length - 1;
        }
    }
    //scrolling background
    private void ScrollRight()
    {
        //int lastLeft = leftIndex;
        weatherBorderBlocks[leftIndex].transform.position = new Vector3(weatherBorderBlocks[rightIndex].transform.position.x + backGroundSize, startPosY, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == weatherBorderBlocks.Length)
        {
            leftIndex = 0;
        }
    }

    private void OnEnable()
    {
        onEnablePosY = gameObject.transform.position.y;
    }
}
