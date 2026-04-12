using UnityEngine;
using System.Collections.Generic;

public class BreakableWall : MonoBehaviour
{
    private static HashSet<string> brokenWallIDs = new HashSet<string>();
    public string uniqueWallID;

    private void Start()
    {
        if (brokenWallIDs.Contains(uniqueWallID))
        {
            Destroy(gameObject);
        }
    }
    public void BreakWall()
    {
        if (!string.IsNullOrEmpty(uniqueWallID))
        {
            brokenWallIDs.Add(uniqueWallID);
        }

        Destroy(gameObject);
    }
}
