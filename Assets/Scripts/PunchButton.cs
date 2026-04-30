using UnityEngine;
using System.Collections.Generic;

public class PunchButton : MonoBehaviour
{
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

    [Header("Hub World Win Settings")]
    [Tooltip("Check this ONLY for the final button in the Hub World")]
    [SerializeField] private bool isWinButton = false;
    [Tooltip("How many hourglasses are required to press this?")]
    [SerializeField] private int requiredHourglasses = 9;
    [Tooltip("Drag the Hub World's PauseMenuManager here")]
    [SerializeField] private PauseMenuManager pauseMenuManager;

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

        if (isWinButton)
        {
            if (LevelGoalManager.GrandTotalCollected < requiredHourglasses)
            {
                Debug.Log("Player tried to win, but doesn't have enough Hourglasses yet!");
                return;
            }

            if (pauseMenuManager != null)
            {
                pauseMenuManager.ShowWinMenu();
            }
        }

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
        }
    }
}