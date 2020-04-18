using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : Carryable
{
    public float distance = 0f;
    public float heightOffset = 0f;

    public new void Start() {
        base.Start();
    }
}
