using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HeightsAndColors
{
    public float fromHeight;
    public Color32 fromColor;
    public float toHeight;
    public Color32 toColor;
}

public struct GrowSpeed
{
    public float redGrow;
    public float greenGrow;
    public float blueGrow;
    public float alphaGrow;
}

public class VisualEffectHelper : MonoBehaviour {
    #region //BackGround work
    public Camera cam;
    public HeightsAndColors[] heightsAndColors;
    private GrowSpeed[] growSpeeds;
    #endregion

    public static VisualEffectHelper instance;
    private List<int> visualEffectObjectId = new List<int>();

    // Use this for initialization
    void Start () {
        //BackGround work
        cam = Camera.main;

        instance = instance ?? this;
        CountAllDeltas();
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region     //BackGround work
        if (!AllObjectData.instance.IsNotFlyinig())
        {
            for (int i = 0; i < heightsAndColors.Length; i++)
            {
                if (AllObjectData.instance.posY > heightsAndColors[i].fromHeight
                    && AllObjectData.instance.posY <= heightsAndColors[i].toHeight)
                {
                    float currentHeightPositionToColor = AllObjectData.instance.posY - heightsAndColors[i].fromHeight;
                    byte red = Convert.ToByte(heightsAndColors[i].fromColor.r + growSpeeds[i].redGrow * currentHeightPositionToColor);
                    byte green = Convert.ToByte(heightsAndColors[i].fromColor.g + growSpeeds[i].greenGrow * currentHeightPositionToColor);
                    byte blue = Convert.ToByte(heightsAndColors[i].fromColor.b + growSpeeds[i].blueGrow * currentHeightPositionToColor);
                    byte alpha = Convert.ToByte(heightsAndColors[i].fromColor.a + growSpeeds[i].alphaGrow * currentHeightPositionToColor);
                    cam.backgroundColor = new Color32(red, green, blue, alpha);
                }
            }
        }
        
#endregion
    }
    public void ToBaseColor(List<SpriteRenderer> renderer, int id)
    {
        foreach (var item in renderer)
        {
            item.color = ConstsLibrary.proColor;
        }
    }
   
    public void RedFlash(List<SpriteRenderer> renderer, int id, float time = 0.5f)
    {

        PainToColor(renderer, id, ConstsLibrary.redFlashColor, time);
    }

    public void PainToColor(List<SpriteRenderer> spriteRenderer, int id, Color32 color32, float timeAfterToPaintBack = 0f)
    {
        if (visualEffectObjectId.Contains(id))
        {
            return;
        }
        if (timeAfterToPaintBack <= 0)
        {
            foreach (var item in spriteRenderer)
            {
                item.color = color32;
            }
            return;
        }
        foreach (var item in spriteRenderer)
        {
            item.color = Color.white;
            StartCoroutine(PaintToColorInvoke(item, id, color32, timeAfterToPaintBack));
        }
        
    }

    public void CountAllDeltas()
    {
        growSpeeds = new GrowSpeed[heightsAndColors.Length];
        CountDeltas();
    }
    private IEnumerator PaintToColorInvoke(SpriteRenderer spriteRenderer, int id,  Color32 colortoPaintTo, float timeAfterToPaintBack)
    {
        visualEffectObjectId.Add(id);
        Color32 startingColor = spriteRenderer.color;
        spriteRenderer.color = colortoPaintTo;
        yield return new WaitForSeconds(timeAfterToPaintBack);
        spriteRenderer.color = startingColor;
        visualEffectObjectId.Remove(id);
    }

    public void RemoveFromPainting(int id)
    {
        visualEffectObjectId.Clear();
    }


    private IEnumerator RedFlashInvoke(GameObject go, float time)
    {
        Color previousColor = go.GetComponent<SpriteRenderer>().color;
        go.GetComponent<SpriteRenderer>().color = ConstsLibrary.redFlashColor;
        yield return new WaitForSeconds(time);
        go.GetComponent<SpriteRenderer>().color = previousColor;
    }

#region //BackGround work
    private void CountDeltas()
    {
        for (int i = 0; i < heightsAndColors.Length; i++)
        {
            float heightDelta = heightsAndColors[i].toHeight - heightsAndColors[i].fromHeight;
            growSpeeds[i].redGrow = (heightsAndColors[i].toColor.r - heightsAndColors[i].fromColor.r) / heightDelta;
            growSpeeds[i].greenGrow = (heightsAndColors[i].toColor.g - heightsAndColors[i].fromColor.g) / heightDelta;
            growSpeeds[i].blueGrow = (heightsAndColors[i].toColor.b - heightsAndColors[i].fromColor.b) / heightDelta;
            growSpeeds[i].alphaGrow = (heightsAndColors[i].toColor.a - heightsAndColors[i].fromColor.a) / heightDelta;
        }
    }

    public void SetBackGroundColor(Color32 color)
    {
        cam.backgroundColor = color;
    }
    public void SetBackGroundColor(byte red, byte green, byte blue, byte alpha)
    {
        cam.backgroundColor = new Color32(red, green, blue, alpha);
    }
    #endregion


}
