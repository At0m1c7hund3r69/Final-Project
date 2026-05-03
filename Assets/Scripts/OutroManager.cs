using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OutroManager : MonoBehaviour
{
    [Tooltip("Drag your VideoPlayer here")]
    public VideoPlayer videoPlayer;

    [Tooltip("Drag the parent object of your Win Menu UI here")]
    public GameObject winScreenUI;

    [Tooltip("Name of the Main Menu scene")]
    public string mainMenuSceneName = "MainMenu";

    private bool isVideoDone = false;

    void Start()
    {
        if (winScreenUI != null) winScreenUI.SetActive(false);

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndReached;
        }
    }

    void EndReached(VideoPlayer vp)
    {
        ShowWinScreen();
    }

    void Update()
    {
        if (!isVideoDone && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)))
        {
            ShowWinScreen();
        }
        else if (isVideoDone && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    void ShowWinScreen()
    {
        isVideoDone = true;
        if (videoPlayer != null) videoPlayer.Stop();

        if (winScreenUI != null) winScreenUI.SetActive(true);
    }
}
