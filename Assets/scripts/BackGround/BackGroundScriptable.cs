using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New BackGround", menuName = "MenuItems/BackGround")]
public class BackGroundScriptable : ScriptableObject
{
    public bool isScrollingX;
    public float spriteXSize;
    public bool isScrollingY;
    public float spriteYSize;
    public bool isParralax;
    public float parallaxSpeedX;
    public float parallaxSpeedY;
    public string sortingLayerName;
    //[Space]
    //public Sprite[] spritesL1= new Sprite[3];
    //[Space]
    //public Sprite[] spritesL2 = new Sprite[3];
    //[Space]
    //public Sprite[] spritesL3 = new Sprite[3];
    [Space]
    [SerializeField]
    public SpritesLevel[] sprites;

}

[System.Serializable]
public class SpritesLevel
{
    public Sprite[] spriteLevels = new Sprite[3];
}