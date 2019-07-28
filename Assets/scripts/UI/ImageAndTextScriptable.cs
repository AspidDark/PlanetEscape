using UnityEngine;

[CreateAssetMenu(fileName = "New Screen", menuName = "MenuItems/Screen")]
public class ImageAndTextScriptable : ScriptableObject {
    public string screenName;
    public Sprite sprite;
    public string text;
}
