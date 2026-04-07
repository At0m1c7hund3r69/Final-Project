using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        PlayerBody body = other.GetComponentInParent<PlayerBody>();

        if (body == null)
        {
            Debug.Log("Entered object is not the player.");
            return;
        }

        Debug.Log("Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
