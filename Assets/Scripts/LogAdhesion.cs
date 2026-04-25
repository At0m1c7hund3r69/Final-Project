using UnityEngine;

public class LogAdhesion : MonoBehaviour
{
    private CharacterController playerController;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = null;
        }
    }

    private void LateUpdate()
    {
        if (playerController != null)
        {
            if (playerController.isGrounded)
            {
                Vector3 platformMovement = transform.position - lastPosition;
                platformMovement.y -= 0.05f;
                playerController.Move(platformMovement);
            }
        }

        lastPosition = transform.position;
    }
}