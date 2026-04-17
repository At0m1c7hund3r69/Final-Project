using UnityEngine;

public class LogDespawnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the thing touching the zone is specifically tagged "Log"
        if (other.CompareTag("Log"))
        {
            // 2. Safety Check: Look through all the children attached to this log
            // If the player is still riding it, un-parent them so they don't get deleted!
            foreach (Transform child in other.transform)
            {
                if (child.CompareTag("Player"))
                {
                    child.SetParent(null);
                }
            }

            // 3. Now that the player is safely off, destroy the log
            Destroy(other.gameObject);
        }
    }
}