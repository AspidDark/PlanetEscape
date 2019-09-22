using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonControl : MonoBehaviour
{
    public static MenuButtonControl instance;
    #region //Game Panel
    public GameObject inGamePannel;
    #endregion

    [Space]
    public Button getMoneyButton;
    public Button toEconomucButton;

    public UnityEngine.UI.Button optionsButton;
#region //In Menu Pannel
    public GameObject inMenusPanel;

    public GameObject techPannel;
    public GameObject economicPanel;

    public GameObject mainShowingMenuPanel;
    public GameObject switchButton;
    private bool isTechPanell;
    #endregion

    public GameObject mainCanvas;

    public GameObject optionsMenu;
    public GameObject soundOptionsMenu;
    public GameObject pannel;
    [Space]
    public GameObject loadingScreen;
    public Slider loadingBar;
    public GameObject loadingText;
    [HideInInspector]
    public TextMeshProUGUI loadingTextTextMeshProGui;
    [Space]
    public ImageAndTextScriptable[] imageAndTextScriptable;
    public GameObject loadingScreenPicture;
    private Image image;

    public Sprite spriteEconomic;
    public Sprite spriteToTech;

    private void Awake()
    {
        image = loadingScreenPicture.GetComponent<Image>();
        instance = instance ?? this;
    }

    private void Start()
    {
        isTechPanell = true;
        instance = instance ?? this;
        loadingTextTextMeshProGui = loadingText.GetComponent<TextMeshProUGUI>();

    }

    public void OnShopMenuEntered()
    {
        inMenusPanel.SetActive(true);
        inGamePannel.SetActive(false);
        Pause();
    }



    private void ToEconomicMenuButtonClick()
    {
        economicPanel.SetActive(true);
        mainShowingMenuPanel.SetActive(true);
        techPannel.SetActive(false);

        //switchButton.GetComponentInChildren<Text>().text = "To Tech";
        switchButton.GetComponentInChildren<Image>().sprite = spriteToTech;
        isTechPanell = false;
    }

    private void ToTechMenuButtonClick()
    {
        NodeInformer.instance.OnCancelQuestButtonClick();
        techPannel.SetActive(true);
        mainShowingMenuPanel.SetActive(true);
        economicPanel.SetActive(false);
        //switchButton.GetComponentInChildren<Text>().text = "To Economic";
        switchButton.GetComponentInChildren<Image>().sprite = spriteEconomic;
        isTechPanell = true;
     }

    public void StartButtonClick()
    {
        NodeInformer.instance.OnCancelQuestButtonClick();
        if (PlayerStats.instance.playerCash<0)
        {
            InGameWiever.instance.ChangeTextFontSize(InGameWiever.instance.moneyTextMeshProGui,
                ConstsLibrary.moneyTextBaseSize, ConstsLibrary.moneyTextEnlargeSize);
            PaintButtonIfNegativeMoney();
            SendMessageToInformer(ConstsLibrary.negativeCashToGamePressedHead, ConstsLibrary.negativeCashToGamePressedBody);
            return;
        }
        #region reseting for new level
        GameMaster.instance.ResetAll();
        #endregion
        inMenusPanel.SetActive(false);
        inGamePannel.SetActive(true);
        GameMaster.instance.AddDay();
        Resume();
    }

    public void OnNotEnoughMoney()
    {
        InGameWiever.instance.ChangeTextFontSize(InGameWiever.instance.moneyTextMeshProGui,
                ConstsLibrary.moneyTextBaseSize, ConstsLibrary.moneyTextEnlargeSize);
    }

    public void SwitcherButtonClick()
    {
        if (economicPanel.activeSelf)
        {
            ToTechMenuButtonClick();
        }
        else
        {
            ToEconomicMenuButtonClick();
        }
    }


    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
    /// <summary>
    /// Options Button
    /// </summary>
    public void OnOptionsButtonClick()
    {
        Pause();
        optionsButton.gameObject.SetActive(false);
        pannel.SetActive(true);
        optionsMenu.SetActive(true);

    }

    #region Options Menu
    public void OnOptionsSoundOptionsMenuButtonClick()
    {
        optionsMenu.SetActive(false);
        soundOptionsMenu.SetActive(true);
    }
    public void OnOptionsResumeButtonClick()
    {
        optionsButton.gameObject.SetActive(true);
        pannel.SetActive(false);
        optionsMenu.SetActive(false);
        
        if (techPannel.active || economicPanel.active)
        {
            return;
        }
        Resume();

    }

    public void OnOptionsMenuQuitButtonClick()
    {
        Application.Quit();
    }
    #endregion

    /// <summary>
    /// Sound Options Menu
    /// </summary>
    public void OnSoundOptionsMenuBackButtonClick()
    {
        soundOptionsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    private void SetStartingPosition(GameObject go)
    {
        go.transform.position = new Vector3(0.01f,ConstsLibrary.startingPositionY, 0);
        go.transform.rotation = Quaternion.identity;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }
    }

    public void OnSkipDayButtonClicked()
    {
        if (PlayerStats.instance.playerCash > 0)
        {
            SendMessageToInformer(ConstsLibrary.positiveCashLookForMoneyPressedHead, ConstsLibrary.positiveCashLookForMoneyPressedBody);
            return;
        }
        SendMessageToInformer(ConstsLibrary.negativeCashLookForMoneyPressedHead, ConstsLibrary.negativeCashLookForMoneyPressedBody);
        PlayerStats.instance.DaySkiped();
    }

    private void SendMessageToInformer(string head, string body)
    {
        NodeInformer.instance.ShowMessage(head, body);
    }

    public void SetLoadingScreenActive()
    {
        loadingScreen.SetActive(true);
    }

    public void SetWinLoseScreen(int number)
    {
        if (number < imageAndTextScriptable.Length)
        {
            image.sprite = imageAndTextScriptable[number].sprite;
        }
    }
    public void PaintButtonIfNegativeMoney()
    {
        if (isTechPanell)
        {
            ButtonLightUp(toEconomucButton, ConstsLibrary.proColor, ConstsLibrary.redFlashColor);
        }
        else
        {
            ButtonLightUp(getMoneyButton, ConstsLibrary.proColor, ConstsLibrary.redFlashColor);
        }
    }



    public void ButtonLightUp(Button btn, Color32 baseColor, Color32 toColor, float time=0.5f)
    {
        var colors = btn.colors;
        colors.normalColor = baseColor;
        btn.colors = colors;
        StopCoroutine("PaintButton");
        StartCoroutine(PaintButton(btn, baseColor, toColor, time));
    }


    IEnumerator PaintButton(Button btn, Color32 baseColor, Color32 toColor, float time)
    {
        var colors = btn.colors;
        colors.normalColor = toColor;
        btn.colors = colors;
        while (time > 0)
        {
        yield return null;
            time -= MainCount.instance.fixedDeltaTime;
        }
        colors.normalColor = baseColor;
        btn.colors = colors;
        yield return null;
    }
  


}
