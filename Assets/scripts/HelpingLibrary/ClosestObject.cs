using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestObject : MonoBehaviour
{
    public static ClosestObject instance;

    public GameObject fromGameobject;
    private void Start()
    {
        instance = instance ?? this;
        if (fromGameobject == null)
        {
            fromGameobject = GameObject.FindGameObjectWithTag("Player");
        }
        instance = instance ?? this;
    }

    List<GameObject> objectList = new List<GameObject>();


    public void AddToArray(GameObject gameObject)
    {
        objectList.Add(gameObject);
    }

    public void RemoveForomArray(GameObject gameObject)
    {
        objectList.Remove(gameObject);
    }

    public float GetMinimunDistance()
    {
        if (objectList == null||objectList.Count==0)
        {
            return 1000;
        }
        return Vector3.Distance(fromGameobject.transform.position, FindClosestObject(objectList, fromGameobject).transform.position);

    }

    /// <summary>
    /// Finding closest object from array to object
    /// </summary>
    /// <param name="arrayFrom">Array jf objects too look in</param>
    /// <param name="toWaht">object to what closest must be found</param>
    /// <returns></returns>
    public GameObject FindClosestObject(List<GameObject> arrayFrom, GameObject toWaht)
    {
        GameObject closest = null;
        float distance = float.MaxValue;
        Vector3 position = toWaht.transform.position;
        foreach (GameObject go in arrayFrom)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    /// <summary>
    /// Finding closest object from array to object
    /// </summary>
    /// <param name="arrayFrom">Array jf objects too look in</param>
    /// <param name="toWaht">object to what closest must be found</param>
    /// <returns></returns>
    public GameObject FindClosestObject(GameObject[] arrayFrom, GameObject toWaht, string objectName)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = toWaht.transform.position;
        foreach (GameObject go in arrayFrom)
        {
            if (go.name == objectName)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

}
