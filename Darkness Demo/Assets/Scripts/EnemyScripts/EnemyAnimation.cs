using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    public enum AnimStates
    {
        Idle,
        Attack,
        Walk,
        Death,
        Hurt
    }

    public AnimStates State;

    private Animator _animator;
    private string currentState;

    //animation name
    const string IdleAnim = "Idle";
    const string WalkAnim = "Walk";
    const string HurtAnim = "Hurt";
    const string DeathAnim = "Death";
    const string AttackAnim = "Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (State)
        {
            case AnimStates.Idle:
                Animation.ChangeAnimationState(IdleAnim, currentState, _animator);
                break;
            case AnimStates.Attack:
                Animation.ChangeAnimationState(AttackAnim, currentState, _animator);
                break;
            case AnimStates.Walk:
                Animation.ChangeAnimationState(WalkAnim, currentState, _animator);
                break;
            case AnimStates.Death:
                Animation.ChangeAnimationState(DeathAnim, currentState, _animator);
                break;
            case AnimStates.Hurt:
                Animation.ChangeAnimationState(HurtAnim, currentState, _animator);
                break;
            default:
                break;
        }
    }
}
