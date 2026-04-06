using UnityEngine;

public class PunchHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BreakableWall wall = other.GetComponent<BreakableWall>()
                             ?? other.GetComponentInParent<BreakableWall>();

        if (wall != null)
        {
            wall.BreakWall();
        }
    }
}