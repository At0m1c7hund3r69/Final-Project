using UnityEngine;

public class GloveScript : MonoBehaviour
{

    public PunchGunV2 myGun;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            myGun.returnSpring();

            Debug.Log("Collider Triggered");
        }
        
    }

}
