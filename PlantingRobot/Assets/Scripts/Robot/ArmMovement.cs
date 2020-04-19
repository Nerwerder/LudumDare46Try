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
        Debug.Log("ArmMovement.ArmsUp");
        _animator.SetTrigger("Up");
    }

    public void ArmsMid()
    {
        Debug.Log("ArmMovement.ArmsMid");
        _animator.SetTrigger("Mid");
    }

    public void ArmsDown()
    {
        Debug.Log("ArmMovement.ArmsDown");
        _animator.SetTrigger("Down");
    }
}
