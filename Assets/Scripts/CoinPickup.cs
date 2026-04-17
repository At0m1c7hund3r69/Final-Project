using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private static HashSet<string> collectedCoins = new HashSet<string>();

    [Header("Save State")]
    [Tooltip("Give this coin a unique ID so it doesn't respawn")]
    public string uniqueSaveID;

    [SerializeField] private int value = 1;
    [SerializeField] private float rotateSpeed = 120f;

    [Header("Audio Settings")]
    [Tooltip("Drop your coin pickup sound effect here")]
    [SerializeField] private AudioClip pickupSound;

    private void Start()
    {
        if (!string.IsNullOrEmpty(uniqueSaveID) && collectedCoins.Contains(uniqueSaveID))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.World);
    }

    public static void ResetCollectedBells()
    {
        collectedCoins.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBody body = other.GetComponent<PlayerBody>();

        if (body != null)
        {
            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.AddCoins(value);
            }

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            if (!string.IsNullOrEmpty(uniqueSaveID))
            {
                collectedCoins.Add(uniqueSaveID);
            }

            Destroy(gameObject);
        }
    }
}