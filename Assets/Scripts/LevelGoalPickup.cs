using UnityEngine;

public class LevelGoalPickup : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 120f;

    private void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null || other.GetComponentInParent<PlayerMovement>() != null)
        {
            if (LevelGoalManager.Instance != null)
            {
                LevelGoalManager.Instance.CollectObjective(1);
            }

            Destroy(gameObject);
        }
    }
}