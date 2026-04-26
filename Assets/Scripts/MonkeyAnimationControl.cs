using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonkeyAnimationControl : MonoBehaviour
{


    private Animator myAnim;

    private bool isWalking;

    public float jumpLandingTime;

    public AudioClip[] jumpSounds;

    private AudioSource mySource;

    private bool isJumping;

    private bool canPlaySound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isWalking = false;

        myAnim = GetComponent<Animator>();

        mySource = GetComponent<AudioSource>();

        isJumping = false;

        canPlaySound = true;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCommands();
    }


    private void KeyCommands()
    {

        if (Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            isWalking = true;
        }
        else if (!Keyboard.current.anyKey.isPressed)
        {
            isWalking = false;
        }
        
        if (Keyboard.current.spaceKey.isPressed && isJumping == false)
        {
            
            myAnim.SetTrigger("isJumping");
            isJumping = true;

            int jumpClip = Random.Range(0, 3);

            if (canPlaySound == true)
            {
                
                canPlaySound = false;
                if (jumpClip == 0)
                {
                    mySource.PlayOneShot(jumpSounds[0]);
                }
                else if (jumpClip == 1)
                {
                    mySource.PlayOneShot(jumpSounds[1]);
                }
                else if (jumpClip == 2)
                {
                    mySource.PlayOneShot(jumpSounds[2]);
                }
                
            }
            

        }

        if (!Keyboard.current.spaceKey.isPressed && isJumping == true)
        {
            isJumping = false;
            
        }


        if (isWalking == true)
        {
            myAnim.SetTrigger("isWalking");
            myAnim.ResetTrigger("stopWalking");
        }
        else if (isWalking == false)
        {
            myAnim.ResetTrigger("isWalking");
            myAnim.SetTrigger("stopWalking");
        }
    }


    IEnumerator JumpTimer(float landingTime)
    {
        yield return new WaitForSeconds(landingTime);
        
        myAnim.ResetTrigger("isJumping");
        
    }

    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            returnJump();
        }
    }

    

    public void returnJump()
    {
        if (isWalking == true)
        {
            myAnim.SetTrigger("isWalking");
            myAnim.ResetTrigger("stopWalking");
            myAnim.ResetTrigger("isJumping");
        }
        else if (isWalking == false)
        {
            myAnim.SetTrigger("stopWalking");
            myAnim.ResetTrigger("isWalking");
            myAnim.ResetTrigger("isJumping");
        }
        canPlaySound = true;

    }

}
