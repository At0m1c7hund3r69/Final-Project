using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private float rotateSpeed = 120f;

    private void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.World);
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

            Destroy(gameObject);
        }
    }
}