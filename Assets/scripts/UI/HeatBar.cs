using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HeatBar : MonoBehaviour {

    public Image imageBar;
    public Color32 startColor;
    public Color32 middleColor;
    public Color32 finalColor;

    private void Start()
    {
        if (imageBar == null)
        {
            imageBar = GetComponent<Image>();
        }
    }
    void Update ()
    {
        if (imageBar.fillAmount > .66f)
        {
            if (imageBar.fillAmount > 0.84)
            {
                imageBar.color = finalColor;
             }
            else
            {
                imageBar.color= middleColor;
            }
        }
        else
        {
            imageBar.color = startColor;
        }
	}
}
