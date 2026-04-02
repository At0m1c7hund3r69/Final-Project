using UnityEngine;

public class GloveCheck : MonoBehaviour
{

    public GameObject gunBase;

    private Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gunBase.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            anim.SetTrigger("canGoBack");

            Debug.Log("Trigger Entered");
        }
    }

}