using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsArray : MonoBehaviour
{

    public static ObjectsArray current;
    public static GameObject[] enemiesGameObject;
    public static GameObject[] housesGameObject;
    public static GameObject[] barnsGameObject;
    public static GameObject[] horsesGameobject;
    public static GameObject[] cowsGameobject;

    // Use this for initialization
    void Start()
    {
        enemiesGameObject = GameObject.FindGameObjectsWithTag("enemy");
        housesGameObject = GameObject.FindGameObjectsWithTag("house");
        barnsGameObject = GameObject.FindGameObjectsWithTag("barn");
        horsesGameobject = GameObject.FindGameObjectsWithTag("horse");
        cowsGameobject = GameObject.FindGameObjectsWithTag("cow");
    }

}
