using UnityEngine;

public class CarryController : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;

    public GrabbableObject HeldObject { get; private set; }
    public bool IsHolding => HeldObject != null;

    private void Reset()
    {
        holdPoint = transform;
    }

    public bool TryGrab(GrabbableObject obj)
    {
        if (obj == null || IsHolding || obj.IsHeld || holdPoint == null)
            return false;

        bool grabbed = obj.Grab(holdPoint);

        if (grabbed)
        {
            HeldObject = obj;
            return true;
        }

        return false;
    }

    public bool TryDeliverTo(DropOffZone zone)
    {
        if (zone == null || !IsHolding)
            return false;

        if (!zone.CanAccept(HeldObject))
            return false;

        string deliveredName = HeldObject.name;
        zone.Accept(HeldObject);
        HeldObject = null;

        return true;
    }

    public void ForceDrop()
    {
        if (!IsHolding || holdPoint == null)
            return;

        HeldObject.Release(holdPoint.position, holdPoint.rotation);
        HeldObject = null;
    }
}