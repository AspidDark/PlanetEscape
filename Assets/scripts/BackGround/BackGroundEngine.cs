using UnityEngine;

public class BackGroundEngine : MonoBehaviour {

    public static BackGroundEngine instance;
    private ParralaxingBackGround2[] parralaxingBackGround;
    private void Awake()
    {
        instance = instance ?? this;
    }
    // Use this for initialization
    void Start () {
        instance = instance ?? this;
        parralaxingBackGround = gameObject.GetComponentsInChildren<ParralaxingBackGround2>();
    }
	
    public void GenerateBackGrounds(int number)
    {
        foreach (var item in parralaxingBackGround)
        {
            item.tempValue = number;
        }
    }
}
