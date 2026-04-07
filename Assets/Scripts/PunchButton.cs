using UnityEngine;

public class PunchButton : MonoBehaviour
{
    [SerializeField] private BridgeTransition targetBridge;
    [SerializeField] private bool singleUse = true;

    private bool hasBeenPressed;

    private void OnTriggerEnter(Collider other)
    {
        PunchHitbox punch = other.GetComponent<PunchHitbox>();

        if (punch == null)
            return;

        if (singleUse && hasBeenPressed)
            return;

        hasBeenPressed = true;

        if (targetBridge != null)
        {
            targetBridge.LowerBridge();
        }
    }
}
