using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneToGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene("HubWorld");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
