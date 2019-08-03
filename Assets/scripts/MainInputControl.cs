using UnityEngine;

public class MainInputControl : MonoBehaviour {

    public static MainInputControl instance;
    public bool cheatsEnabled;
    public float tmpChash;
    private void Awake()
    {
        instance = instance ?? this;
    }
    // Use this for initialization
    void Start () {
        instance = instance ?? this;
    }

    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            RocketMovement.instance.moveForward = true;
        }
        else
        {
            RocketMovement.instance.moveForward = false;
        }
        if (Input.GetKey("a"))
        {
            RocketMovement.instance.moveLeft = true;
        }
        else
        {
            RocketMovement.instance.moveLeft = false;
        }
        if (Input.GetKey("d"))
        {
            RocketMovement.instance.moveRight = true;
        }
        else
        {
            RocketMovement.instance.moveRight = false;
        }
    }

    // Update is called once per frame
    void Update () {
        if (cheatsEnabled)
        {
            if (Input.GetKeyDown("m"))
            {
                PlayerStats.instance.AddCash(tmpChash);
                print("CashCame" + tmpChash);
            }
            if (Input.GetKeyDown("k"))
            {
                RocketMovement.instance.AddFuel(15);
            }
        }
        if (Input.GetKeyDown("o"))
        {
            HelpSaveLoad.DeleteAllExeptSystem();
            print("DeleteAllExeptSystem");
        }
    }
}
