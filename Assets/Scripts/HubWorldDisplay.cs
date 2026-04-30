using UnityEngine;
using TMPro;

public class HubWorldDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text boardText;

    [Header("Game Totals")]
    [Tooltip("Type the total amount of Bells available in the entire game")]
    [SerializeField] private int maxBells = 90;

    [Tooltip("The amount of Hourglasses needed to win")]
    [SerializeField] private int maxHourglasses = 9;

    private void Update()
    {
        if (boardText != null)
        {
            boardText.text = 
                             " " + CoinManager.Coins + " / " + maxBells + "\n" + "\n" +
                             " " + LevelGoalManager.GrandTotalCollected + " / " + maxHourglasses;
        }
    }
}