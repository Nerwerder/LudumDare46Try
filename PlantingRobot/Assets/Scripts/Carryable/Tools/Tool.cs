using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : Carryable
{
    public float carryOffset = 0f;

    public new void Start() {
        base.Start();
    }
}
