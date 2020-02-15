using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Walk : StateMachineBehaviour
{
    Rigidbody rb;
    DragonController controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!rb)
            rb = animator.GetComponentInParent<Rigidbody>();

        if (!controller)
            controller = animator.GetComponentInParent<DragonController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = controller.Direction.normalized * controller.WalkSpeed;
    }
}
