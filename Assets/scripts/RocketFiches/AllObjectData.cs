using UnityEngine;

public class AllObjectData : MonoBehaviour {
    public static AllObjectData instance;


    public GameObject go;
    public Rigidbody2D rb2d;
    // Use this for initialization

    public float posX;
    public float posY;
    public float speed;
    public Vector3 gameObjectPosition;
    public Vector2 gameobjectVelocity;
    public float angleZ;

    public bool isStarted;
    /// <summary>
    /// is rocket destroyed?
    /// </summary>
    public bool rocketDestroyed;
    public bool rocketLanded;
    public bool isSafeLanded;
    [HideInInspector]
    public bool startedRunOnceSent;

    private void Awake()
    {
        instance = instance ?? this;
    }
    void Start () {
        
        rb2d = rb2d ?? go.GetComponent<Rigidbody2D>();
        instance = instance ?? this;
        SetStartingValues();
    }
    public void SetStartingValues()
    {
        isStarted = false;
        rocketLanded = false;
        rocketDestroyed = false;
        startedRunOnceSent = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!startedRunOnceSent && isStarted)
        {
            startedRunOnceSent = true;
            GameMaster.instance.AddIteration();
        }

        gameObjectPosition = go.transform.position;
        gameobjectVelocity = rb2d.velocity;      //to get a Vector3 representation of the velocity
        speed = gameobjectVelocity.magnitude;             // to get magnitude
        posY = go.transform.position.y;
        posX = go.transform.position.x;
        angleZ = go.transform.rotation.eulerAngles.z;

        if (posY > ConstsLibrary.heightStartedFrom)
        {
            isStarted = true;
        }
    }

    public bool IsNotFlyinig()
    {
        return (rocketDestroyed || rocketLanded || isSafeLanded) && isStarted;
    }
}
