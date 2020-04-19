using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void ArmsUp()
    {
        _animator.SetTrigger("Up");
        _animator.ResetTrigger("Down");
        _animator.ResetTrigger("Mid");
    }

    public void ArmsMid()
    {
        _animator.SetTrigger("Mid");
        _animator.ResetTrigger("Down");
        _animator.ResetTrigger("Up");
    }

    public void ArmsDown()
    {
        _animator.SetTrigger("Down");
        _animator.ResetTrigger("Up");
        _animator.ResetTrigger("Mid");
    }
}
