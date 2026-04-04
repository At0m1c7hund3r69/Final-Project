using System.Collections;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public Animator anim;

    private bool canPress;

    public GameObject placeholder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canPress = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == ("ButtonInteraction"))
        {
            if(canPress == true)
            {
                canPress = false;
                anim.SetTrigger("Pressed");
                StartCoroutine("returnButton");
                Destroy(placeholder);
            }
        }
    }

    IEnumerator returnButton()
    {
        yield return new WaitForSeconds(1);
        canPress = true;

    }

}
