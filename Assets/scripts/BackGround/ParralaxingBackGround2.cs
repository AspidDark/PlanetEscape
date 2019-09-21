using UnityEngine;

public class ParralaxingBackGround2 : MonoBehaviour
{

    public BackGroundScriptable[] backGroundScriptable;
    private LayerAndChIldObjects[] layerAndChIldObjects;

    public Transform playerTransform;

    private float viewZoneX = 10;
    private float viewZoneY = 10;


    private float lastcameraX;

    private float lastcameraY;

    public int backGroundToSpawn;

    private int lowest;
    private int hiehest;
    // Use this for initialization
    void Start()
    {
        //scrolling background
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        layerAndChIldObjects = new LayerAndChIldObjects[backGroundScriptable[backGroundToSpawn].sprites.Length];
        for (int j = 0; j < backGroundScriptable[backGroundToSpawn].sprites.Length; j++)
        {
            layerAndChIldObjects[j] = new LayerAndChIldObjects();

            layerAndChIldObjects[j].layers = new Transform[backGroundScriptable[backGroundToSpawn].sprites[j].spriteLevels.Length];
            layerAndChIldObjects[j].childObjects = new GameObject[backGroundScriptable[backGroundToSpawn].sprites[j].spriteLevels.Length];
            for (int i = 0; i < layerAndChIldObjects[j].layers.Length; i++)
            {
                layerAndChIldObjects[j].childObjects[i] = new GameObject("BackGround_" + j+"_"+i);
                layerAndChIldObjects[j].childObjects[i].transform.parent = gameObject.transform;
                layerAndChIldObjects[j].childObjects[i].transform.position =
                    new Vector3(backGroundScriptable[backGroundToSpawn].spriteXSize * i, backGroundScriptable[backGroundToSpawn].spriteYSize*(j-1), 0);
                var spriteRenderer = layerAndChIldObjects[j].childObjects[i].AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = backGroundScriptable[backGroundToSpawn].sprites[j].spriteLevels[i];
                spriteRenderer.sortingLayerName = backGroundScriptable[backGroundToSpawn].sortingLayerName;
                // spriteRenderer.sortingLayerID = backGroundScriptable[backGroundToSpawn].sortingLayerId;
                layerAndChIldObjects[j].layers[i] = layerAndChIldObjects[j].childObjects[i].transform;
            }

            layerAndChIldObjects[j].leftIndexX = 0;
            layerAndChIldObjects[j].rightIndexX = layerAndChIldObjects[j].layers.Length - 1;
         }

        lowest = 0;
        hiehest= layerAndChIldObjects.Length - 1;
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
        if (backGroundScriptable[backGroundToSpawn].isScrollingX)
        {
            if (playerTransform.position.x < (layerAndChIldObjects[0].layers[layerAndChIldObjects[0].leftIndexX].transform.position.x + viewZoneX))
                ScrollLeft();
            if (playerTransform.position.x > (layerAndChIldObjects[0].layers[layerAndChIldObjects[0].rightIndexX].transform.position.x - viewZoneX))
                ScrollRight();
        }

        if (backGroundScriptable[backGroundToSpawn].isScrollingY)
        {
            // var maxValues
            if (playerTransform.position.y < (layerAndChIldObjects[lowest].layers[0].position.y + viewZoneY))
                ScrollDown();
            if (playerTransform.position.y > (layerAndChIldObjects[hiehest].layers[0].position.y - viewZoneY))
                ScrollUp();
        }
    }
    //scrolling background
    private void ScrollLeft()
    {
        for (int i = 0; i < layerAndChIldObjects.Length; i++)
        {
            layerAndChIldObjects[i].layers[layerAndChIldObjects[i].rightIndexX].position
                = new Vector3(layerAndChIldObjects[i].layers[layerAndChIldObjects[i].leftIndexX].position.x - backGroundScriptable[backGroundToSpawn].spriteXSize, 
                layerAndChIldObjects[i].layers[layerAndChIldObjects[i].rightIndexX].position.y, 0);
            layerAndChIldObjects[i].leftIndexX = layerAndChIldObjects[i].rightIndexX;
            layerAndChIldObjects[i].rightIndexX--;
            if (layerAndChIldObjects[i].rightIndexX < 0)
            {
                layerAndChIldObjects[i].rightIndexX = layerAndChIldObjects[i].layers.Length - 1;
            }
        }
    }
    private void ScrollDown()
    {
        for (int i = 0; i < layerAndChIldObjects[hiehest].layers.Length; i++)
        {
            layerAndChIldObjects[hiehest].layers[i].position
                = new Vector3(layerAndChIldObjects[lowest].layers[i].position.x, 
                layerAndChIldObjects[lowest].layers[i].position.y- backGroundScriptable[backGroundToSpawn].spriteYSize, 0);
        }
            lowest = hiehest;
            hiehest--;
            if (hiehest < 0)
            {
                hiehest = layerAndChIldObjects[0].layers.Length - 1;
            }
    }
    //scrolling background
    private void ScrollRight()
    {
        for (int i = 0; i < layerAndChIldObjects.Length; i++)
        {
            layerAndChIldObjects[i].layers[layerAndChIldObjects[i].leftIndexX].position
                = new Vector3(layerAndChIldObjects[i].layers[layerAndChIldObjects[i].rightIndexX].position.x + backGroundScriptable[backGroundToSpawn].spriteXSize, layerAndChIldObjects[i].layers[layerAndChIldObjects[i].rightIndexX].position.y, 0);
            layerAndChIldObjects[i].rightIndexX = layerAndChIldObjects[i].leftIndexX;
            layerAndChIldObjects[i].leftIndexX++;
            if (layerAndChIldObjects[i].leftIndexX == layerAndChIldObjects[i].layers.Length)
            {
                layerAndChIldObjects[i].leftIndexX = 0;
            }
        }
    }

    private void ScrollUp()
    {
        for (int i = 0; i < layerAndChIldObjects[lowest].layers.Length; i++)
        {
            layerAndChIldObjects[lowest].layers[i].position
                = new Vector3(layerAndChIldObjects[hiehest].layers[i].position.x, 
                layerAndChIldObjects[hiehest].layers[i].position.y + backGroundScriptable[backGroundToSpawn].spriteYSize, 0);
        }
            hiehest = lowest;
            lowest++;
            if (lowest == layerAndChIldObjects[0].layers.Length)
            {
                lowest=0;
            }
    }
}




public class LayerAndChIldObjects
{
    public GameObject[] childObjects = new GameObject[3];
    public Transform[] layers = new Transform[3];
    //public LayerAndChIldObjects()
    //{
    //    childObjects = new GameObject[3];
    //    layers = new Transform[3];
    //}

    public int leftIndexX;
    public int rightIndexX;
}

