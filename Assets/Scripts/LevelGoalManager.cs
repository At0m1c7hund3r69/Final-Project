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

    [Header("Level Goal Settings")]
    [SerializeField] private int requiredCount = 3;

    [Header("Global Win Settings")]
    [Tooltip("Total hourglasses needed across ALL levels to trigger the Win Screen")]
    [SerializeField] private int globalRequiredCount = 9;

    [Header("Optional UI")]
    [SerializeField] private TMP_Text objectiveText;

    [Header("Menu Reference")]
    [SerializeField] private PauseMenuManager pauseMenuManager;

    [Header("Sign From God")]
    [Tooltip("The spotlight that points at the final button")]
    [SerializeField] private GameObject signFromGodLight;

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
        CheckSignFromGod();
    }

    public static void ResetGoals()
    {
        GrandTotalCollected = 0;
        levelProgress.Clear();
    }

    public void CollectObjective(int amount = 1)
    {
        if (LevelComplete)
            return;

        CurrentCount += amount;
        GrandTotalCollected += amount;

        UpdateObjectiveText();
        CheckSignFromGod();
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = " " + CurrentCount + " / " + requiredCount;
        }
    }

    private void CheckSignFromGod()
    {
        if (signFromGodLight != null)
        {
            signFromGodLight.SetActive(GrandTotalCollected >= globalRequiredCount);
        }
    }
}