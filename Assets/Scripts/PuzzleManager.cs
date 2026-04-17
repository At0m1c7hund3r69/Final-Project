using UnityEngine;
using System.Collections.Generic;

public class SequencePuzzleManager : MonoBehaviour
{
    public static SequencePuzzleManager Instance { get; private set; }

    private static HashSet<string> solvedPuzzles = new HashSet<string>();

    [Header("Persistence")]
    public string uniquePuzzleID = "Level2_ColorPuzzle";

    [Header("Puzzle Logic")]
    [Tooltip("The ID sequence required to win. E.g., if Orange is 2, Green is 0, Purple is 1, enter: 2, 0, 1")]
    public int[] expectedSequence = { 2, 0, 1 };
    private int currentStepIndex = 0;

    [Header("References")]
    public GameObject doorToOpen;
    public AudioSource audioSource;
    public AudioClip failSound;

    private bool isSolved = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (solvedPuzzles.Contains(uniquePuzzleID))
        {
            UnlockDoor();
        }
    }

    public static void ResetSequencePuzzles()
    {
        solvedPuzzles.Clear();
    }

    public void OnButtonPunched(int buttonID)
    {
        if (isSolved) return;

        // Did they press the right button for this step?
        if (buttonID == expectedSequence[currentStepIndex])
        {
            currentStepIndex++; // Move to the next step

            // Check if that was the final button needed
            if (currentStepIndex >= expectedSequence.Length)
            {
                SolvePuzzle();
            }
        }
        else
        {
            currentStepIndex = 0;

            if (audioSource != null && failSound != null)
            {
                audioSource.PlayOneShot(failSound);
            }

            Debug.Log("Wrong sequence! Resetting to step 0.");
        }
    }

    private void SolvePuzzle()
    {
        isSolved = true;
        if (!string.IsNullOrEmpty(uniquePuzzleID))
        {
            solvedPuzzles.Add(uniquePuzzleID);
        }

        UnlockDoor();
        Debug.Log("Puzzle Solved! Door Opened.");
    }

    private void UnlockDoor()
    {
        isSolved = true;
        if (doorToOpen != null)
        {
            doorToOpen.SetActive(false);
        }
    }
}