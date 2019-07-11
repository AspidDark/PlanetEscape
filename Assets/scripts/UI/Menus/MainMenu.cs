using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject optionsMenu;
    public GameObject mainMenu;

    public int whatLevelToLoad=1;
    ///Дорабоать бегунок звука
    public void PlayGame()
    {
        SceneManager.LoadScene(whatLevelToLoad);
    }

    public void OnClickOptionsMenu()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
