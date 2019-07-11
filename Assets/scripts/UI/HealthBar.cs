using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{

    public Image imageBar;
    public Color32 startColor;
    public Color32 middleColor;
    public Color32 finalColor;

    public float allHpAmount;

    private void Awake()
    {
        imageBar = imageBar ?? GetComponent<Image>();
    }

    private void Start()
    {
        imageBar = imageBar ?? GetComponent<Image>();
    }

    public void DecreaseBar(int amount, int _allHpAmount)
    {

        allHpAmount = _allHpAmount;
        imageBar.fillAmount -= amount / allHpAmount;
    }
    private void Update()
    {
        if (imageBar.fillAmount < 2 / allHpAmount)
        {
            imageBar.color = finalColor;
            return;
        }
        if (imageBar.fillAmount < 4 / allHpAmount)
        {
            imageBar.color = middleColor;
            return;
        }
        else
        {
            imageBar.color = startColor;
        }
    }
    public void ResetBar(int _allHpAmount)
    {
        allHpAmount = _allHpAmount;
        imageBar.fillAmount = _allHpAmount;
    }
}
