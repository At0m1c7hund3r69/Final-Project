using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] private string itemId = "";
    [SerializeField] private bool disableCollidersWhileHeld = true;

    private Rigidbody rb;
    private Collider[] allColliders;

    public string ItemId => itemId;
    public bool IsHeld { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        allColliders = GetComponentsInChildren<Collider>();
    }

    public bool Grab(Transform holdPoint)
    {
        if (IsHeld || holdPoint == null)
            return false;

        IsHeld = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        if (disableCollidersWhileHeld)
        {
            foreach (Collider col in allColliders)
            {
                if (col != null)
                    col.enabled = false;
            }
        }

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        return true;
    }

    public void Release(Vector3 worldPosition, Quaternion worldRotation)
    {
        IsHeld = false;

        transform.SetParent(null);
        transform.SetPositionAndRotation(worldPosition, worldRotation);

        if (disableCollidersWhileHeld)
        {
            foreach (Collider col in allColliders)
            {
                if (col != null)
                    col.enabled = true;
            }
        }

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    public void Deliver(Transform deliveryPoint, bool destroyOnDelivery)
    {
        IsHeld = false;

        if (destroyOnDelivery)
        {
            Destroy(gameObject);
            return;
        }

        if (deliveryPoint != null)
        {
            transform.SetParent(deliveryPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            transform.SetParent(null);
        }

        if (disableCollidersWhileHeld)
        {
            foreach (Collider col in allColliders)
            {
                if (col != null)
                    col.enabled = true;
            }
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}