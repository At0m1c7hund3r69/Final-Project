using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneToGame : MonoBehaviour
{
    [Tooltip("Drag your VideoPlayer object here from the Hierarchy")]
    public VideoPlayer videoPlayer;

    [Tooltip("The exact name of the next level/hub to load")]
    public string nextSceneName = "HubWorld";

    void Start()
    {
        // Wait for the video to finish naturally
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndReached;
        }
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}