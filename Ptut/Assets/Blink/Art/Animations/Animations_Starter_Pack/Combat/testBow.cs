using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBow : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("Aim",true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("Aim",false);
        }



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Aiming",true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("Aiming", false);
        }


        //if (animator.GetBool("Aim") && animator.GetBool("Aiming"))
        //{

        //}
    }
}
