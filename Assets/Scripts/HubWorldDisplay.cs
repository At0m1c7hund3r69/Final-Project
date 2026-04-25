using UnityEngine;
using TMPro;

public class HubWorldDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text boardText;

    [Header("Game Totals")]
    [Tooltip("Type the total amount of Bells available in the entire game")]
    [SerializeField] private int maxBells = 100;

    [Tooltip("The amount of Hourglasses needed to win")]
    [SerializeField] private int maxHourglasses = 9;

    private void Update()
    {
        // We use Update() just in case the player can collect Bells inside the Hub World itself.
        // This ensures the board updates in real-time as they walk around!
        if (boardText != null)
        {
            boardText.text = "Game Progress\n\n" +
                             "Bells: " + CoinManager.Coins + " / " + maxBells + "\n" +
                             "Hourglasses: " + LevelGoalManager.GrandTotalCollected + " / " + maxHourglasses;
        }
    }
}