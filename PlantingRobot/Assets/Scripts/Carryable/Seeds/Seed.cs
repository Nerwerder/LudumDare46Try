using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Carryable
{
    public override CarryableInteractionResult InteractWith(Container c) {
        //Nothing
        return new CarryableInteractionResult(null, false);
    }

    public override CarryableInteractionResult InteractWith(Planter p) {
        if (p.IsFree()) {
            p.Plant(this);
            Destroy(gameObject);
            return new CarryableInteractionResult(null, true);
        }
        return new CarryableInteractionResult(this, false); ;
    }
}
