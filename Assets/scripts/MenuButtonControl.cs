using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class MenuButtonControl : MonoBehaviour
{
    public static MenuButtonControl instance;
    #region //Game Panel
    public GameObject inGamePannel;
    #endregion


#region //In Menu Pannel
    public GameObject inMenusPanel;

    public GameObject techPannel;
    public GameObject economicPanel;

    public GameObject mainShowingMenuPanel;
    public GameObject switchButton;
    #endregion

    public GameObject mainCanvas;

    public GameObject optionsMenu;
    public GameObject soundOptionsMenu;
    public GameObject pannel;
  
    private void Awake()
    {
        instance = instance ?? this;
    }

    private void Start()
    {
        instance = instance ?? this;
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

        switchButton.GetComponentInChildren<Text>().text = "To Thech";
    }

    private void ToTechMenuButtonClick()
    {
        NodeInformer.instance.OnCancelQuestButtonClick();
        techPannel.SetActive(true);
        mainShowingMenuPanel.SetActive(true);
        economicPanel.SetActive(false);
        switchButton.GetComponentInChildren<Text>().text = "To Economic";
     }

    public void StartButtonClick()
    {
        NodeInformer.instance.OnCancelQuestButtonClick();
        if (PlayerStats.instance.playerCash<0)
        {
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
        print("Quit");
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

   // public void 



    //public void ResetAll()
    //{
    //    SetStartingPosition(AllObjectData.instance.go);
    //    RocketMovement.instance.ResetVaues();

    //    AllObjectData.instance.go.SetActive(true);
    //    //Paint o white
    //    GameMaster.instance.GameMaterReset();
    //    RocketMovement.instance.PostResetValues();
    //    GameMaster.instance.ActivationClick();///Сюда цифры передавать
    //    PlayerStats.instance.SetDefaults();

    //    QuestMainEngine.instance.ResetQuest();
    //    AllObjectData.instance.SetStartingValues();

    //    NodeInformer.instance.StartingSetup();
    //    InGameTimer.instance.ResetValues();
    //    WeatherEngine.instance.StartingInitiation();
    //    VisualEffectHelper.instance.SetBackGroundColor(ConstsLibrary.baseRed, ConstsLibrary.baseGreen, ConstsLibrary.baseBlue, ConstsLibrary.baseAlpha);
    //}


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
}
