using System;
using UnityEngine;

public class AirObjectMove : MonoBehaviour {

    public AirObjectScriptable airObjectScriptable;
   // private bool moveLeft;
    public float xSpeed;
    public float ySpeed;

    public bool isLeftToPlayer;
  
    #region DestroyCheck
    private float destroyCheckTimer;
    public float timeBetweenHeightChecks = 0.5f; //set to private!!!!!
    #endregion

    #region //Changing Direction
    public bool changingDirection=false;
    public float changeDirectionTime = 2;
    private float changeDirectionTimer;
    private bool moveUp = true;
    #endregion

    private void Start()
    {
        StartingInitiation();
    }

    private void OnEnable()
    {
        if (ClosestObject.instance == null)
        {
            return;
        }
        ClosestObject.instance.AddToArray(this.gameObject);
        StartingInitiation();
    }

    private void OnDisable()
    {
        if (ClosestObject.instance == null)
        {
            return;
        }
        ClosestObject.instance.RemoveForomArray(this.gameObject);
        transform.localScale = new Vector3(1, 1, 1);
        CancelInvoke();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveX();
        MoveY();
    }
    public virtual void MoveX()
    {
        if (isLeftToPlayer)
        {
            transform.Translate(Vector3.right * MainCount.instance.fixedDeltaTime * xSpeed);
        }
        else
        {
            transform.Translate(Vector3.left * MainCount.instance.fixedDeltaTime * xSpeed);
        }
    }

    public virtual void MoveY()
    {
        if (changingDirection)
        {
            if (moveUp)
            {
                transform.Translate(Vector3.up * MainCount.instance.fixedDeltaTime * Mathf.Abs(ySpeed));
            }
            else
            {
                transform.Translate(Vector3.down * MainCount.instance.fixedDeltaTime * Mathf.Abs(ySpeed));
            }
        }
        else
        {
            if (ySpeed >= 0)
            {
                transform.Translate(Vector3.up * MainCount.instance.fixedDeltaTime * ySpeed);
            }
            else
            {
                transform.Translate(Vector3.down * MainCount.instance.fixedDeltaTime * Mathf.Abs(ySpeed));
            }
        }
    }


    public void Update()
    {
        if (destroyCheckTimer <= 0)
        {
            destroyCheckTimer = timeBetweenHeightChecks;
            CheckDestroy();
        }
        destroyCheckTimer -= MainCount.instance.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            changeDirectionTimer = changeDirectionTime;
            moveUp = !moveUp;
        }
        changeDirectionTimer -= MainCount.instance.deltaTime;
    }

    private void CheckDestroy()
    {
        if ((Mathf.Abs(this.transform.position.x - AllObjectData.instance.posX)  > NodeInformer.instance.cameraXWidth * 2)|| IsAcceptableDistance())
        {
            Destroy();
        }
    }
    private bool IsAcceptableDistance()
    {
        return Vector3.Distance(this.transform.position, AllObjectData.instance.go.transform.position) > ConstsLibrary.maxObjectDistance;
    }

    private void StartingInitiation()
    {
        CountAll();
        destroyCheckTimer = timeBetweenHeightChecks;
        isLeftToPlayer = IsLeftToPlayer();
        if (!isLeftToPlayer)
        {
            Flip();
        }
        else
        {
            ForwardFlip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
    private void ForwardFlip()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
   


    private void CountAll()
    {
        xSpeed = MainCount.instance.FloatRandom(airObjectScriptable.minSpeed, airObjectScriptable.maxSpeed);
        ySpeed= MainCount.instance.FloatRandom(airObjectScriptable.minYSpeed, airObjectScriptable.maxYSpeed);
    }

    private bool IsLeftToPlayer()
    {
               return this.gameObject.transform.position.x < AllObjectData.instance.posX;
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("Player ShotDestroyed  OnCollisionEnter2D");
    //    Destroy();
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("EnemyFace") || collision.CompareTag("Enemy"))
    //    {
    //        return;
    //    }
    //    print("Player ShotDestroyed  OnTriggerEnter2D");
    //    Destroy();
    //}
}
