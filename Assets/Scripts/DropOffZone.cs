using UnityEngine;

public class DropOffZone : MonoBehaviour
{
    [SerializeField] private string requiredItemId = "";
    [SerializeField] private int requiredQuantity = 1;

    [SerializeField] private Transform deliveryPoint;
    [SerializeField] private bool destroyOnDelivery = true;

    [Header("Spawn On Delivery")]
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool spawnOnlyOnce = true;

    private int currentQuantity;
    private bool hasSpawned;

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
            TrySpawnObject();
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