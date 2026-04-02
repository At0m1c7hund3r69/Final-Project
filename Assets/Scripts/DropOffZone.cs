using UnityEngine;

public class DropOffZone : MonoBehaviour
{
    [SerializeField] private string requiredItemId = "";
    [SerializeField] private Transform deliveryPoint;
    [SerializeField] private bool destroyOnDelivery = true;

    public bool CanAccept(GrabbableObject obj)
    {
        if (obj == null)
            return false;

        if (string.IsNullOrEmpty(requiredItemId))
            return true;

        return obj.ItemId == requiredItemId;
    }

    public void Accept(GrabbableObject obj)
    {
        if (!CanAccept(obj))
            return;

        Transform targetPoint = deliveryPoint != null ? deliveryPoint : transform;
        obj.Deliver(targetPoint, destroyOnDelivery);
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
