using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LevelGoalManager : MonoBehaviour
{
    public static LevelGoalManager Instance { get; private set; }

    private static Dictionary<string, int> levelProgress = new Dictionary<string, int>();

    public static int GrandTotalCollected { get; private set; }

    [Header("Level Identification")]
    [Tooltip("Type a unique name for this level so the manager remembers its specific score (e.g., 'DinosaurJungle')")]
    public string levelID = "Level_1";

    [Header("Goal Settings")]
    [SerializeField] private int requiredCount = 3;

    [Header("Optional UI")]
    [SerializeField] private TMP_Text objectiveText;

    [Header("Menu Reference")]
    [SerializeField] private PauseMenuManager pauseMenuManager;

    public int CurrentCount
    {
        get
        {
            if (levelProgress.ContainsKey(levelID))
                return levelProgress[levelID];
            return 0;
        }
        private set
        {
            levelProgress[levelID] = value;
        }
    }

    public bool LevelComplete { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (CurrentCount >= requiredCount)
        {
            LevelComplete = true;
        }

        UpdateObjectiveText();
    }

    public void CollectObjective(int amount = 1)
    {
        if (LevelComplete)
            return;

        CurrentCount += amount;
        GrandTotalCollected += amount;

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