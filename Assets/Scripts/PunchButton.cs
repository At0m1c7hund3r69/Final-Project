using UnityEngine;
using System.Collections.Generic; // Required for static memory

public class PunchButton : MonoBehaviour
{
    // Static memory to remember if this button was pressed across scene loads
    private static HashSet<string> pressedButtons = new HashSet<string>();

    [Header("Persistence")]
    [Tooltip("Type a unique ID to keep the button's effects saved across level transitions")]
    public string uniqueButtonID;

    [Header("Button Settings")]
    [SerializeField] private bool singleUse = true;

    [Header("What does this button do? (Fill any or all)")]
    [SerializeField] private BridgeTransition targetBridge;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private GameObject objectToDestroy;

    private bool hasBeenPressed;

    private void Start()
    {
        
        if (!string.IsNullOrEmpty(uniqueButtonID) && pressedButtons.Contains(uniqueButtonID))
        {
            hasBeenPressed = true;

            if (objectToSpawn != null)
            {
                objectToSpawn.SetActive(true);
            }

            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
            }

            if (targetBridge != null)
            {
                targetBridge.LowerBridge();
            }
        }
        else
        {
            if (objectToSpawn != null)
            {
                objectToSpawn.SetActive(false);
            }
        }
    }

    public static void ResetButtons()
    {
        pressedButtons.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        PunchHitbox punch = other.GetComponent<PunchHitbox>();

        if (punch == null)
            return;

        if (singleUse && hasBeenPressed)
            return;

        hasBeenPressed = true;

        if (!string.IsNullOrEmpty(uniqueButtonID))
        {
            pressedButtons.Add(uniqueButtonID);
        }

        if (targetBridge != null)
        {
            targetBridge.LowerBridge();
        }

        if (objectToSpawn != null)
        {
            objectToSpawn.SetActive(true);
        }

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            Debug.Log($"{name}: Target object destroyed!");
        }
    }
}