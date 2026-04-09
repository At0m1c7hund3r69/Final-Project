using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    [SerializeField] private PauseMenuManager pauseMenuManager;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBody body = other.GetComponentInParent<PlayerBody>();

        if (body != null)
        {
            pauseMenuManager.ShowLoseMenu();
        }
    }
}