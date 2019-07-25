using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject creditsPanel;
    public GameObject headText;
    public GameObject playPressedMenu;
    public GameObject loadGameButton;
    [Space]
    public GameObject loadingScreen;
    public Slider loadingBar;
    [Space]
    public GameObject loadingText;
    TextMeshProUGUI loadingTextTextMeshProGui;
    public int whatLevelToLoad=1;

    private void Start()
    {
        loadingTextTextMeshProGui = loadingText.GetComponent<TextMeshProUGUI>();
        if (HelpSaveLoad.GetValue(ConstsLibrary.newGameStarted, 0) == 0)
        {
            loadGameButton.SetActive(false);
        }
    }

    ///Дорабоать бегунок звука
    public void PlayGame()
    {
        mainMenu.SetActive(false);
        playPressedMenu.SetActive(true);
    }

    public void OnBackFromPlayMenuButtonClocked()
    {
        playPressedMenu.SetActive(false);
        mainMenu.SetActive(true);
        
    }

    public void OnClickOptionsMenu()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnClickCreditsButton()
    {
        creditsPanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnClickCreditsMenuBackButton()
    {
        creditsPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadLevelAsync(int sceneIndex)
    {
        AsyncOperation operationInfo = SceneManager.LoadSceneAsync(whatLevelToLoad);

        while (!operationInfo.isDone)
        {
            float progress = Mathf.Clamp01(operationInfo.progress / 0.9f);
            loadingBar.value = progress;
            loadingTextTextMeshProGui.text = progress*100+"%";
            yield return null;
        }
    }

    public void OnNewGameButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        HelpSaveLoad.SetValue(ConstsLibrary.newGameStarted, 1);
        StartLevelOading();


    }

    public void OnLoadGameButtonClicked()
    {
        StartLevelOading();
    }

    private void StartLevelOading()
    {
        playPressedMenu.SetActive(false);
        headText.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(whatLevelToLoad));
    }

}
