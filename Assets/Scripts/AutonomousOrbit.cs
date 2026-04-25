using UnityEngine;

public class AutonomousOrbit : MonoBehaviour
{
    [Header("Orbit Settings")]
    [Tooltip("How fast it spins on each axis (X, Y, Z). Usually, you just want Y for a flat orbit.")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 90f, 0f);

    private void Update()
    {
        // Continuously rotates the object based on the speed and the time passed since the last frame.
        // Space.World ensures it spins on the global axis, keeping the orbit perfectly flat.
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
    }
}
