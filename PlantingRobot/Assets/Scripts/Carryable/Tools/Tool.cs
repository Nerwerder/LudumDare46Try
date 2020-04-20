using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : Carryable
{
    public float carryOffset = 0f;
    public float toolRange = 3f;
    public int level = 0;
    public int cost = 0;

    public new void Start() {
        base.Start();
    }
}
