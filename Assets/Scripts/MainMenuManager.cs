using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private string gameplaySceneName = "SampleScene";

    private void Start()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        howToPlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
