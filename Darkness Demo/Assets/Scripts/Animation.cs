using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public static Animator animator;
    Rigidbody2D rb;
    private string currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Attack.Instance.isAttacking && Attack.Instance.attackCount == 2)
            ChangeAnimationState("Attack3", currentState, animator);

        else if (Attack.Instance.isAttacking && Attack.Instance.attackCount == 1)
            ChangeAnimationState("Attack2", currentState, animator);

        else if (Attack.Instance.isAttacking && Attack.Instance.attackCount == 0)
            ChangeAnimationState("Attack1", currentState, animator);

        else if (rb.linearVelocity.y < -0.001f)
            ChangeAnimationState("FallAnim", currentState, animator);

        else if (rb.linearVelocity.y > 0.001f)
            ChangeAnimationState("JumpAnim", currentState, animator);

        else if (Input.GetAxisRaw("Horizontal") != 0)
            ChangeAnimationState("RunAnim", currentState, animator);

        else 
            ChangeAnimationState("Idle", currentState, animator);
    }

    public static void ChangeAnimationState(string newState, string currentState, Animator animator)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
}