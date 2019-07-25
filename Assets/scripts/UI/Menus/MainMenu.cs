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
    }

    ///Дорабоать бегунок звука
    public void PlayGame()
    {
        //Picture Set Active
        //Loading bar SetActive
        //Main Menu disable
        mainMenu.SetActive(false);
        headText.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(whatLevelToLoad));
        
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
}
