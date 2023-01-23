using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    private string currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < 0)
            ChangeAnimationState("FallAnim");
        else if (rb.velocity.y > 0)
            ChangeAnimationState("JumpAnim");

        else if (Input.GetAxisRaw("Horizontal") != 0)
            ChangeAnimationState("RunAnim");

        else
            ChangeAnimationState("Idle");
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
}