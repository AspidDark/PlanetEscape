using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFuelCounter : MonoBehaviour {


    public float fuelMaxSize;
    public Image bar;
    public RectTransform button;

    private float fuelValue=0.001f;
    public float FuelValue { get { return fuelValue; }
        set {
            if (value > 0)
            {
                fuelValue = value;
            }
            else
            {
                fuelValue = 0.001f;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        FuelChange(fuelValue, fuelMaxSize);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = fuelValue.ToString("f1");
    }

    private  void FuelChange(float value,float fuelMaxSizeAmount)
    {
        if (fuelMaxSizeAmount <= 0)
        {
            return;
        }
        float amount = value / fuelMaxSizeAmount * 0.5f;
        bar.fillAmount = amount;
        button.localEulerAngles = new Vector3(0, 0, -amount * 360);
    }
}
