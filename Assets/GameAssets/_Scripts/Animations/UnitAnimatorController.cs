using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimatorController : MonoBehaviour
{

    private Animator _animator;

    private string attack = "Attack";
    private string attack2 = "Attack2";
    private string walk = "Walk";
    private string charge = "Charge";
    private string death = "Death";
    private string death2 = "Death2";
    private string idle = "Idle";

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Idle()
    {
        _animator.SetBool(walk, false);
        _animator.SetBool(charge, false);
    }

    public void Attack()
    {
        var random = Random.value;
        _animator.SetTrigger(random <= 0.5f ? attack : attack2);
    }

    public void Walk()
    {
        _animator.SetBool(walk, true);
        _animator.SetBool(charge, false);
    }

    public void Charge()
    {
        _animator.SetBool(walk, false);
        _animator.SetBool(charge, true);
    }

    public void Die()
    {
        var random = Random.value;
        _animator.SetTrigger(random <= 0.5f ? death : death2);
    }

    public void IdleTrigger()
    {
        _animator.SetTrigger(idle);
    }
}
