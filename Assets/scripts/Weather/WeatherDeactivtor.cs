using UnityEngine;

public class WeatherDeactivtor : MonoBehaviour
{
    public GameObject[] weatherBorderBlocks;
    public float border;

    public float viewZone = 5;
    private int leftIndex;
    private int rightIndex;
    public float backGroundSize = 20;
    public float startPosY;

    void Start()
    {
        // weatherBorderBlocks = gameObject.GetComponentsInChildren<GameObject>();
        startPosY = gameObject.transform.position.y;
    }
    void Update()
    {
        foreach (var item in weatherBorderBlocks)
        {
            if (gameObject.transform.position.y + border > AllObjectData.instance.posY && gameObject.transform.position.y - border < AllObjectData.instance.posY)
            {
                item.SetActive(true);
                if (AllObjectData.instance.posX < (weatherBorderBlocks[leftIndex].transform.position.x + viewZone))
                {
                    ScrollLeft();
                }
                if (AllObjectData.instance.posX > (weatherBorderBlocks[rightIndex].transform.position.x - viewZone))
                {
                    ScrollRight();
                }

            }
            else
            {
                item.SetActive(false);
            }
        }
    }

    private void ScrollLeft()
    {
        // print("ScrollLeft");
       // int lastRight = rightIndex;
        weatherBorderBlocks[rightIndex].transform.position = new Vector3(weatherBorderBlocks[leftIndex].transform.position.x - backGroundSize, startPosY, 0);
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
        //  print(" ScrollRight");
       // int lastLeft = leftIndex;
        weatherBorderBlocks[leftIndex].transform.position = new Vector3(weatherBorderBlocks[rightIndex].transform.position.x + backGroundSize, startPosY, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == weatherBorderBlocks.Length)
        {
            leftIndex = 0;
        }
    }
}
