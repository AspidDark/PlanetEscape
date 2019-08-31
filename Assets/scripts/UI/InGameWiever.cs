using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameWiever : MonoBehaviour
{

    public static InGameWiever instance;
    //timer
    public GameObject timer;
    TextMeshProUGUI timerTextMeshProGui;

    //fuel
    public UIFuelCounter uIFuelCounter;
    public float fuelValue;
    public float maxFuel;

    //heat
    public Image heatImage;
    public float heatImageFiller;

    //health
    public HealthBar healthBar;

    //money
    public float moneyAmount;
    public GameObject moneyText;
    public TextMeshProUGUI moneyTextMeshProGui;

    //day
    public GameObject dayText;

    TextMeshProUGUI dayTextMeshProGui;

    //Height Counter
    public GameObject toSurfaceRangeCounter;
    public TextMeshProUGUI toSurfaceTextMeshProGui;

    [Space]
    public GameObject warningTemplateGameObject;
    public TextMeshProUGUI warningTemplate;

    public GameObject eventDescriptionGameObject;
    public TextMeshProUGUI eventDescription;

    private bool sizeChangePresed = false;

    private void Awake()
    {
        instance = instance ?? this;
        dayTextMeshProGui = dayText.GetComponent<TextMeshProUGUI>();
        moneyTextMeshProGui = moneyText.GetComponent<TextMeshProUGUI>();
        timerTextMeshProGui = timer.GetComponent<TextMeshProUGUI>();

    }
    private void Start()
    {
        instance = instance ?? this;
        //fuel
        uIFuelCounter.fuelMaxSize = RocketStats.instance.MaxFuelCapacity;

    }
    public void SetTimerText(string timerText)
    {
        //timer
        timerTextMeshProGui.text = timerText;
        //timer.GetComponent<TextMeshProUGUI>().text = timerText;
    }

    public void SetMoneyText(float amount)
    {
        moneyTextMeshProGui.text = amount.ToString("f1");
        // moneyText.GetComponent<TextMeshProUGUI>().text = amount.ToString("f1");
    }

    public void SetDayText(int day)
    {
        dayTextMeshProGui.text = "Day: " + day.ToString();
    }

    private void Update()
    {
        //fuel
        uIFuelCounter.fuelMaxSize = maxFuel;
        uIFuelCounter.FuelValue = fuelValue;

        //heat
        heatImage.fillAmount = heatImageFiller / 100;

        //Height show
        HeightCheckerValue(AllObjectData.instance.posY);
    }
    #region HelthBar
    public void DamageViewer(int amount, int fullHealth)
    {
        healthBar.DecreaseBar(amount, fullHealth);
    }

    public void ResetHelthBar(int amount)
    {
        healthBar.ResetBar(amount);
    }
    #endregion
    #region HeigthWiever
    public void HeightCheckerActivation(bool result = true)
    {
        if (PlayerStats.instance.openTechNodesCount > 0)
            toSurfaceRangeCounter.SetActive(result);
    }

    public void HeightCheckerValue(float height)
    {

        if (toSurfaceRangeCounter.active && toSurfaceRangeCounter != null)
        {
            toSurfaceTextMeshProGui.text = (height - ConstsLibrary.surfacelevel).ToString("f1");
        }
    }
    #endregion

    #region Event Informer
    public void EventInformerActivator(bool result = true)
    {
        if (PlayerStats.instance.openTechNodesCount > 0)
        {
            warningTemplateGameObject.SetActive(result);
            eventDescriptionGameObject.SetActive(result);
        }

    }
    public const string warningTemplateText = "WARNING!!!";
    public void EventInformer(string message)
    {
        warningTemplate.text = warningTemplateText;
        eventDescription.text = message;
        Invoke("EventInformerDisable", 3f);
    }

    private void EventInformerDisable()
    {
        warningTemplate.text = string.Empty;
        eventDescription.text = string.Empty;
    }

    public void QuestCompletedInformer(string message)
    {
        eventDescription.text = message;
        Invoke("QuestCompletedInformerDisable", 3f);
    }
    private void QuestCompletedInformerDisable()
    {
        eventDescription.text = string.Empty;
    }


    #endregion
    #region Closest Object TODO
    public void ClosestObjectActivation(bool result = true)
    {
        //if (PlayerStats.instance.openTechNodesCount > 0)
        //    toSurfaceRangeCounter.SetActive(result);
    }

    public void ClosestObjectValue(float height)
    {

        //if (toSurfaceRangeCounter.active && toSurfaceRangeCounter != null)
        //{
        //    toSurfaceTextMeshProGui.text = (height - ConstsLibrary.surfacelevel).ToString("f1");
        //}
    }
    #endregion


    public void ChangeTextFontSize(TextMeshProUGUI textToChange, float baseSize, float greaterSize, float timeOfEffect = 0.5f)
    {
        StopCoroutine("SizeChanger");
        textToChange.fontSize = baseSize;
        if(!sizeChangePresed)
        StartCoroutine(SizeChanger(textToChange, baseSize, greaterSize, timeOfEffect));
    }
    IEnumerator SizeChanger(TextMeshProUGUI textToChange, float baseSize, float greaterSize, float timeOfEffect)
    {
        sizeChangePresed = true;
        float valuteToIncrease = (greaterSize - baseSize) / (timeOfEffect / 2);
        float timer = timeOfEffect / 2;
        while (timer > 0)
        {
            textToChange.fontSize += valuteToIncrease * MainCount.instance.fixedDeltaTime;
            timer -= MainCount.instance.fixedDeltaTime;
            yield return null;
        }
        timer = timeOfEffect / 2;
        while (timer > 0)
        {
            textToChange.fontSize -= valuteToIncrease * MainCount.instance.fixedDeltaTime;
            timer -= MainCount.instance.fixedDeltaTime;
            yield return null;
        }
        sizeChangePresed = false;
        textToChange.fontSize = baseSize;
    }
}
