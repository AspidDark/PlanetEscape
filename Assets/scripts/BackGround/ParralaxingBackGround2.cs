using UnityEngine;

public class ParralaxingBackGround2 : MonoBehaviour {

    public BackGroundScriptable[] backGroundScriptable;
    private GameObject[] childObjects;
    private Transform[] layers;
    public Transform playerTransform;

    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    private float lastcameraX;

    private float lastcameraY;

    public int backGroundToSpawn;
    // Use this for initialization
    void Start()
    {
        //scrolling background
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        layers= new Transform[backGroundScriptable[backGroundToSpawn].sprites.Length];
       childObjects = new GameObject[backGroundScriptable[backGroundToSpawn].sprites.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            childObjects[i] = new GameObject("BackGround"+i);
            childObjects[i].transform.parent = gameObject.transform;
            childObjects[i].transform.position = new Vector3(backGroundScriptable[backGroundToSpawn].spriteXSize*i, 0, 0);
            var spriteRenderer =  childObjects[i].AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = backGroundScriptable[backGroundToSpawn].sprites[i];
            spriteRenderer.sortingLayerName= backGroundScriptable[backGroundToSpawn].sortingLayerName;
            // spriteRenderer.sortingLayerID = backGroundScriptable[backGroundToSpawn].sortingLayerId;
            layers[i] = childObjects[i].transform;
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;

        //parallax
        lastcameraX = playerTransform.position.x;

        lastcameraY = playerTransform.position.y;
    }

    void FixedUpdate()
    {
        //parallax
        if (backGroundScriptable[backGroundToSpawn].isParralax)
        {
            float deltaX = playerTransform.position.x - lastcameraX;
            transform.position += Vector3.right * (deltaX * backGroundScriptable[backGroundToSpawn].parallaxSpeedX);

            float deltaY = playerTransform.position.y - lastcameraY;
            transform.position += Vector3.up * (deltaY * backGroundScriptable[backGroundToSpawn].parallaxSpeedY);
        }
        lastcameraX = playerTransform.position.x;

        lastcameraY = playerTransform.position.y;

        //scrolling background
        if (backGroundScriptable[backGroundToSpawn].isScrolling)
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
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backGroundScriptable[backGroundToSpawn].spriteXSize, layers[rightIndex].position.y, 0);
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
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backGroundScriptable[backGroundToSpawn].spriteXSize, layers[rightIndex].position.y, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
