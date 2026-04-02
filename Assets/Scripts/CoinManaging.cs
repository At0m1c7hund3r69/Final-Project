using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    [SerializeField] private TMP_Text coinText;
    public int Coins { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        UpdateCoinText();
        Debug.Log("Coins: " + Coins);
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + Coins;
        }
    }
}