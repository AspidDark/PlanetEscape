using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New BackGround", menuName = "MenuItems/BackGround")]
public class BackGroundScriptable : ScriptableObject
{
    public bool isScrolling;
    public bool isParralax;
    public float parallaxSpeedX;
    public float parallaxSpeedY;
    public float spriteXSize;
    public int sortingLayerId;
    [Space]
    public Sprite[] sprites= new Sprite[3];

}
