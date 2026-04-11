using UnityEngine;

public class BridgeTransition : MonoBehaviour
{
    [SerializeField] private Vector3 raisedEulerAngles;
    [SerializeField] private Vector3 loweredEulerAngles = new Vector3(0f, 0f, -90f);
    [SerializeField] private float rotateSpeed = 90f;

    private Quaternion raisedRotation;
    private Quaternion loweredRotation;
    private bool shouldLower;
    private bool isLowered;

    private void Awake()
    {
        raisedRotation = Quaternion.Euler(raisedEulerAngles);
        loweredRotation = Quaternion.Euler(loweredEulerAngles);

        transform.rotation = raisedRotation;
    }

    private void Update()
    {
        if (!shouldLower)
            return;

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            loweredRotation,
            rotateSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(transform.rotation, loweredRotation) < 0.1f)
        {
            transform.rotation = loweredRotation;
            shouldLower = false;
            isLowered = true;
        }
    }

    public void LowerBridge()
    {
        if (isLowered)
            return;

        shouldLower = true;
    }
}
