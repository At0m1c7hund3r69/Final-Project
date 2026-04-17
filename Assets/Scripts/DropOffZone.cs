using UnityEngine;
using System.Collections.Generic;

public class DropOffZone : MonoBehaviour
{
    private static HashSet<string> completedZones = new HashSet<string>();

    [Header("Persistence")]
    [Tooltip("Type a unique ID to keep the door open across level transitions (e.g., 'Level3_KeyDrop')")]
    [SerializeField] private string uniqueZoneID = "";

    [SerializeField] private string requiredItemId = "";
    [SerializeField] private int requiredQuantity = 1;

    [SerializeField] private Transform deliveryPoint;
    [SerializeField] private bool destroyOnDelivery = true;

    [Header("Spawn On Delivery")]
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool spawnOnlyOnce = true;

    [Header("Door Unlocking (New)")]
    [Tooltip("Assign the door object you want to disappear when the delivery is complete")]
    [SerializeField] private GameObject doorToOpen;

    private int currentQuantity;
    private bool hasSpawned;

    private void Start()
    {
        // When the scene loads, check if this zone was already completed previously
        if (!string.IsNullOrEmpty(uniqueZoneID) && completedZones.Contains(uniqueZoneID))
        {
            currentQuantity = requiredQuantity;
            UnlockDoor();

            // If it was supposed to spawn a reward item, make sure it still spawns
            if (spawnOnlyOnce)
            {
                TrySpawnObject();
            }
        }
    }

    public bool CanAccept(GrabbableObject obj)
    {
        if (obj == null)
            return false;

        if (spawnOnlyOnce && hasSpawned)
            return false;

        if (!string.IsNullOrEmpty(requiredItemId) && obj.ItemId != requiredItemId)
            return false;

        return true;
    }

    public void Accept(GrabbableObject obj)
    {
        if (!CanAccept(obj))
            return;

        Transform targetPoint = deliveryPoint != null ? deliveryPoint : transform;
        obj.Deliver(targetPoint, destroyOnDelivery);

        currentQuantity++;

        Debug.Log($"{name}: Delivered {currentQuantity}/{requiredQuantity} of {requiredItemId}");

        if (currentQuantity >= requiredQuantity)
        {
            // Save the completion state to the global memory list
            if (!string.IsNullOrEmpty(uniqueZoneID))
            {
                completedZones.Add(uniqueZoneID);
            }

            TrySpawnObject();
            UnlockDoor();
        }
    }

    private void TrySpawnObject()
    {
        if (objectToSpawn == null)
            return;

        if (spawnOnlyOnce && hasSpawned)
            return;

        Transform point = spawnPoint != null ? spawnPoint : transform;

        Instantiate(objectToSpawn, point.position, point.rotation);
        hasSpawned = true;

        Debug.Log($"{name}: Spawned {objectToSpawn.name}");
    }

    private void UnlockDoor()
    {
        if (doorToOpen != null)
        {
            // Instantly deactivate the physical door object to open the path
            doorToOpen.SetActive(false);
            Debug.Log($"{name}: Door opened!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CarryController carryController =
            other.GetComponent<CarryController>() ??
            other.GetComponentInParent<CarryController>();

        if (carryController != null && carryController.IsHolding)
        {
            carryController.TryDeliverTo(this);
        }
    }
}