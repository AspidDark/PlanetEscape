using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivator : MonoBehaviour
{

    public void Deactivation(params GameObject[] spr)
    {
        foreach (var f in spr) //deactivation GameObjects
        {
            if (f.active == true) f.SetActive(false);
        }

    }
}
