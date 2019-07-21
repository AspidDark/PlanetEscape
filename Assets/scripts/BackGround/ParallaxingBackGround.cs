using UnityEngine;

public class ParallaxingBackGround : MonoBehaviour
{

    public bool scrolling;
    public bool parallax;
    //scrolling background
    public float backGroundSize;

    public Transform playerTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    //parallax
    public float parallaxSpeedX;
    private float lastcameraX;
    [Space]
    public float parallaxSpeedY;
    private float lastcameraY;
    // Use this for initialization
    void Start()
    {

        //scrolling background
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;

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
            float deltaX = playerTransform.position.x - lastcameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpeedX);

            float deltaY = playerTransform.position.y - lastcameraY;
            transform.position += Vector3.up * (deltaY * parallaxSpeedY);
        }
        lastcameraX = playerTransform.position.x;

        lastcameraY = playerTransform.position.y;

        //scrolling background
        if (scrolling)
        {
            if (playerTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
                ScrollLeft();
            if (playerTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
                ScrollRight();
        }
    }
    //scrolling background
    private void ScrollLeft()
    {
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backGroundSize, layers[rightIndex].position.y, 0);
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
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backGroundSize, layers[rightIndex].position.y, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
