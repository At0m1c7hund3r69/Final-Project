using UnityEngine;

public class GrabHitbox : MonoBehaviour
{
    private CarryController carryController;

    private void Awake()
    {
        carryController = GetComponentInParent<CarryController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (carryController == null || carryController.IsHolding)
            return;

        GrabbableObject grabbable =
            other.GetComponent<GrabbableObject>() ??
            other.GetComponentInParent<GrabbableObject>();

        if (grabbable != null)
        {
            carryController.TryGrab(grabbable);
        }
    }
}
