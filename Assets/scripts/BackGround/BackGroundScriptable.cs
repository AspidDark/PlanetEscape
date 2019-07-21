using UnityEngine;

[CreateAssetMenu(fileName = "New BackGround", menuName = "MenuItems/BackGround")]
public class BackGroundScriptable : ScriptableObject
{
    public string backGroundToFindTag;
    public float backGroundSize;
    // public GameObject go;
    [Space]
    public float parallaxSpeedX;
    public float parallaxSpeedY;

    [HideInInspector]
    public int leftIndex;
    [HideInInspector]
    public int rightIndex;
    [HideInInspector]
    public Transform[] layers;
    [HideInInspector]
    public GameObject go;
    public void InitiateObject()
    {
        go = GameObject.FindGameObjectWithTag(backGroundToFindTag);
        if (go == null)
        {
            return;
        }
        layers = new Transform[go.transform.childCount];
        for (int i = 0; i < go.transform.childCount; i++)
        {
            layers[i] = go.transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }
}
