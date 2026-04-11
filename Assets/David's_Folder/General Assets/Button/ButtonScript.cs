using System.Collections;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public Animator anim;

    private bool canPress;

    public PunchGunV2 myGun;


    public int puzzleNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        canPress = true;
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    IEnumerator returnButton()
    {
        yield return new WaitForSeconds(1);
        anim.ResetTrigger("Pressed");
        canPress = true;
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PunchGun")
        {
            if (canPress == true)
            {
                anim.SetTrigger("Pressed");
                canPress = false;
                StartCoroutine(returnButton());

                if (puzzleNumber == 0)
                {

                }
            }

            myGun.returnSpring();

        }

        

    }

}
