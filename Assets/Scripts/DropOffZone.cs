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

    [Header("Visuals On Delivery")]
    [Tooltip("The non-interactive version of the item to leave on the pedestal")]
    [SerializeField] private GameObject deliveredPropPrefab;
    [Tooltip("The exact anchor location to spawn the dummy prop")]
    [SerializeField] private Transform propSpawnPoint;

    [Header("Door Unlocking (New)")]
    [Tooltip("Assign the door object you want to disappear when the delivery is complete")]
    [SerializeField] private GameObject doorToOpen;

    private int currentQuantity;
    private bool hasSpawned;

    private void Start()
    {
        if (!string.IsNullOrEmpty(uniqueZoneID) && completedZones.Contains(uniqueZoneID))
        {
            currentQuantity = requiredQuantity;
            UnlockDoor();

            //if (spawnOnlyOnce)
            //{
            //    TrySpawnObject();
            //}

            if (deliveredPropPrefab != null && propSpawnPoint != null)
            {
                Instantiate(deliveredPropPrefab, propSpawnPoint.position, propSpawnPoint.rotation);
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

        if (destroyOnDelivery && deliveredPropPrefab != null && propSpawnPoint != null)
        {
            Instantiate(deliveredPropPrefab, propSpawnPoint.position, propSpawnPoint.rotation, propSpawnPoint);
        }

        if (currentQuantity >= requiredQuantity)
        {
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