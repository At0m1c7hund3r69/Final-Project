using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {

        PlayerBody body = other.GetComponentInParent<PlayerBody>();

        if (body == null)
        {
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
