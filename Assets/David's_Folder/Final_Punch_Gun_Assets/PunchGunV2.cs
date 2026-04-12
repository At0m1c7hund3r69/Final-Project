using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchGunV2 : MonoBehaviour
{

    private Animator anim;

    private bool canPunch;

    public float punchOutTime;

    bool isTimerRunning;

    float currentTime;

    float currentSpeed;

    AnimatorStateInfo stateInfo;



    public GameObject punchGlove;

    public GameObject grabGlove;

    private bool isPunchGlove;

    private bool canChangeGlove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

        canPunch = true;

        isTimerRunning = false;

        currentTime = 0;

        isPunchGlove = true;

        canChangeGlove = true;


    }

    // Update is called once per frame
    void Update()
    {


        if (isPunchGlove == true)
        {
            punchGlove.SetActive(true);
            grabGlove.SetActive(false);
        }
        else if (isPunchGlove == false)
        {
            punchGlove.SetActive(false);
            grabGlove.SetActive(true);
        }



        if (isTimerRunning == true)
        {
            currentTime += Time.deltaTime;
        }

        currentSpeed = anim.GetFloat("Speed");

        bool isShiftPressed = Keyboard.current.shiftKey.wasPressedThisFrame;
        bool isGamepadYPressed = Gamepad.current != null && Gamepad.current.yButton.wasPressedThisFrame;

        if ((isShiftPressed || isGamepadYPressed) && canChangeGlove == true)
        {
            isPunchGlove = !isPunchGlove;
        }

        bool isFPressed = Keyboard.current.fKey.wasPressedThisFrame;
        bool isGamepadXPressed = Gamepad.current != null && Gamepad.current.xButton.wasPressedThisFrame;

        if ((isFPressed || isGamepadXPressed) && canPunch == true)
        {
            Debug.Log("Space pressed!");

            anim.Play("SpringOut", -1, 0f);

            anim.SetFloat("Speed", 2f);

            canPunch = false;

            isTimerRunning = true;

            canChangeGlove = false;

            //StartCoroutine(defaultSpringReturn());

        }

        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("SpringOut") && stateInfo.normalizedTime >= 1.0f && !anim.IsInTransition(0))
        {
            returnSpring();
        }


    }


    public void returnSpring()
    {
        anim.ResetTrigger("Punch");
        isTimerRunning = false;
        anim.SetFloat("Speed", -2f);
        StartCoroutine(restorePunch());
    }


    

    IEnumerator restorePunch()
    {
        yield return new WaitForSeconds(currentTime);

        currentTime = 0;

        canPunch = true;

        canChangeGlove = true;

        anim.Play("Idle", -1, 0f);


    }

}
