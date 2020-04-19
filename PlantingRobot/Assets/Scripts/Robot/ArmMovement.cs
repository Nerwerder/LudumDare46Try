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
    }

    public void ArmsMid()
    {
        _animator.SetTrigger("Mid");
    }

    public void ArmsDown()
    {
        _animator.SetTrigger("Down");
    }



}
