using UnityEngine;
using TMPro;

public class LevelGoalManager : MonoBehaviour
{
    public static LevelGoalManager Instance { get; private set; }

    [Header("Goal Settings")]
    [SerializeField] private int requiredCount = 3;

    [Header("Optional UI")]
    [SerializeField] private TMP_Text objectiveText;

    [Header("Menu Reference")]
    [SerializeField] private PauseMenuManager pauseMenuManager;

    public int CurrentCount { get; private set; }
    public bool LevelComplete { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        UpdateObjectiveText();
    }

    public void CollectObjective(int amount = 1)
    {
        if (LevelComplete)
            return;

        CurrentCount += amount;
        UpdateObjectiveText();

        if (CurrentCount >= requiredCount)
        {
            CompleteLevel();
        }
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = "Objectives: " + CurrentCount + " / " + requiredCount;
        }
    }

    private void CompleteLevel()
    {
        LevelComplete = true;

        Debug.Log("Level Complete!");

        if (pauseMenuManager != null)
        {
            pauseMenuManager.ShowWinMenu();
        }
    }
}