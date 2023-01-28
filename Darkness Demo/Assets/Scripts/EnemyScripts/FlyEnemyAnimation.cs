using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyAnimation : MonoBehaviour
{
    [SerializeField]
    public enum AnimStates
    {
        Idle,
        AttackBfHit,
        AttackHit,
        AfterAttack,
        Walk,
        DeathUp,
        DeathDown,
        Hurt
    }

    public static AnimStates State;

    private Animator _animator;
    private string currentState;

    //animation name
    const string IdleAnim = "Idle";
    const string WalkAnim = "Walk";
    const string HurtAnim = "Hurt";
    const string DeathUpAnim = "DeathUp";
    const string DeathDownAnim = "DeathDown";
    const string AttackBfHitAnim = "AttackBfHit";
    const string AttackHitAnim = "AttackHit";
    const string AfterAttack = "AfterAttack";

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
            case AnimStates.AttackBfHit:
                Animation.ChangeAnimationState(AttackBfHitAnim, currentState, _animator);
                break;
            case AnimStates.AttackHit:
                Animation.ChangeAnimationState(AttackHitAnim, currentState, _animator);
                break;
            case AnimStates.AfterAttack:
                Animation.ChangeAnimationState(AfterAttack, currentState, _animator);
                break;
            case AnimStates.Walk:
                Animation.ChangeAnimationState(WalkAnim, currentState, _animator);
                break;
            case AnimStates.DeathUp:
                Animation.ChangeAnimationState(DeathUpAnim, currentState, _animator);
                break;
            case AnimStates.DeathDown:
                Animation.ChangeAnimationState(DeathDownAnim, currentState, _animator);
                break;
            case AnimStates.Hurt:
                Animation.ChangeAnimationState(HurtAnim, currentState, _animator);
                break;
            default:
                break;
        }
    }
}
