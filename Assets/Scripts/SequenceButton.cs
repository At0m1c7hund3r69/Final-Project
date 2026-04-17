using UnityEngine;

public class SequenceButton : MonoBehaviour
{
    [Tooltip("Assign a unique number to each color. E.g., Green=0, Purple=1, Orange=2")]
    public int buttonID;
    private bool canBePressed = true;

    private void OnTriggerEnter(Collider other)
    {
        PunchHitbox punch = other.GetComponent<PunchHitbox>();

        if (punch != null && canBePressed)
        {
            if (SequencePuzzleManager.Instance != null)
            {
                SequencePuzzleManager.Instance.OnButtonPunched(buttonID);
                canBePressed = false;
                Invoke(nameof(ResetCooldown), 0.5f);
            }
        }
    }

    private void ResetCooldown()
    {
        canBePressed = true;
    }
}