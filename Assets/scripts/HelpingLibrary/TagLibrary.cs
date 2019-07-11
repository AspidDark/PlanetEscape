using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagLibrary : MonoBehaviour
{

    public static List<string> tagList = new List<string>();
    void Awake()
    {
        tagList.Add("Lava");
        tagList.Add("Bush");
        tagList.Add("PlayerShot");
    }
}
