using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FlyEvents : MonoBehaviour
{

    public static FlyEvents instance;

   // public AllObjectData allObjectData;
    public MainCount mainCount;
    public GameObject player;
    public RocketMovement rocketMovement;

    public bool disableEvents;

    public float timeBetweenHeightChecks = 5f; //set to private!!!!!
    private float heightCheckTimer;


    public FlyEvent[] flyEvents;

    private void Awake()
    {
        instance = instance ?? this;
    }
    // Use this for initialization
    void Start()
    {
        heightCheckTimer = timeBetweenHeightChecks;
        player = player ?? GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (heightCheckTimer <= 0 && !disableEvents)
        {
            heightCheckTimer = timeBetweenHeightChecks;
            GenerateEvent();
        }
        heightCheckTimer -= MainCount.instance.deltaTime;
    }

    public void GenerateEvent()
    {
        if (flyEvents == null && flyEvents.Length == 0)
        {
            return;
        }
        //Geting List of Objects whitch can be spawned on player's Height
        List<FlyEvent> listOfEventsOnThisHeight = new List<FlyEvent>();
        
        foreach (var item in flyEvents)
        {
            if (AllObjectData.instance.posY <= item.maxHeight
                && AllObjectData.instance.posY >= item.minHeight)
            {
                listOfEventsOnThisHeight.Add(item);
            }
        }
        //If no object found
        if (listOfEventsOnThisHeight == null || listOfEventsOnThisHeight.Count == 0)
        {
            return;
        }
        int[] eventWeights = new int[listOfEventsOnThisHeight.Count];
        for (int i = 0; i < listOfEventsOnThisHeight.Count; i++)
        {
            eventWeights[i] = listOfEventsOnThisHeight[i].chance;
        }
        int eventToGenerate = mainCount.DifferentWeightRandom(eventWeights);
        if (eventToGenerate < 0)
        {
            return;
        }
        switch (listOfEventsOnThisHeight[eventToGenerate].gameEventType)
        {
            case GameEventType.Impulse:
              //  print("Impulse");
                EventImpulse(listOfEventsOnThisHeight[eventToGenerate]);
                break;
            case GameEventType.RotationImpulse:
              //  print("RotationImpulse");
                EventRotationImpulse(listOfEventsOnThisHeight[eventToGenerate]);
                break;
            case GameEventType.Damage:
               // print("Damage");
                EventDamage(listOfEventsOnThisHeight[eventToGenerate]);
                break;
            case GameEventType.Heat:
              //  print("Heat");
                EventHeat(listOfEventsOnThisHeight[eventToGenerate]);
                break;
            case GameEventType.Control:
              //  print("Control");
                EventControl(listOfEventsOnThisHeight[eventToGenerate]);
                break;
            default:
             //   print("Empty");
                return;
        }
    }

    private void EventImpulse(FlyEvent flyEvent)
    {
        SendEventMessage(flyEvent);
        rocketMovement.DoImpulse(FromRight() * GetEffectValue(flyEvent));
    }
    private void EventRotationImpulse(FlyEvent flyEvent)
    {
        SendEventMessage(flyEvent);
        rocketMovement.DoRotation(FromRight() * GetEffectValue(flyEvent));
    }

    private void EventDamage(FlyEvent flyEvent)
    {
        SendEventMessage(flyEvent);
        rocketMovement.DoDamage((int)GetEffectValue(flyEvent));
    }

    private void EventHeat(FlyEvent flyEvent)
    {
        SendEventMessage(flyEvent);
        rocketMovement.DoHeat((int)GetEffectValue(flyEvent));
    }

    private void EventControl(FlyEvent flyEvent)
    {
        SendEventMessage(flyEvent);
        rocketMovement.DoDisable(GetEffectValue(flyEvent));
    }

    private void SendEventMessage(FlyEvent flyEvent)
    {
        InGameWiever.instance.EventInformer(flyEvent.eventDescription);
    }



    private float GetEffectValue(FlyEvent flyEvent)
    {
        return mainCount.FloatRandom(flyEvent.minEffect, flyEvent.maxEffect);
    }

    private float FromRight()
    {
        if (!mainCount.BoolRandom())
            return -1;
        return 1;
    }
}


