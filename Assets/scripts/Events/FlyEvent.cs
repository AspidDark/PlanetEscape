using UnityEngine;

[CreateAssetMenu(fileName ="New FlyEvent", menuName ="Events/FlyEvent")]
public class FlyEvent : ScriptableObject {

    public string eventName;
    public string eventDescription;
    public float minEffect;
    public float maxEffect;
    public float maxHeight;
    public float minHeight;
    [Range(ConstsLibrary.minChance, ConstsLibrary.maxChance)]
    public int chance;
    public GameEventType gameEventType;
    [Range(ConstsLibrary.minHardness, ConstsLibrary.maxHardness)]
    public int eventHardness;
}

