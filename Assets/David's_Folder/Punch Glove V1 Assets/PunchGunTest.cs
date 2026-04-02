using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchGunTest : MonoBehaviour
{

    private bool isPunchGlove;

    Animator animator;

    public GameObject punchHand;

    public GameObject grabHand;

    private Transform endBone;

    private bool canChangeGlove;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        isPunchGlove = true;

        endBone = transform.Find("Back_Spring_Joint_12");

        canChangeGlove = true;
    }

    // Update is called once per frame
    void Update()
    {


        if (isPunchGlove == true)
        {
            punchHand.SetActive(true);
            grabHand.SetActive(false);
        }
        else if (isPunchGlove == false)
        {
            punchHand.SetActive(false);
            grabHand.SetActive(true);
        }


        //punchHand.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);

        //punchHand.transform.position = new Vector3(endBone.position.x, endBone.position.y, endBone.position.z);

        if (Keyboard.current.shiftKey.wasPressedThisFrame && canChangeGlove == true)
        {
            isPunchGlove = !isPunchGlove;
        }
        else if (Keyboard.current.fKey.wasPressedThisFrame && animator.GetBool("canPunch") == false)
        {

            animator.SetBool("canPunch", true);
            Debug.Log("Truth Nuke!");
            StartCoroutine(stopSpring(0.5f));
            canChangeGlove = false;
        }
        else if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            animator.SetTrigger("canGoBack");

            Debug.Log("X Pressed");
        }

        


    }





    IEnumerator stopSpring(float time)
    {
        yield return new WaitForSeconds(time);

        animator.SetTrigger("canGoBack");
        
    }


    public void makeGunUseable()
    {
        canChangeGlove = true;
    }

}