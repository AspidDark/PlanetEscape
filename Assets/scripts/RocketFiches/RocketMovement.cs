using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public static RocketMovement instance;

    #region //Stages
    public int additionalStagesCount;
    public int AdditionalStagesCount
    {
        get
        {
            return additionalStagesCount;
        }
        set
        {
            additionalStagesCount = value;
            SaveRocketStagesValue(additionalStagesCount);
        }
    }
    public bool singleStageNotDroped;
    public bool firstStageNotDroped;
    public bool secondStageNotDroped;
    //To ActivateOnly 1
    public GameObject[] rocketTypes;
    #endregion

    #region //Disable vars
    private float disableTimer;
    private bool greatDisable;
    private bool steersDisable;
    /// <summary>
    /// Disabe all movement
    /// </summary>
    public bool diasbleAll;
    #endregion

    public RocketStats rocketStats;
    #region//Rocket Stats

    /// <summary>
    /// Rocket Engine Power Mulitplyer
    /// </summary>
    public float mainEnginePower;
    /// <summary>
    /// Rocket Rotation Power
    /// </summary>
    public float rotationEnginePower;
    /// <summary>
    /// Rocket Fuel
    /// </summary>
    public float fuel;
    public float maxFuel;
    /// <summary>
    /// Rocket Weight
    /// </summary>
    public float rocketWeight;

    public int maxRocketHealth = 15;
    public int healthCounter;

    /// <summary>
    /// ////////////////
    /// </summary>
    public float rocketHeatRate;
    public float rocketHeat;

    #region //Particle work
    public int particleClorNumber;

    private float engineFlameSize;
    private float leftSteer;
    private float rightSteer;

    #endregion
    //Max Rocket  Speed
    public float maxSpeed = 30;



    public float canTakeDamage;
    private float canTakeDamageTimer = 1;

    private List<SpriteRenderer> allRocketSprites = new List<SpriteRenderer>();

    #region MovementControls
    [HideInInspector]
    public bool moveForward;
    [HideInInspector]
    public bool moveLeft;
    [HideInInspector]
    public bool moveRight;
    #endregion

    private void Awake()
    {
        instance = instance ?? this;
    }

    public void UpdateValues()
    {
        moveForward = moveLeft = moveRight = false;

        mainEnginePower = RocketStats.instance.MaxEngine;
        rotationEnginePower = RocketStats.instance.MaxRotation;
        fuel = RocketStats.instance.MaxFuelCapacity;
        maxFuel = RocketStats.instance.MaxFuelCapacity;//
        rocketWeight = RocketStats.instance.MaxWeight;
        rocketHeatRate = RocketStats.instance.HeatValue;
        rb.mass = rocketWeight;

        SetRocketType(additionalStagesCount);
    }

    #endregion
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        instance = instance ?? this;
        rb = gameObject.GetComponent<Rigidbody2D>();
        //Disable event
        ResetVaues();
    }
    public void PostResetValues()
    {
        rb.gravityScale = 1;
        rb.mass = rocketWeight;
    }


    public void ResetVaues()
    {
        additionalStagesCount = HelpSaveLoad.GetValue(ConstsLibrary.rocketStagesValue, 0);
        canTakeDamage = canTakeDamageTimer;
        foreach (var item in allRocketSprites)
        {
            item.color = Color.white;
        }
        RocketStageEngine.instance.ResetStages();
        InGameWiever.instance.ResetHelthBar(maxRocketHealth);
        SetRocketType(additionalStagesCount);
        if (allRocketSprites.Count > 0)
        {
            VisualEffectHelper.instance.ToBaseColor(allRocketSprites, gameObject.GetInstanceID());
        }

        healthCounter = maxRocketHealth;
        greatDisable = false;
        steersDisable = false;
        rocketHeat = 0;

        diasbleAll = false;

        //Stages
        singleStageNotDroped = true;
        firstStageNotDroped = true;
        secondStageNotDroped = true;

        RocketEngineParticles.instance.SetFireSetNumber(particleClorNumber);
        List<SpriteRenderer> allSprites = GetComponentsInChildren<SpriteRenderer>().ToList();
        foreach (var item in allSprites)
        {
            if (item.gameObject.active)
            {
                allRocketSprites.Add(item);
            }
        }

        fuel = maxFuel;
        TimerReset();

    }

    private void Update()
    {
        canTakeDamage -= MainCount.instance.deltaTime;
        if (canTakeDamage < -100)
        {
            canTakeDamage = -1;
        }
        CheckDestroy();

    }

    void FixedUpdate()
    {
        XSpeedLimiter();
        Stages();
        VisualEffects();
        RocketHeat();
        //If Rocket OverHeated Disabling control 
        if (rocketHeat > ConstsLibrary.disableRocketHeatLevel)
        {
            DoDisable(1);
            DoHeat(-10);
        }

        if (diasbleAll)
        {
            return;
        }

        //disable event
        disableTimer -= MainCount.instance.fixedDeltaTime;
        if (disableTimer < 0)
        {
            greatDisable = false;
        }
        if (disableTimer < -100)
        {
            disableTimer = -1;
        }
        if (disableTimer > 0 && greatDisable)
        {
            return;
        }
        //disable event

        //if (Input.GetKey("w") && fuel > 0)
        if (moveForward && fuel > 0)
        {
            MoveForward(AllObjectData.instance.gameobjectVelocity.y <= maxSpeed);
            InGameTimer.instance.StartTimer();  //visual Timer Start
            engineFlameSize += MainCount.instance.deltaTime; //visual

            //Sound
            EngineSoundOn();
        }
        else
        {
            engineFlameSize = 0;//visual
            //Sound
            EngineSoundOff();
        }
        //disable event
        if (disableTimer > 0)
        {
            return;
        }
        //disable event

        // if (Input.GetKey("a") && fuel > 0 && !Input.GetKey("d"))
        if (moveLeft && fuel > 0 && !moveRight)
        {
            //rb.AddForce(-transform.right * rotationEnginePower, ForceMode2D.Force);
            transform.Rotate(Vector3.forward * MainCount.instance.fixedDeltaTime * rotationEnginePower);
            fuel -= MainCount.instance.fixedDeltaTime / (ConstsLibrary.steeerEngineUseLessFuel * ConstsLibrary.fuelLossDelimeter);
            rocketHeat += MainCount.instance.fixedDeltaTime * (1 + rocketHeatRate);
            rightSteer += MainCount.instance.deltaTime; //visual
        }
        else
        {
          //  if (!Input.GetKey("d") || fuel < 0)
                if (!moveRight || fuel < 0)
                    leftSteer = 0;//visual
        }

        //if (Input.GetKey("d") && fuel > 0 && !Input.GetKey("a"))
        if (moveRight && fuel > 0 && !moveLeft)
        {
            // rb.AddForce(transform.right * rotationEnginePower, ForceMode2D.Force);
            transform.Rotate(Vector3.back * MainCount.instance.fixedDeltaTime * rotationEnginePower);
            fuel -= MainCount.instance.fixedDeltaTime / (ConstsLibrary.steeerEngineUseLessFuel * ConstsLibrary.fuelLossDelimeter);
            rocketHeat += MainCount.instance.fixedDeltaTime * (1 + rocketHeatRate);
            leftSteer += MainCount.instance.deltaTime; //visual
        }
        else
        {
           // if (!Input.GetKey("a") || fuel < 0)
                if (!moveLeft || fuel < 0)
                rightSteer = 0;//visual
        }

    }

    private void XSpeedLimiter()
    {
        if (rb.velocity.magnitude > ConstsLibrary.maxXSpeed)
        {
            float multypuer = rb.velocity.x > 0 ? 1 : -1;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }


    private void MoveForward(bool idelingEngine)
    {
        if (idelingEngine)
        {
            rb.AddForce(transform.up * mainEnginePower / ConstsLibrary.mainEnginePowerDelimeter);
            fuel -= MainCount.instance.fixedDeltaTime / ConstsLibrary.fuelLossDelimeter;
            rocketHeat += MainCount.instance.fixedDeltaTime * (1 + rocketHeatRate);
        }
        else
        {
            fuel -= MainCount.instance.deltaTime / (ConstsLibrary.mainEngineUseLessFuelDelimeter * ConstsLibrary.fuelLossDelimeter);
            rocketHeat += MainCount.instance.fixedDeltaTime * (1 + rocketHeatRate) / ConstsLibrary.mainEngineUseLessFuelDelimeter;
        }

    }

    private void VisualEffects()
    {
        RocketEngineParticles.instance.SetSize(engineFlameSize);
        RocketEngineParticles.instance.SetRightSize(rightSteer);
        RocketEngineParticles.instance.SetLeftSize(leftSteer);

        InGameWiever.instance.fuelValue = fuel;
        InGameWiever.instance.maxFuel = RocketStats.instance.MaxFuelCapacity;
        InGameWiever.instance.heatImageFiller = rocketHeat;
        TimerStop();
    }

    private void Stages()
    {
        //Stages
        if (additionalStagesCount == 1 && fuel < maxFuel / 2 && singleStageNotDroped)
        {
            singleStageNotDroped = false;
            DropSingleStage(rb.velocity, rb.angularVelocity);
        }

        if (additionalStagesCount == 2 && fuel < maxFuel * 0.6 && firstStageNotDroped)
        {
            firstStageNotDroped = false;

            DropFirstStage(rb.velocity, rb.angularVelocity);
        }
        else
        {
            if (additionalStagesCount == 2 && fuel < maxFuel * 0.3 && secondStageNotDroped)
            {
                secondStageNotDroped = false;

                DropSecondStage(rb.velocity, rb.angularVelocity);
            }
        }
    }

    private void RocketHeat()
    {
        if (rocketHeat >= ConstsLibrary.minRocketHeat)
        {
            rocketHeat -= MainCount.instance.fixedDeltaTime;
        }
        if (rocketHeatRate > ConstsLibrary.maxRocketHeat)
        {
            rocketHeat = ConstsLibrary.maxRocketHeat;
        }
    }

    public void TimerStop()
    {
        if (fuel <= 0) //visual Timer Stop
        {
            InGameTimer.instance.StopTimer();
        }
    }
    public void TimerReset()
    {
        InGameTimer.instance.ResetTimer();
    }


    public void DoDamage(int amount)
    {
        if (amount >= 0)
        {
            if (canTakeDamage > 0)
            {
                return;
            }
            canTakeDamage = canTakeDamageTimer;
            healthCounter -= amount;
            InGameWiever.instance.DamageViewer(amount, maxRocketHealth);
            if (allRocketSprites.Count > 0)
            {
                VisualEffectHelper.instance.RedFlash(allRocketSprites, gameObject.GetInstanceID());
            }
            if (healthCounter <= 0)
            {
                RocketDestroy.instance.Destroy();

                VisualEffectHelper.instance.RemoveFromPainting(gameObject.GetInstanceID());
            }
        }
        else
        {
            if (healthCounter - amount < maxRocketHealth)
            {
                healthCounter -= amount;
                InGameWiever.instance.DamageViewer(amount, maxRocketHealth);
            }
            else
            {

                InGameWiever.instance.DamageViewer(healthCounter - maxRocketHealth, maxRocketHealth);
                healthCounter = maxRocketHealth;
            }

        }
    }
    /// <summary>
    /// Positive adds heat negative removes
    /// </summary>
    /// <param name="amount"></param>
    public void DoHeat(int amount)
    {
        if (rocketHeat + amount > ConstsLibrary.maxRocketHeat)
        {
            rocketHeat = ConstsLibrary.maxRocketHeat;
            return;
        }
        if (rocketHeat + amount < ConstsLibrary.minRocketHeat)
        {
            rocketHeat = ConstsLibrary.minRocketHeat;
            return;
        }
        rocketHeat += amount;
    }

    public void DoDisable(float time)
    {
        greatDisable = time > 3;
        disableTimer = time;
    }

    public void DoRotation(float amount)
    {
        // player.transform.Rotate(0, 0, fromRight * GetEffectValue(flyEvent));
        gameObject.GetComponent<Rigidbody2D>().AddTorque(amount);
    }

    public void DoImpulse(float amount)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * amount);
    }


    private void SetRocketType(int type = 0)
    {
        foreach (var item in rocketTypes)
        {
            item.SetActive(false);
        }
        rocketTypes[type].SetActive(true);
        RocketStageEngine.instance.SetRocketType();
    }

    private void DropSingleStage(Vector3 rbVelocityValue, float rbAngularVelocityValue)
    {
        rb.mass -= 1;
        RocketStageEngine.instance.DropSingleStage(rbVelocityValue, rbAngularVelocityValue);
    }

    private void DropFirstStage(Vector3 rbVelocityValue, float rbAngularVelocityValue)
    {
        rb.mass -= .7f;
        RocketStageEngine.instance.DropFirstStage(rbVelocityValue, rbAngularVelocityValue);
    }
    private void DropSecondStage(Vector3 rbVelocityValue, float rbAngularVelocityValue)
    {
        rb.mass -= .7f;
        RocketStageEngine.instance.DropSecondStage(rbVelocityValue, rbAngularVelocityValue);
    }

    /// <summary>
    /// Adds fuel
    /// </summary>
    /// <param name="amount"></param>
    public void AddFuel(float amount = 5)
    {
        fuel += amount;
    }


    private void SaveRocketStagesValue(int value)
    {
        HelpSaveLoad.SetValue(ConstsLibrary.rocketStagesValue, value);
    }


    private void EngineSoundOn()
    {
        SoundRandom.instance.DoEngineSound();
    }

    private void EngineSoundOff()
    {
        SoundRandom.instance.ResetEngineSound();
    }

    /// <summary>
    /// If Height is less than started and rocket is not destroyed
    /// </summary>
    private void CheckDestroy()
    {
        if (!AllObjectData.instance.rocketDestroyed)
        {
            if (!AllObjectData.instance.isStarted &&
                (gameObject.transform.position.y < ConstsLibrary.heightToDestroyIfNotStarted
                || fuel <= 0
                || InGameTimer.instance.minutCount > ConstsLibrary.timeInMinutesToDestroyAfterIfNotstrarted))
            {
                RocketDestroy.instance.Destroy();
            }
        }

    }
}
