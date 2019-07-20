using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxingBackGround : MonoBehaviour {

    public bool scrolling, parallax;

    //scrolling background
    public BackGroundScriptable[] backGrounds;

    public Transform playerTransform;
    private float viewZone = 10;

    //parallax
    private float lastcameraX;
    [Space]
    private float lastcameraY;
    // Use this for initialization
    void Start()
    {

        //scrolling background
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        foreach (var item in backGrounds)
        {
            item.InitiateObject();
        }      

        //parallax
        lastcameraX = playerTransform.position.x;

        lastcameraY = playerTransform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //parallax
        if (parallax)
        {
            foreach (var item in backGrounds)
            {
            float deltaX = playerTransform.position.x - lastcameraX;
            transform.position += Vector3.right * (deltaX * item.parallaxSpeedX);

            float deltaY = playerTransform.position.y - lastcameraY;
            transform.position += Vector3.up * (deltaY * item.parallaxSpeedY);
            }
        }

        lastcameraX = playerTransform.position.x;

        lastcameraY = playerTransform.position.y;

        //scrolling background
        if (scrolling)
        {
            for (int i = 0; i < backGrounds.Length; i++)
            {
            if (playerTransform.position.x < (backGrounds[i].layers[backGrounds[i].leftIndex].transform.position.x + viewZone))
                ScrollLeft(i);
            if (playerTransform.position.x > (backGrounds[i].layers[backGrounds[i].rightIndex].transform.position.x - viewZone))
                ScrollRight(i);
            }
        }
    }
    //scrolling background
    private void ScrollLeft(int numberToScroll)
    {
        backGrounds[numberToScroll].layers[backGrounds[numberToScroll].rightIndex].position 
            = new Vector3(backGrounds[numberToScroll].layers[backGrounds[numberToScroll].leftIndex].position.x 
            - backGrounds[numberToScroll].backGroundSize, backGrounds[numberToScroll].layers[backGrounds[numberToScroll].rightIndex].position.y, 0);
        backGrounds[numberToScroll].leftIndex = backGrounds[numberToScroll].rightIndex;
        backGrounds[numberToScroll].rightIndex--;
        if (backGrounds[numberToScroll].rightIndex < 0)
        {
            backGrounds[numberToScroll].rightIndex = backGrounds[numberToScroll].layers.Length - 1;
        }
    }
    //scrolling background
    private void ScrollRight(int numberToScroll)
    {
        backGrounds[numberToScroll].layers[backGrounds[numberToScroll].leftIndex].position 
            = new Vector3(backGrounds[numberToScroll].layers[backGrounds[numberToScroll].rightIndex].position.x 
            + backGrounds[numberToScroll].backGroundSize, backGrounds[numberToScroll].layers[backGrounds[numberToScroll].rightIndex].position.y, 0);
        backGrounds[numberToScroll].rightIndex = backGrounds[numberToScroll].leftIndex;
        backGrounds[numberToScroll].leftIndex++;
        if (backGrounds[numberToScroll].leftIndex == backGrounds[numberToScroll].layers.Length)
        {
            backGrounds[numberToScroll].leftIndex = 0;
        }
    }
}
