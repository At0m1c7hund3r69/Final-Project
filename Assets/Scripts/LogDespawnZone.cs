using UnityEngine;

public class LogDespawnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LogMovement log = other.GetComponentInParent<LogMovement>();

        if (log != null)
        {
            Destroy(log.gameObject);
        }
    }
}
