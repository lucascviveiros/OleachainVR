using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.SetBool("OtherAnimationInController", true);
    }

    // Update is called once per frame
    void Update()
    {
        //{Debug}
        /*if (Input.GetKeyDown("space"))
        {
            Debug.Log("Key pressed:");
            animator.SetBool("Idle", false);
            animator.SetBool("Take", true);
        }

        */
    }
}
