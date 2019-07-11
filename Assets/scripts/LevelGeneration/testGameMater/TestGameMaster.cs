using UnityEngine;

public class TestGameMaster : MonoBehaviour {

    public int hardness;
    public int generationObjectsNumber = 0;
    public int inGameEventsNumber = 0;
    public int weatherNumber = 0;
    public int inUpgradeMenuEventsNumber=0;
    public int randomEventsNumber=0;

    public static TestGameMaster instance;
    private void Start()
    {
        instance = instance ?? this;
    }

   
}
