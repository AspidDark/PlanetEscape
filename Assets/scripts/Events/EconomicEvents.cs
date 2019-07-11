using UnityEngine;
[CreateAssetMenu(fileName = "New EconomicEvent", menuName = "Events/New EconomicEvent")]
public class EconomicEvents : ScriptableObject {

    public string eventName;
    public string eventDescription;
    public float minEffect;
    public float maxEffect;

    public virtual void Prevent()
    {

    }

    public virtual void Empower()
    {

    }

}
