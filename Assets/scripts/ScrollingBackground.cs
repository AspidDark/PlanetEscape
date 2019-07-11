using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public bool scrolling, parallax;

    //scrolling background
    public float backGroundSize;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    //parallax
    public float parallaxSpeed;
    private float lastcameraX;

    // Use this for initialization
    void Start()
    {

        //scrolling background
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;

        //parallax
        lastcameraX = cameraTransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {


        //parallax
        if (parallax)
        {
            float deltaX = cameraTransform.position.x - lastcameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpeed);
        }
        lastcameraX = cameraTransform.position.x;

        //scrolling background
        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
                ScrollLeft();
            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
                ScrollRight();
        }
    }
    //scrolling background
    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backGroundSize);
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
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backGroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
