using UnityEngine;

public class FleeDetection : MonoBehaviour
{
    private LevelGoalRunning runner;

    private void Awake()
    {
        runner = GetComponentInParent<LevelGoalRunning>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>() ?? other.GetComponentInParent<PlayerMovement>();

        if (pm != null)
        {
            runner.SetPlayerInRange(pm.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>() ?? other.GetComponentInParent<PlayerMovement>();

        if (pm != null)
        {
            runner.SetPlayerInRange(pm.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>() ?? other.GetComponentInParent<PlayerMovement>();

        if (pm != null)
        {
            runner.ClearPlayerInRange(pm.transform);
        }
    }
}
